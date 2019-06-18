using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using FileIO = System.IO.File;
using Kemrex.Database;
using Kemrex.FWCommon;
using Kemrex.SoilCalculator.Web.ActionFilters;
using Kemrex.SoilCalculator.Web.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class KPTController : BaseController
    {
        public UnitOfWork uow;
        public int CurrentUID => (Session["sid"].Convert2String().ParseIntNullable() ?? 0);
        public SysAccount CurrentUser => uow.Account.Get(CurrentUID);
        public DateTime CurrentDateTime = DateTime.Now;
        public KPTController()
        {
            uow = new UnitOfWork();
        }

        [BackendAuthorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType)
        {
            SysRole role = CurrentUser.Role(3).Role();
            ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            ViewBag.canExport = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 4 && x.PermissionFlag).Count() > 0;
            List<TblKpt> lst = new List<TblKpt>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                var data = uow.db.TblKpt.AsQueryable();
                if (!CurrentUser.FlagAdminCalc)
                {
                    var staffs = db.CalcAccountStaff.Where(x => x.AccountId == CurrentUID).Select(x => x.StaffId).ToList();
                    if (staffs.Count() > 0) { data = data.Where(x => staffs.Contains(x.CreatedBy) || x.CreatedBy == CurrentUID); }
                    else { data = data.Where(x => x.CreatedBy == CurrentUID); }
                }
                int total = data.Count();
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "KPT", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>(),
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = data
                    .OrderByDescending(x => x.KptDate)
                    .ThenBy(x => x.ProjectName)
                    .Include(x => x.TblKptDetail)
                    .Skip((Pagination .Page- 1)* Pagination.Size)
                    .Take(Pagination.Size)
                    .ToList();
            }
            catch (Exception ex)
            {
                WidgetAlertModel Alert = new WidgetAlertModel()
                {
                    Type = AlertMsgType.Danger,
                    Message = ex.GetMessage()
                };
                ViewBag.Alert = Alert;
            }
            return View(lst);
        }

        [BackendAuthorized]
        public ActionResult Export(int id)
        {
            try
            {
                TblKptStation ob = db.TblKptStation
                    .Where(x => x.StationId == id)
                    .Include(x => x.TblKpt)
                    .Include(x => x.TblKptStationAttachment)
                    .Include(x => x.TblKptStationDetail)
                    .FirstOrDefault();
                if (ob == null)
                { throw new Exception("No KPT Data."); }
                DataTable dt = new DataTable("ds");
                dt.Columns.Add(new DataColumn("ImgUri"));
                //foreach (TblKptStationAttachment it in ob.TblKptStationAttachment)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["ImgUri"] = new Uri(Server.MapPath("~/" + it.AttachPath)).AbsoluteUri;
                //    dt.Rows.Add(dr);
                //}
                //DataSet ds = new DataSet();
                //ds.Tables.Add(dt);
                ReportDataSource rptData = new ReportDataSource("ds", dt);
                ReportViewer rptViewer = new ReportViewer();
                rptViewer.ProcessingMode = ProcessingMode.Local;
                rptViewer.LocalReport.ReportPath = Server.MapPath("~/reports/rptKPT.rdlc");
                rptViewer.LocalReport.SetParameters(new ReportParameter("headProjectName", ob.TblKpt.ProjectName));
                rptViewer.LocalReport.SetParameters(new ReportParameter("headCustomerName", ob.TblKpt.CustomerName));
                rptViewer.LocalReport.SetParameters(new ReportParameter("headLocation", ob.TblKpt.KptLocation + (ob.TblKpt.SubDistrictId > 0 ?
                    " " + ob.TblKpt.SubDistrict().SubDistrictNameTH + " " + ob.TblKpt.SubDistrict().District().DistrictNameTH + " " + ob.TblKpt.SubDistrict().District().State().StateNameTH :
                    "")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("headStation", ob.StationName));
                rptViewer.LocalReport.SetParameters(new ReportParameter("headDate", ob.TblKpt.KptDate.ToString("dd/MM/yyyy")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("headTestedBy", ob.StationTestBy));
                int endKpt = 0;
                decimal sumAvgN = 0;
                decimal sumAvgQu = 0;
                for (int i = 1; i <= 7; i++)
                {
                    if (ob.TblKptStationDetail
                        .Where(x =>
                            x.StationDepth >= ((5 * (i - 1)) + 1)
                            && x.StationDepth <= ((5 * (i - 1)) + 5))
                        .Sum(x => x.StationBlowCount) > 0 && (endKpt + 1 == i))
                    {
                        int sumCount = 0;
                        decimal sumQu = 0;
                        for (int j = (5 * (i - 1)) + 1; j <= (5 * (i - 1)) + 5; j++)
                        {
                            var tmp = ob.TblKptStationDetail.Where(x => x.StationDepth == j).FirstOrDefault();
                            if (tmp == null) { throw new Exception("Failed to retrive data. [Id: " + ob.StationId + "-i:" + i + "-j:" + j + "]"); }
                            int blowCount = tmp.StationBlowCount;
                            int blowCountN = (int)(blowCount > 15 ? 15 + Math.Floor(0.5 * (blowCount - 15)) : blowCount);
                            decimal qu = blowCountN <= 0 ? 0 : (decimal)1.92 * (blowCountN + (decimal)0.954);
                            qu = Math.Floor(qu * 100) / 100;
                            rptViewer.LocalReport.SetParameters(new ReportParameter("dataBlowCount_" + j, blowCount.ToString()));
                            rptViewer.LocalReport.SetParameters(new ReportParameter("dataBearing_" + j, qu.ToString("0.00")));
                            sumCount += blowCount;
                            sumQu += qu;
                        }
                        decimal avgN = Math.Floor(((decimal)sumCount / 5) * 100) / 100;
                        decimal avgQu = Math.Floor((sumQu / 5) * 100) / 100;
                        decimal cohesion = Math.Floor((avgQu / 2) * 100) / 100;
                        rptViewer.LocalReport.SetParameters(new ReportParameter("dataAvgN_" + i, avgN.ToString("0.00")));
                        rptViewer.LocalReport.SetParameters(new ReportParameter("dataAvgQu_" + i, avgQu.ToString("0.00")));
                        rptViewer.LocalReport.SetParameters(new ReportParameter("dataCohesion_" + i, cohesion.ToString("0.00")));
                        sumAvgN += avgN;
                        sumAvgQu += avgQu;
                        endKpt = i;
                    }
                }
                decimal lastN = (sumAvgN / (decimal)endKpt);
                decimal lastQu = (sumAvgQu / (decimal)endKpt);
                decimal lastCohesion = Math.Floor((lastQu / 2) * 100) / 100;
                rptViewer.LocalReport.SetParameters(new ReportParameter("avgN", lastN.ToString("0.00")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("avgQu", lastQu.ToString("0.00")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("avgCohesion", lastCohesion.ToString("0.00")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataEndKpt", endKpt.ToString()));
                rptViewer.LocalReport.SetParameters(new ReportParameter("PreparedBy", CurrentUser.AccountName));
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.DataSources.Add(rptData);


                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;
                byte[] bytes = rptViewer.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);

                if (ob.TblKptStationAttachment.Count() > 0)
                {
                    foreach (TblKptStationAttachment it in ob.TblKptStationAttachment)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ImgUri"] = new Uri(Server.MapPath("~/" + it.AttachPath)).AbsoluteUri;
                        dt.Rows.Add(dr);
                    }
                    ReportDataSource rptAttachData = new ReportDataSource("ds", dt);
                    ReportViewer rptAttachViewer = new ReportViewer();
                    rptAttachViewer.ProcessingMode = ProcessingMode.Local;
                    rptAttachViewer.LocalReport.ReportPath = Server.MapPath("~/reports/rptKPTAttachment.rdlc");
                    rptAttachViewer.LocalReport.EnableExternalImages = true;
                    rptAttachViewer.LocalReport.DataSources.Clear();
                    rptAttachViewer.LocalReport.DataSources.Add(rptAttachData);
                    Warning[] warningsAttach;
                    string[] streamidsAttach;
                    string mimeTypeAttach;
                    string encodingAttach;
                    string filenameExtensionAttach;
                    byte[] bytesAttach = rptAttachViewer.LocalReport.Render(
                        "PDF", null, out mimeTypeAttach, out encodingAttach, out filenameExtensionAttach,
                        out streamidsAttach, out warningsAttach);
                    List<byte[]> pdf = new List<byte[]>();
                    pdf.Add(bytes);
                    pdf.Add(bytesAttach);

                    byte[] mergedPdf = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Document document = new Document())
                        {
                            using (PdfCopy copy = new PdfCopy(document, ms))
                            {
                                document.Open();

                                for (int i = 0; i < pdf.Count; ++i)
                                {
                                    PdfReader reader = new PdfReader(pdf[i]);
                                    // loop over the pages in that document
                                    int n = reader.NumberOfPages;
                                    for (int page = 0; page < n;)
                                    {
                                        copy.AddPage(copy.GetImportedPage(reader, ++page));
                                    }
                                }
                            }
                        }
                        mergedPdf = ms.ToArray();
                        bytes = mergedPdf;
                    }
                }

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename= KPT-" + CurrentDate.ToString("yyyy-MM-dd_HHmmss") + "." + filenameExtension);
                Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
                Response.Flush(); // send it to the client to download  
                Response.End();
                return View();
            }
            catch (Exception ex)
            { return RedirectToAction("Index", "KPT", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        [BackendAuthorized]
        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            SysRole role = CurrentUser.Role(3).Role();
            ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            ViewBag.canExport = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 4 && x.PermissionFlag).Count() > 0;
            TblKpt ob = uow.db.TblKpt
                .Include(x => x.TblKptDetail)
                .Include(x => x.TblKptAttachment)
                .Where(x => x.KptId == id).FirstOrDefault() ?? new TblKpt() { TblKptDetail = new List<TblKptDetail>(), TblKptAttachment = new List<TblKptAttachment>() };
            if (ob.KptId <= 0)
            {
                ob.KptDate = CurrentDateTime.Date;
            }
            return ViewDetail(ob, msg, msgType);
        }

        [BackendAuthorized]
        public ActionResult Station(int kId, int? id, string msg, AlertMsgType? msgType)
        {
            SysRole role = CurrentUser.Role(3).Role();
            ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            ViewBag.canExport = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 4 && x.PermissionFlag).Count() > 0;
            TblKptStation ob = uow.db.TblKptStation
                .Include(x => x.TblKptStationDetail)
                .Where(x => x.StationId == id).FirstOrDefault() ?? new TblKptStation() { TblKptStationDetail = new List<TblKptStationDetail>() };
            if (ob.StationId <= 0)
            {
                ob.KptId = kId;
                ob.StationOrder = db.TblKptStation.Where(x => x.KptId == kId).Count() <= 0 ? 1 : db.TblKptStation.Where(x => x.KptId == kId).Max(x => x.StationOrder) + 1;
            }
            return ViewStation(ob, msg, msgType);
        }
        
        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int Id = Request.Form["KptId"].ParseInt();
            TblKpt ob = uow.db.TblKpt.Find(Id) ?? new TblKpt();
            if (ob.KptId <= 0)
            {
                ob.KptDate = CurrentDateTime.Date;
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDateTime;
            }
            ob.ProjectName = Request.Form["ProjectName"];
            ob.CustomerName = Request.Form["CustomerName"];
            ob.KptLatitude = Request.Form["KptLatitude"];
            ob.KptLongtitude = Request.Form["KptLongtitude"];
            ob.SubDistrictId = Request.Form["SubDistrictId"].ParseInt();
            ob.KptLocation = Request.Form["KptLocation"];
            //ob.KptStation = Request.Form["KptStation"];
            ob.KptRemark = Request.Form["KptRemark"];
            ob.TestBy = Request.Form["TestBy"];
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDateTime;
            try
            {
                bool flagInsert = false;
                if (ob.KptId <= 0) {
                    uow.db.TblKpt.Add(ob);
                    flagInsert = true;
                }
                else { uow.db.Entry(ob).State = EntityState.Modified; }
                uow.SaveChanges();
                
                if (flagInsert && ob.KptId > 0)
                {
                    return RedirectToAction("Detail", new
                    {
                        id = ob.KptId,
                        controller = "KPT",
                        msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                        msgType = AlertMsgType.Success
                    });
                }
                else
                {
                    return RedirectToAction("Index", new
                    {
                        area = "",
                        controller = "KPT",
                        msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                        msgType = AlertMsgType.Success
                    });
                }
            }
            catch (DbEntityValidationException ex)
            {
                string msg = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    msg += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += string.Format("{{\n}}- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return ViewDetail(ob, msg, AlertMsgType.Danger);
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage();
                return ViewDetail(ob, msg, AlertMsgType.Danger);
            }
        }

        [HttpPost]
        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        public ActionResult SetAttach()
        {
            int kptId = Request.Form["KptId"].ParseInt();
            //SysRole role = CurrentUser.Role(3).Role();
            //ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            //ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            //ViewBag.canExport = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 4 && x.PermissionFlag).Count() > 0;
            TblKpt ob = db.TblKpt.Find(kptId);
            try
            {
                TblKptAttachment attach = new TblKptAttachment()
                {
                    TblKpt = ob,
                    AttachName = Request.Form["AttachName"].Convert2String()
                };

                if (Request.Files.Count > 0 && Request.Files["AttachFile"] != null && Request.Files["AttachFile"].ContentLength > 0)
                {
                    HttpPostedFileBase uploadedFile = Request.Files["AttachFile"];
                    string FilePath = string.Format("files/kpt/{0}{1}", CurrentDate.ParseString(DateFormat._yyyyMMddHHmmssfff), Path.GetExtension(uploadedFile.FileName));
                    if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                    if (!Directory.Exists(Server.MapPath("~/files/kpt"))) { Directory.CreateDirectory(Server.MapPath("~/files/kpt")); }
                    uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));
                    attach.AttachPath = FilePath;
                }
                else { throw new Exception("กรุณาอัพโหลดไฟล์แนบ"); }
                db.TblKptAttachment.Add(attach);
                db.SaveChanges();
                return RedirectToAction("Detail", "KPT", new { id = ob.KptId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
                //return ViewDetail(ob, "บันทึกข้อมูลเรียบร้อยแล้ว", AlertMsgType.Success);
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage();
                return RedirectToAction("Detail", "KPT", new { id = ob.KptId, msg, msgType = AlertMsgType.Danger });
            }
        }

        [HttpPost]
        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        public ActionResult SetAttachStation()
        {
            int Id = Request.Form["StationId"].ParseInt();
            //SysRole role = CurrentUser.Role(3).Role();
            //ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            //ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            //ViewBag.canExport = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 4 && x.PermissionFlag).Count() > 0;
            TblKptStation ob = db.TblKptStation.Include(x => x.TblKpt).Where(x => x.StationId == Id).FirstOrDefault();
            try
            {
                TblKptStationAttachment attach = new TblKptStationAttachment()
                {
                    TblKptStation = ob,
                    AttachName = Request.Form["AttachName"].Convert2String()
                };

                if (Request.Files.Count > 0 && Request.Files["AttachFile"] != null && Request.Files["AttachFile"].ContentLength > 0)
                {
                    HttpPostedFileBase uploadedFile = Request.Files["AttachFile"];
                    string FilePath = string.Format("files/kpt/{0}{1}", CurrentDate.ParseString(DateFormat._yyyyMMddHHmmssfff), Path.GetExtension(uploadedFile.FileName));
                    if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                    if (!Directory.Exists(Server.MapPath("~/files/kpt"))) { Directory.CreateDirectory(Server.MapPath("~/files/kpt")); }
                    uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));
                    attach.AttachPath = FilePath;
                    if (string.IsNullOrWhiteSpace(attach.AttachName))
                    { attach.AttachName = Path.GetFileNameWithoutExtension(uploadedFile.FileName); }
                }
                else { throw new Exception("กรุณาอัพโหลดไฟล์แนบ"); }
                db.TblKptStationAttachment.Add(attach);
                db.SaveChanges();
                return RedirectToAction("Station", "KPT", new { kId = ob.TblKpt.KptId, id = ob.StationId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
                //return ViewDetail(ob, "บันทึกข้อมูลเรียบร้อยแล้ว", AlertMsgType.Success);
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage();
                return RedirectToAction("Station", "KPT", new { kId = ob.TblKpt.KptId, id = ob.StationId, msg, msgType = AlertMsgType.Danger });
            }
        }

        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Station")]
        public ActionResult SetStation()
        {
            int Id = Request.Form["StationId"].ParseInt();
            int kptId = Request.Form["KptId"].ParseInt();
            TblKptStation ob = uow.db.TblKptStation.Find(Id) ?? new TblKptStation();
            if (ob.KptId <= 0)
            {
                ob.KptId = kptId;
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDateTime;
            }
            ob.StationName = Request.Form["StationName"];
            ob.StationTestBy = Request.Form["StationTestBy"];
            ob.StationRemark = Request.Form["StationRemark"];
            ob.StationOrder = Request.Form["StationOrder"].ParseInt();
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDateTime;
            try
            {
                //if (!ob.ValidateModel(out string errMsg))
                //{ throw new Exception(errMsg); }
                foreach (string key in Request.Form.AllKeys.Where(x => x.StartsWith("blowCount-")))
                {
                    int depth = key.Split('-')[1].ParseInt();
                    var dt = ob.StationId > 0 ?
                        uow.db.TblKptStationDetail
                            .Where(x => x.StationId == ob.StationId && x.StationDepth == depth)
                            .FirstOrDefault() ?? new TblKptStationDetail() { TblKptStation = ob, StationDepth = depth } :
                        new TblKptStationDetail() { TblKptStation = ob, StationDepth = depth };
                    dt.StationBlowCount = Request.Form[key].ParseInt();
                    if (dt.Id <= 0) { uow.db.TblKptStationDetail.Add(dt); }
                    else { uow.db.Entry(dt).State = EntityState.Modified; }
                }

                if (ob.StationId <= 0) { uow.db.TblKptStation.Add(ob); }
                else { uow.db.Entry(ob).State = EntityState.Modified; }
                uow.SaveChanges();

                return RedirectToAction("Detail", new
                {
                    id = ob.KptId,
                    controller = "KPT",
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                    msgType = AlertMsgType.Success
                });
            }
            catch (DbEntityValidationException ex)
            {
                string msg = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    msg += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += string.Format("{{\n}}- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return ViewStation(ob, msg, AlertMsgType.Danger);
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage();
                return ViewStation(ob, msg, AlertMsgType.Danger);
            }
        }

        [HttpPost]
        [BackendAuthorized]
        public ActionResult Delete(int id)
        {
            try
            {
                SysRole role = CurrentUser.Role(3).Role();
                bool canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
                if (!canDelete) { return RedirectToAction("Index", "KPT", new { msg = "ไม่มีสิทธิ์ในการลบข้อมูล", msgType = AlertMsgType.Danger }); }

                TblKpt ob = db.TblKpt.Find(id);
                if (ob == null)
                { return RedirectToAction("Index", "KPT", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                var subs = db.TblKptDetail.Where(x => x.KptId == id);
                db.TblKptDetail.RemoveRange(subs);
                var stations = db.TblKptStation.Where(x => x.KptId == id);
                var stationAttachs = db.TblKptStationAttachment.Where(x => stations.Select(y => y.StationId).Contains(x.StationId));
                List<string> imgsPath = stationAttachs.Select(x => x.AttachPath).ToList();
                var stationDetails = db.TblKptStationDetail.Where(x => stations.Select(y => y.StationId).Contains(x.StationId));
                db.TblKptStationDetail.RemoveRange(stationDetails);
                db.TblKptStationAttachment.RemoveRange(stationAttachs);
                db.TblKptStation.RemoveRange(stations);
                db.TblKpt.Remove(ob);
                db.SaveChanges();
                if (imgsPath.Count() > 0)
                {
                    foreach (string path in imgsPath)
                    {
                        string svPath = Server.MapPath("~/" + path);
                        if (FileIO.Exists(svPath))
                        { FileIO.Delete(svPath); }
                    }
                }
                return RedirectToAction("Index", "KPT", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", "KPT", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        [HttpPost]
        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStation(int kId)
        {
            try
            {
                SysRole role = CurrentUser.Role(3).Role();
                bool canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
                if (!canDelete) { return RedirectToAction("Detail", "KPT", new { msg = "ไม่มีสิทธิ์ในการลบข้อมูล", msgType = AlertMsgType.Danger, id = kId }); }

                int id = Request.Form["Id"].ParseInt();
                TblKptStation ob = db.TblKptStation.Find(id);
                if (ob == null)
                { return RedirectToAction("Detail", "KPT", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning, id = kId }); }

                var subs = db.TblKptStationDetail.Where(x => x.StationId == id);
                db.TblKptStationDetail.RemoveRange(subs);
                var stationAttachs = db.TblKptStationAttachment.Where(x => x.StationId == id);
                List<string> imgsPath = stationAttachs.Select(x => x.AttachPath).ToList();
                db.TblKptStationAttachment.RemoveRange(stationAttachs);
                db.TblKptStation.Remove(ob);
                db.SaveChanges();
                if (imgsPath.Count() > 0)
                {
                    foreach (string path in imgsPath)
                    {
                        string svPath = Server.MapPath("~/" + path);
                        if (FileIO.Exists(svPath))
                        { FileIO.Delete(svPath); }
                    }
                }
                return RedirectToAction("Detail", "KPT", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success, id = kId });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", "KPT", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger, id = kId }); }
        }

        [HttpPost]
        [BackendAuthorized]
        public ActionResult DeleteAttachStation(int sid)
        {
            TblKptStation st = db.TblKptStation.Find(sid);
            try
            {
                SysRole role = CurrentUser.Role(3).Role();
                bool canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
                if (!canDelete) { return RedirectToAction("Station", "KPT", new { msg = "ไม่มีสิทธิ์ในการลบข้อมูล", msgType = AlertMsgType.Danger, id = st.StationId, kId = st.KptId }); }

                int id = Request.Form["Id"].ParseInt();
                TblKptStationAttachment ob = db.TblKptStationAttachment.Find(id);
                if (ob == null)
                { return RedirectToAction("Station", "KPT", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning, id = st.StationId, kId = st.KptId }); }

                string filePath = ob.AttachPath;
                db.TblKptStationAttachment.Remove(ob);
                db.SaveChanges();
                if (FileIO.Exists(Server.MapPath("~/" + filePath)))
                { FileIO.Delete(Server.MapPath("~/" + filePath)); }
                return RedirectToAction("Station", "KPT", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success, id = st.StationId, kId = st.KptId });
            }
            catch (Exception ex)
            { return RedirectToAction("Station", "KPT", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger, id = st.StationId, kId = st.KptId }); }
        }

        #region Private Action
        private ActionResult ViewDetail(TblKpt ob, string msg, AlertMsgType? msgType)
        {
            try
            {
                if (ob == null)
                { throw new Exception("ไม่พบข้อมูลที่ต้องการ, กรุณาลองใหม่อีกครั้ง"); }

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                ViewData["GoogleMapsAPIKey"] = ConfigurationManager.AppSettings["GoogleMapsAPI"];
                return View("Detail", ob);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new
                {
                    area = "",
                    controller = "KPT",
                    msg = ex.GetMessage(),
                    msgType = AlertMsgType.Danger
                });
            }
        }
        private ActionResult ViewStation(TblKptStation ob, string msg, AlertMsgType? msgType)
        {
            try
            {
                if (ob == null)
                { throw new Exception("ไม่พบข้อมูลที่ต้องการ, กรุณาลองใหม่อีกครั้ง"); }

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                return View("Station", ob);
            }
            catch (Exception ex)
            {
                if (ob == null)
                {
                    return RedirectToAction("Index", new
                    {
                        controller = "KPT",
                        msg = ex.GetMessage(),
                        msgType = AlertMsgType.Danger
                    });
                }
                return RedirectToAction("Detail", new
                {
                    id = ob.KptId,
                    controller = "KPT",
                    msg = ex.GetMessage(),
                    msgType = AlertMsgType.Danger
                });
            }
        }
        #endregion
    }
}
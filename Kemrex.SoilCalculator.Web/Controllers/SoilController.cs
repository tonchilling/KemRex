using DMN.Standard.Common.Extensions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Kemrex.Database;
using Kemrex.FWCommon;
using Kemrex.SoilCalculator.Web.ActionFilters;
using Kemrex.SoilCalculator.Web.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IOFile = System.IO.File;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class SoilController : BaseController
    {
        public UnitOfWork uow;
        public int CurrentUID => (Session["sid"].Convert2String().ParseIntNullable() ?? 0);
        public SysAccount CurrentUser => uow.Account.Get(CurrentUID);
        public DateTime CurrentDateTime = DateTime.Now;
        public SoilController()
        {
            uow = new UnitOfWork();
        }

        [BackendAuthorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType)
        {
            SysRole role = CurrentUser.Role(3).Role();
            ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3210 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 3210 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            ViewBag.canExport = role.SysRolePermission.Where(x => x.MenuId == 3210 && x.PermissionId == 4 && x.PermissionFlag).Count() > 0;
            List<TblCalLoad> lst = new List<TblCalLoad>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                var data = db.TblCalLoad
                    .Where(x => x.CreatedBy == CurrentUID);
                int total = data.Count();
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Soil", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>(),
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = (from d in data
                       join m in db.TblPile on d.ModelId equals m.PileId
                       orderby
                        m.PileName ascending
                        , d.InputC ascending
                       select d)
                    .Skip((Pagination.Page - 1) * Pagination.Size)
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
        public ActionResult Calculator()
        {
            return View();
        }

        [BackendAuthorized]
        [HttpGet, ActionName("Calculated")]
        public ActionResult GetCalculated()
        {
            return RedirectToAction("Calculator", new { controller = "Soil", area = "" });
        }

        [BackendAuthorized]
        public ActionResult Export(int id)
        {
            try
            {
                TblCalLoad ob = db.TblCalLoad
                    .Where(x => x.CalId == id)
                    .FirstOrDefault();
                if (ob == null)
                { throw new Exception("Data not found."); }
                SoilCalculatedModel md = new SoilCalculatedModel(uow)
                {
                    inputC = ob.InputC,
                    inputDegree = ob.InputDegree,
                    ModelId = ob.ModelId,
                    inputSafeload = ob.InputSafeLoad
                };
                ReportDataSource rptData = new ReportDataSource("db", new List<SysParameter>());
                ReportViewer rptViewer = new ReportViewer();
                rptViewer.ProcessingMode = ProcessingMode.Local;
                rptViewer.LocalReport.ReportPath = Server.MapPath("~/reports/rptLoadCalculator.rdlc");
                rptViewer.LocalReport.EnableExternalImages = true;
                string imagePath = new Uri(Server.MapPath("~/" + ob.PileModel().TblPileSeries.SeriesImage)).AbsoluteUri;
                rptViewer.LocalReport.SetParameters(new ReportParameter("imagePile", imagePath));
                rptViewer.LocalReport.SetParameters(new ReportParameter("pileWidth", (md.ModelInfo.PileDia * 1000).ToString("#,##0")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("pileLength", md.ModelInfo.PileLength.ToString("#,##0")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("inputC", md.inputC.ToString()));
                rptViewer.LocalReport.SetParameters(new ReportParameter("inputDegree", md.inputDegree.ToString()));
                rptViewer.LocalReport.SetParameters(new ReportParameter("inputModel", md.ModelInfo.PileName));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataSu", md.inputC.ToString()));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataDia", md.ClayDia.ToString("#,##0.000")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataAb", md.ClayAb.ToString("#,##0.00000")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataQb", md.Qu.ToString("#,##0.00")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataShearH", md.ShearH.ToString()));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataShearArea", md.ShearArea.ToString("#,##0.000")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataShearResist", md.ShearResist.ToString("#,##0.00")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataQComp", md.UltCompressForce.ToString("#,##0")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataSafeLoadComp", md.Qcomp.ToString("#,##0.00")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataHCone", md.UpliftH.ToString("#,##0.000")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataWeightOfSoil", md.WeightOfSoil.ToString("#,##0")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataQUplift", md.UltUpliftForce.ToString("#,##0")));
                rptViewer.LocalReport.SetParameters(new ReportParameter("dataSafeLoadUplift", md.Quplift.ToString("#,##0.00")));
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

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename= LoadCalc-" + CurrentDate.ToString("yyyy-MM-dd_HHmmss") + "." + filenameExtension);
                Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
                Response.Flush(); // send it to the client to download  
                Response.End();
                return View();
            }
            catch (Exception ex)
            { return RedirectToAction("Index", "KPT", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        [BackendAuthorized]
        public ActionResult ExportCal(int id)
        {
            try
            {
                TblCalLoad ob = db.TblCalLoad.Find(id);
                if (ob == null)
                { throw new Exception("Data not found."); }

                SoilCalculatedModel md = new SoilCalculatedModel(uow)
                {
                    inputC = ob.InputC,
                    inputDegree = ob.InputDegree,
                    ModelId = ob.ModelId,
                    inputSafeload = ob.InputSafeLoad
                };

                string sourceFile = Server.MapPath("~/files/KEMREXCalculated.xlsx");
                byte[] byteArray = IOFile.ReadAllBytes(sourceFile);
                using (MemoryStream stream = new MemoryStream())
                {
                    stream.Write(byteArray, 0, (int)byteArray.Length);
                    using (SpreadsheetDocument spreadsheetDoc = SpreadsheetDocument.Open(stream, true))
                    {
                        IEnumerable<Sheet> sheets = spreadsheetDoc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().Where(s => s.Name == "Calculated");
                        if (sheets.Count() <= 0) { throw new Exception("Cannot find 'Calculated' sheet in source file."); }

                        string relationshipId = sheets.First().Id.Value;
                        WorksheetPart worksheetPart = (WorksheetPart)spreadsheetDoc.WorkbookPart.GetPartById(relationshipId);
                        if (worksheetPart == null) { throw new Exception("Cannot load 'Calculated' sheet."); }

                        Worksheet sheet = worksheetPart.Worksheet;
                        UpdateCell(ref sheet, "E", 5, md.inputC.ToString());
                        UpdateCell(ref sheet, "E", 6, md.inputDegree.ToString());
                        UpdateCell(ref sheet, "E", 7, md.ModelInfo.PileName);
                        UpdateCell(ref sheet, "E", 11, md.inputC.ToString());
                        UpdateCell(ref sheet, "E", 12, md.ModelInfo.PileDia.ToString("#,##0.000"));
                        UpdateCell(ref sheet, "E", 13, md.ClayAb.ToString("#,##0.00000"));
                        UpdateCell(ref sheet, "E", 15, md.Qu.ToString("#,##0.00"));
                        UpdateCell(ref sheet, "E", 18, md.ShearH.ToString("#,##0"));
                        UpdateCell(ref sheet, "E", 19, md.ShearArea.ToString("#,##0.000"));
                        UpdateCell(ref sheet, "E", 21, md.ShearResist.ToString("#,##0.00"));
                        UpdateCell(ref sheet, "E", 24, md.UltCompressForce.ToString("#,##0"));
                        UpdateCell(ref sheet, "E", 26, md.ShearResist.ToString("#,##0.00"));
                        UpdateCell(ref sheet, "E", 31, md.UpliftH.ToString("#,##0.000"));
                        UpdateCell(ref sheet, "E", 32, md.UpliftHcone.ToString("#,##0.000"));
                        UpdateCell(ref sheet, "E", 33, md.WeightOfSoil.ToString("#,##0"));
                        UpdateCell(ref sheet, "E", 34, md.ShearResist.ToString("#,##0.00"));
                        UpdateCell(ref sheet, "E", 36, md.UltUpliftForce.ToString("#,##0"));
                        UpdateCell(ref sheet, "E", 37, md.Quplift.ToString("#,##0.00"));
                    }
                    //IOFile.WriteAllBytes("C:\\temp\\newName.xlsx", stream.ToArray());
                    string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename= KEMREXCalculated-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");
                    Response.BinaryWrite(stream.ToArray());
                    Response.Flush(); // send it to the client to download  
                    Response.End();
                }
                return View();
            }
            catch (Exception ex)
            { return RedirectToAction("Index", "Soil", new { msg = "Something wrong. [" + ex.GetMessage() + "]", msgType = AlertMsgType.Danger }); }
        }

        [HttpPost]
        [BackendAuthorized]
        public ActionResult Calculated()
        {
            SysRole role = CurrentUser.Role(3).Role();
            ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3210 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            int CalId = Request.Form["CalId"].ParseInt();
            decimal cValue = Request.Form["inputC"].ParseDecimal();
            int degreeValue = Request.Form["inputDegree"].ParseInt();
            int modelId = Request.Form["inputModel"].ParseInt();
            decimal safeLoad = Request.Form["inputSafeLoad"].ParseDecimal();
            string name = Request.Form["inputProject"];
            string remark = Request.Form["inputRemark"];
            SoilCalculatedModel md = new SoilCalculatedModel(uow)
            {
                calId = CalId,
                inputC = cValue,
                inputDegree = degreeValue,
                ModelId = modelId,
                inputSafeload = safeLoad,
                ProjectName = name,
                CalRemark = remark
            };
            if (Request.Form["isExported"].ParseBoolean())
            {
                string sourceFile = Server.MapPath("~/files/KEMREXCalculated.xlsx");
                byte[] byteArray = IOFile.ReadAllBytes(sourceFile);
                using (MemoryStream stream = new MemoryStream())
                {
                    stream.Write(byteArray, 0, (int)byteArray.Length);
                    using (SpreadsheetDocument spreadsheetDoc = SpreadsheetDocument.Open(stream, true))
                    {
                        IEnumerable<Sheet> sheets = spreadsheetDoc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().Where(s => s.Name == "Calculated");
                        if (sheets.Count() <= 0) { throw new Exception("Cannot find 'Calculated' sheet in source file."); }

                        string relationshipId = sheets.First().Id.Value;
                        WorksheetPart worksheetPart = (WorksheetPart)spreadsheetDoc.WorkbookPart.GetPartById(relationshipId);
                        if (worksheetPart == null) { throw new Exception("Cannot load 'Calculated' sheet."); }

                        Worksheet sheet = worksheetPart.Worksheet;
                        UpdateCell(ref sheet, "E", 5, md.inputC.ToString());
                        UpdateCell(ref sheet, "E", 6, md.inputDegree.ToString());
                        UpdateCell(ref sheet, "E", 7, md.ModelInfo.PileName);
                        UpdateCell(ref sheet, "E", 11, md.inputC.ToString());
                        UpdateCell(ref sheet, "E", 12, md.ModelInfo.PileDia.ToString("#,##0.000"));
                        UpdateCell(ref sheet, "E", 13, md.ClayAb.ToString("#,##0.00000"));
                        UpdateCell(ref sheet, "E", 15, md.Qu.ToString("#,##0.00"));
                        UpdateCell(ref sheet, "E", 18, md.ShearH.ToString("#,##0"));
                        UpdateCell(ref sheet, "E", 19, md.ShearArea.ToString("#,##0.000"));
                        UpdateCell(ref sheet, "E", 21, md.ShearResist.ToString("#,##0.00"));
                        UpdateCell(ref sheet, "E", 24, md.UltCompressForce.ToString("#,##0"));
                        UpdateCell(ref sheet, "E", 26, md.ShearResist.ToString("#,##0.00"));
                        UpdateCell(ref sheet, "E", 31, md.UpliftH.ToString("#,##0.000"));
                        UpdateCell(ref sheet, "E", 32, md.UpliftHcone.ToString("#,##0.000"));
                        UpdateCell(ref sheet, "E", 33, md.WeightOfSoil.ToString("#,##0"));
                        UpdateCell(ref sheet, "E", 34, md.ShearResist.ToString("#,##0.00"));
                        UpdateCell(ref sheet, "E", 36, md.UltUpliftForce.ToString("#,##0"));
                        UpdateCell(ref sheet, "E", 37, md.Quplift.ToString("#,##0.00"));
                    }
                    //IOFile.WriteAllBytes("C:\\temp\\newName.xlsx", stream.ToArray());
                    string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename= KEMREXCalculated-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");
                    Response.BinaryWrite(stream.ToArray());
                    Response.Flush(); // send it to the client to download  
                    Response.End();
                }
            }
            return View(md);
        }

        [HttpPost]
        [BackendAuthorized]
        public ActionResult SetCalculated()
        {
            try
            {
                int calId = Request.Form["CalId"].ParseInt();
                decimal inputC = Request.Form["inputC"].ParseDecimal();
                int inputDegree = Request.Form["inputDegree"].ParseInt();
                int modelId = Request.Form["inputModel"].ParseInt();
                decimal inputSafeLoad = Request.Form["inputSafeLoad"].ParseDecimal();
                TblCalLoad ob = db.TblCalLoad.Find(calId) ?? new TblCalLoad()
                {
                    CreatedBy = CurrentUID,
                    CreatedDate = CurrentDate
                };

                ob.InputC = inputC;
                ob.InputDegree = inputDegree;
                ob.ModelId = modelId;
                ob.InputSafeLoad = inputSafeLoad;
                ob.ProjectName = Request.Form["ProjectName"];
                ob.CalRemark = Request.Form["CalRemark"];
                ob.UpdatedBy = CurrentUID;
                ob.UpdatedDate = CurrentDate;
                if (ob.CalId <= 0) { db.TblCalLoad.Add(ob); }
                else { db.Entry(ob).State = EntityState.Modified; }
                db.SaveChanges();
                { return RedirectToAction("Index", "Soil", new { msg = "Data saved.", msgType = AlertMsgType.Success }); }
            }
            catch (Exception ex)
            { return RedirectToAction("Index", "Soil", new { msg = "Something wrong. [" + ex.GetMessage() + "]", msgType = AlertMsgType.Danger }); }
        }

        [HttpPost]
        [BackendAuthorized]
        public ActionResult Delete(int id)
        {
            try
            {
                SysRole role = CurrentUser.Role(3).Role();
                bool canDelete = role.SysRolePermission.Where(x => x.MenuId == 3220 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
                if (!canDelete) { return RedirectToAction("Index", "Soil", new { msg = "ไม่มีสิทธิ์ในการลบข้อมูล", msgType = AlertMsgType.Danger }); }

                TblCalLoad ob = db.TblCalLoad.Find(id);
                if (ob == null)
                { return RedirectToAction("Index", "Soil", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                db.TblCalLoad.Remove(ob);
                db.SaveChanges();
                return RedirectToAction("Index", "Soil", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", "Soil", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        [BackendAuthorized]
        public ActionResult KPT()
        {
            return View();
        }

        private void UpdateCell(ref Worksheet worksheet, string columnName, uint rowIndex, string text)
        {
            Cell cell = GetCell(ref worksheet, columnName, rowIndex);
            cell.CellValue = new CellValue(text);
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
        }

        private static Cell GetCell(ref Worksheet worksheet,
                  string columnName, uint rowIndex)
        {
            Row row = GetRow(ref worksheet, rowIndex);

            if (row == null)
                return null;

            return row.Elements<Cell>().Where(c => string.Compare
                   (c.CellReference.Value, columnName +
                   rowIndex, true) == 0).First();
        }
        
        // Given a worksheet and a row index, return the row.
        private static Row GetRow(ref Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().
              Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        }
    }
}
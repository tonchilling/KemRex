using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using System.Linq;
using IOFile = System.IO.File;

namespace Kemrex.Web.Main.Controllers
{
    public class JobOrderController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
                   string src = "")
        {
            List<TblJobOrder> lst = new List<TblJobOrder>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
              /*  int total = uow.Modules.JobOrder.Count(src,0 );
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "JobOrder", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>()
                    {
                        { "src", src }
                    },
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = uow.Modules.JobOrder.Gets(Pagination.Page, Pagination.Size, src, 0).ToList();*/
            }
            catch (Exception ex)
            {
                WidgetAlertModel Alert = new WidgetAlertModel()
                {
                    Type = AlertMsgType.Danger,
                    Message = ex.GetMessage(true)
                };
                ViewBag.Alert = Alert;
            }
            return View(lst);
        }


        [HttpGet]
        public JsonResult GetSaleOrderNotInJobOrder(string startDate, string toDate)
        {

            //  List<TblSaleOrder> saleOrderList = uow.Modules.SaleOrder.GetFindByCondition(startDate, toDate,"2");
            DateTime? StDate=null;
            DateTime? ToDate = null;

            if (startDate != "")
            {
                StDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                ToDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));


            }
                List<TblSaleOrder> saleOrderList = uow.Modules.SaleOrder.GetSaleOrderInInvoice(StDate, ToDate);
            



            return Json(saleOrderList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetList()
        {
            string formateDate = "yyyy-MM-dd";
            //  DateTime searchOrderDate = Converting.StringToDate(saleOrderDate, formateDate);
            List<TblJobOrder> ob = uow.Modules.JobOrder.GetHeaderForEngineer();



            return Json(ob);
        }

        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); ;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US"); ;

            TblJobOrder ob = uow.Modules.JobOrder.Get(id ?? 0);

            if (ob != null && ob.StatusId == null)
            {
                ob.StatusId = 0;
            }
            ViewData["optEmployee"] = uow.Modules.Employee.Gets();
            ViewData["userAccount"] = CurrentUser;
            ViewData["SurveyTemplate"] = uow.Modules.Survey.GetList();
            return ViewDetail(ob, msg, msgType);
        }


        private string getJobId()
        {
            string qid = "J";
            System.Globalization.CultureInfo _cultureTHInfo = new System.Globalization.CultureInfo("en-US");
            var dt = DateTime.Now.ToString("yyMMdd", _cultureTHInfo);
            string Id = uow.Modules.JobOrder.GetLastId(qid+ dt);
            
          
            string preid = Id.Split('-')[0];
            int runid = int.Parse(Id.Split('-')[1]);

            if (preid.Contains(dt))
            {
                runid++;
            }
            else
            {
                runid = 1;
            }
            qid += dt + "-" + runid.ToString("D3");
            return qid;
        }



        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail(TblJobOrder jobOrder)
        {
            System.Globalization.CultureInfo _cultureTHInfo = new System.Globalization.CultureInfo("en-US");
            List<TblJobOrderProjectType> projectType = null;
            List<TblJobOrderEquipmentType> EquipmentType = null;
            List<TblJobOrderLandType> LandType = null;
            List<TblJobOrderUndergroundType> UndergroundType = null;
            List<TblJobOrderObstructionType> ObstructionType = null;
            List<TblJobOrderAttachmentType> AttachmentType = null;
            List<TblJobOrderSurveyDetail> SurveyDetailType = null;

            int approveStatus = Request.Form["hdApprove"] != null ? Request.Form["hdApprove"].ParseInt() : 0;

            string JobOrderId = this.Request.Form["JobOrderId"];

            string StartHH = this.Request.Form["StartHH"];
            string StartMM = this.Request.Form["StartMM"];
            string EndHH = this.Request.Form["EndHH"];
            string EndMM = this.Request.Form["EndMM"];
            string SaleName = this.Request.Form["SaleName"];
            string SaleMobile = this.Request.Form["SaleMobile"];
            string hddProject = this.Request.Form["hddProject"];
            string hddEquipmentType = this.Request.Form["hddEquipmentType"];
            string hddLandType = this.Request.Form["hddLandType"];
            string hddUndergroundType = this.Request.Form["hddUndergroundType"];
            string hddObstructionType = this.Request.Form["hddObstructionType"];
            string hddAttachmentType = this.Request.Form["hddAttachmentType"];
            string hddSurveyType = this.Request.Form["hddSurveyType"];
            string hddSurveyComment = this.Request.Form["hddSurveyComment"];
            jobOrder.ProductId= this.Request.Form["ProductId"].ParseInt();
            jobOrder.StartWorkingTime = StartHH + ":" + StartMM;
            jobOrder.EndWorkingTime = EndHH + ":" + EndMM;
            if (JobOrderId == "" || JobOrderId == "0")
            {
                jobOrder.JobOrderNo = getJobId();
                jobOrder.CreatedDate = DateTime.Now;
                jobOrder.CreatedBy = Convert.ToInt32(CurrentUID);
            }
            else {
                jobOrder.UpdatedDate = DateTime.Now;
                jobOrder.UpdatedBy = Convert.ToInt32(CurrentUID);
            }


            if (approveStatus == 3)
            {
                jobOrder.ClosedBy = Convert.ToInt32(CurrentUID);
            }

            if (Request.Form["StartDate"].ToString() !="")
            {
                var dd = Request.Form["StartDate"]+ " 00:00:00";

                jobOrder.StartDate = dd.ParseDate(DateFormat.ddMMyyyyHHmmss,culInfo: _cultureTHInfo);
            }

            if (Request.Form["EndDate"].ToString() != "")
            {
                var dd = Request.Form["EndDate"].Split(' ')[0] + " 00:00:00";

                jobOrder.EndDate = dd.ParseDate(DateFormat.ddMMyyyyHHmmss, culInfo: _cultureTHInfo);
            }

            if (Request.Form["SurveyDate"].ToString() != "")
            {
                var dd = Request.Form["SurveyDate"].Split(' ')[0] + " 00:00:00";

                jobOrder.SurveyDate = dd.ParseDate(DateFormat.ddMMyyyyHHmmss, culInfo: _cultureTHInfo);
            }



            /*# hddProject
            # hddEquipmentType
            # hddLandType
            # hddUndergroundType
            # hddObstructionType
            # hddAttachmentType
                        */


            try
            {

              //  uow.Modules.JobOrder.Set(jobOrder);
              //  uow.SaveChanges();



                #region Project Type
                projectType = new List<TblJobOrderProjectType>();

                if (hddProject != null && hddProject.Length > 0)
                {
                    foreach (string id in hddProject.Split(','))
                    {
                        projectType.Add(new TblJobOrderProjectType() { JobOrderId = jobOrder.JobOrderId, ProjectTypeId = id.ParseInt() });
                    }
                    jobOrder.ProjectType = projectType;

                }
                #endregion

                #region EquipmentType 
                EquipmentType = new List<TblJobOrderEquipmentType>();

                if (hddEquipmentType != null && hddEquipmentType.Length > 0)
                {
                    foreach (string id in hddEquipmentType.Split(','))
                    {
                        EquipmentType.Add(new TblJobOrderEquipmentType() { JobOrderId = jobOrder.JobOrderId, EquipmentTypeId = id.ParseInt() });
                    }
                    jobOrder.EquipmentType = EquipmentType;

                }
                #endregion

                #region LandType  
                LandType = new List<TblJobOrderLandType>();

                if (hddLandType != null && hddLandType.Length > 0)
                {
                    foreach (string id in hddLandType.Split(','))
                    {
                        LandType.Add(new TblJobOrderLandType() { JobOrderId = jobOrder.JobOrderId, LandTypeId = id.ParseInt() });
                    }
                    jobOrder.LandType = LandType;

                }
                #endregion

                #region UndergroundType  
                UndergroundType = new List<TblJobOrderUndergroundType>();

                if (hddUndergroundType != null && hddUndergroundType.Length > 0)
                {
                    foreach (string id in hddUndergroundType.Split(','))
                    {
                        UndergroundType.Add(new TblJobOrderUndergroundType() { JobOrderId = jobOrder.JobOrderId, UndergroundTypeId = id.ParseInt() });
                    }
                    jobOrder.UndergroundType = UndergroundType;

                }
                #endregion

                #region ObstructionType  
                ObstructionType = new List<TblJobOrderObstructionType>();

                if (hddObstructionType != null && hddObstructionType.Length > 0)
                {
                    foreach (string id in hddObstructionType.Split(','))
                    {
                        ObstructionType.Add(new TblJobOrderObstructionType() { JobOrderId = jobOrder.JobOrderId, ObstructionTypeId = id.ParseInt() });
                    }
                    jobOrder.ObstructionType = ObstructionType;

                }
                #endregion


                #region TblJobOrderAttachmentType  
                AttachmentType = new List<TblJobOrderAttachmentType>();

                if (hddAttachmentType != null && hddAttachmentType.Length > 0)
                {
                    foreach (string id in hddAttachmentType.Split(','))
                    {
                        AttachmentType.Add(new TblJobOrderAttachmentType() { JobOrderId = jobOrder.JobOrderId, AttachmentTypeId = id.ParseInt() });
                    }
                    jobOrder.AttachmentType = AttachmentType;

                }
                #endregion

                #region SurveyDetail Type

               // selSurveyComment
                SurveyDetailType = new List<TblJobOrderSurveyDetail>();

                if (hddSurveyType != null && hddSurveyType.Length > 0)
                {
                    foreach (string id in hddSurveyType.Split(','))
                    {
                        string comment = hddSurveyComment.Split(',').Where(o => o.IndexOf(id) > -1).Count() >0? hddSurveyComment.Split(',').Where(o => o.IndexOf(id) > -1).First():"";
                        if (comment.Length > 1 && comment.IndexOf(":") > -1)
                        {
                            comment = comment.Split(':')[1];
                        }
                        SurveyDetailType.Add(new TblJobOrderSurveyDetail() { JobOrderId = jobOrder.JobOrderId, SurveyDetailId = id ,Desc= comment });
                    }
                    jobOrder.SurveyDetail = SurveyDetailType;

                }
                #endregion


                uow.Modules.JobOrder.Set(jobOrder);

                uow.SaveChanges();

                return RedirectToAction("Detail", MVCController, new { id = jobOrder.JobOrderId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(jobOrder, msg, AlertMsgType.Danger);
            }




            return RedirectToAction("Detail", MVCController, new { id = 1, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        public ActionResult AddProduct()
        {
            int sid = Request.Form["JobOrderId"]!=null? Request.Form["JobOrderId"].Split(',')[0].ParseInt():0;
            var id = Request.Form["selProduct"].Split(':');  //  ProductId:PriceNet
            int qty = Request.Form["Quantity"].ParseInt();
           

            if (id.Count() > 0)
            {
                var detail = uow.Modules.JobOrder.GetDetails(sid);
                int pid = int.Parse(id[0]);

                TblJobOrderDetail ob = uow.Modules.JobOrder.GetDetail(0);

                ob.No = detail != null ? detail.Count + 1 : 1;
                ob.JobOrderId = sid;
                ob.ProductId = pid;
                ob.Quantity = qty;


                uow.Modules.JobOrder.SetDetail(ob);
                uow.SaveChanges();


            }
            return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Product", msg = "บันทึกข้อมูลรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        public ActionResult AddProperty()
        {
            int sid = Request.Form["JobOrderId"] != null ? Request.Form["JobOrderId"].Split(',')[0].ParseInt() : 0;
            var id = Request.Form["selProductP"].Split(':');  //  ProductId:PriceNet
            int qty = Request.Form["QuantityP"].ParseInt();


            if (id.Count() > 0)
            {
                var detail = uow.Modules.JobOrder.GetProperties(sid);
                int pid = int.Parse(id[0]);

                TblJobOrderProperties ob = uow.Modules.JobOrder.GetPropertie(0);

                ob.No = detail != null ? detail.Count + 1 : 1;
                ob.JobOrderId = sid;
                ob.ProductId = pid;
                ob.Quantity = qty;


                uow.Modules.JobOrder.SetProperty(ob);
                uow.SaveChanges();


            }
            return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Product", msg = "บันทึกข้อมูลอุปกรณ์เรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        [HttpPost]
        //[Authorized]
        public ActionResult Delete()
        {
            return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning });
        }
        #region Private Action
        private ActionResult ViewDetail(TblJobOrder ob, string msg, AlertMsgType? msgType)
        {
            try
            {
                TblSaleOrder saleOrder = null;
                TblCustomer customer = null;
                if (ob == null)
                { throw new Exception("ไม่พบข้อมูลที่ต้องการ, กรุณาลองใหม่อีกครั้ง"); }

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }


                ViewData["JobOrderDetail"] = uow.Modules.JobOrder.Gets();
                ViewData["SysCategoryDetail"] = uow.Modules.SysCategory.Gets();
                ViewData["SaleorderDetail"] = uow.Modules.SaleOrder.Gets();
                ViewData["Saleorder"] =  saleOrder = uow.Modules.SaleOrder.Get(ob.SaleOrderId.HasValue? ob.SaleOrderId.Value:-1);
                ViewData["TeamOperation"] = uow.Modules.TeamOperation.Gets();
                ViewData["optCustomer"] = customer = uow.Modules.Customer.Get(saleOrder.CustomerId.HasValue ? saleOrder.CustomerId.Value : -1);
                ViewData["optProduct"] = uow.Modules.Product.Gets().ToList();
                ViewData["optAttachment"] = uow.Modules.SaleOrderAttachment.Gets(ob.SaleOrderId.HasValue ? ob.SaleOrderId.Value : -1);
                //    ViewData["optCustomerAddress"] = uow.Modules.CustomerAddress.Get(customer.addHasValue ? saleOrder.CustomerId.Value : -1); 
                return View(ob);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", MVCController, new
                {
                    area = MVCArea,
                    msg = ex.GetMessage(),
                    msgType = AlertMsgType.Danger
                });
            }
        }

        [HttpPost]
        //[Authorized]
        public ActionResult DetailDelete()
        {
            int qid = Request.Form["JobOrderId"].ParseInt();
            try
            {
                int Seq = Request.Form["No"].ParseInt();
                TblJobOrderDetail ob = uow.Modules.JobOrder.GetDetail(qid, Seq);
                if (ob == null)
                { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.JobOrder.DeleteDetail(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }



        [HttpPost]
        //[Authorized]
        public ActionResult PropertiesDelete()
        {
            int qid = Request.Form["JobOrderId"].ParseInt();
            try
            {
                int Seq = Request.Form["No"].ParseInt();
                TblJobOrderProperties ob = uow.Modules.JobOrder.GetPropertie(qid, Seq);
                if (ob == null)
                { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.JobOrder.DeletePropertie(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }


        // This action handles the form POST and the upload
        /*   [HttpPost]
         public ActionResult UploadFile()
          {
              // Verify that the user selected a file
              string sid = Request.Form["soId"];
              string FilePath = "";
              try
              {
                  if (Request.Files.Count > 0 && Request.Files["FileAttachment"] != null && Request.Files["FileAttachment"].ContentLength > 0)
                  {

                      TblJobOrderAttachment sa = uow.Modules.JobOrderAttachment.Get(0);

                      HttpPostedFileBase uploadedFile = Request.Files["FileAttachment"];
                      FilePath = string.Format("files/so/{0}", sid);
                      if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                      if (!Directory.Exists(Server.MapPath("~/files/so"))) { Directory.CreateDirectory(Server.MapPath("~/files/so")); }
                      if (!Directory.Exists(Server.MapPath("~/" + FilePath))) { Directory.CreateDirectory(Server.MapPath(FilePath)); }
                      FilePath += "/" + Path.GetFileName(uploadedFile.FileName);
                      sa.AttachmentPath = FilePath;
                      sa.JobOrderId = int.Parse(sid);
                      sa.AttachmentRemark = Path.GetExtension(uploadedFile.FileName);
                      sa.AttachmentOrder = uow.Modules.JobOrderAttachment.GetLastOrder(int.Parse(sid)) + 1;
                      uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));

                      uow.Modules.JobOrderAttachment.Set(sa);
                      uow.SaveChanges();
                  }
                  else
                  {
                      string msg = "ไม่พบไฟล์แนบ";
                      return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Attachment", fi = FilePath, msg, msgType = AlertMsgType.Danger });
                  }
              }
              catch (Exception ex)
              {
                  string msg = ex.GetMessage(true);
                  return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Attachment", fi = FilePath, msg, msgType = AlertMsgType.Danger });
              }
              // redirect back to the index action to show the form once again
              return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Attachment", msg = "", msgType = AlertMsgType.Success });
          }
         
        [HttpPost]
        public ActionResult DeleteFile()
        {
            string sid = Request.Form["soId"];
            string atid = Request.Form["atId"];
            string atname = Request.Form["atName"];

            // Delete phisical file
            if (!string.IsNullOrWhiteSpace(atname) && IOFile.Exists(Server.MapPath("~/" + atname)))
            { IOFile.Delete(Server.MapPath("~/" + atname)); }
            // Delete database
            uow.Modules.JobOrderAttachment.Delete(int.Parse(atid));
            uow.SaveChanges();

            return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Attachment", msg = "", msgType = AlertMsgType.Success });
        }
         */
        #endregion
    }
}
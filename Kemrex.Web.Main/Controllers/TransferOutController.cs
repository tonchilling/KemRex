using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class TransferOutController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
                 string src = "")
        {
            List<TransferHeader> lst = new List<TransferHeader>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Transfer.Count(0,"O");
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "TransferOut", "")
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
                lst = uow.Modules.Transfer.Gets(Pagination.Page, Pagination.Size, src, "O");
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


        private string getId(string type)
        {
            string qid = "T" + type;
            System.Globalization.CultureInfo _cultureTHInfo = new System.Globalization.CultureInfo("en-US");
            var dt = DateTime.Now.ToString("yyMMdd", _cultureTHInfo);
            string Id = uow.Modules.Transfer.GetLastId(qid + dt, type);

            if (Id == null)
            {
                Id = string.Format("{0}{1}-0000", qid, dt);
            }

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
            qid = qid+ dt + "-" + runid.ToString("D3");
            return qid;
        }


        [HttpPost, ActionName("SetDetail")]
        public ActionResult SetDetail(TransferHeader obj)
        {
            System.Globalization.CultureInfo _cultureTHInfo = new System.Globalization.CultureInfo("en-US");


            string TransferId = this.Request.Form["TransferId"];

            /*  string StartHH = this.Request.Form["StartHH"];
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
          */
            if (TransferId == "" || TransferId == "0")
            {
                obj.TransferNo = getId("O");
                obj.CreatedDate = DateTime.Now;
            }
            else
            {
                obj.TransferId = Convert.ToInt32(TransferId);
                obj.UpdatedDate = DateTime.Now;
            }

            obj.TransferType = "O";
            if (Request.Form["TransferDate"].ToString() != "")
            {
                var dd = Request.Form["TransferDate"];

                obj.TransferDate = dd.ParseDate(DateFormat.ddMMyyyy, culInfo: _cultureTHInfo);
            }

         //   obj.EmpId = "1";



            /*# hddProject
            # hddEquipmentType
            # hddLandType
            # hddUndergroundType
            # hddObstructionType
            # hddAttachmentType
                        */


            try
            {

                uow.Modules.Transfer.Set(obj);
                uow.SaveChanges();




                //  uow.Modules.Transfer.Set(obj);

                //  uow.SaveChanges();


            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(obj, msg, AlertMsgType.Danger);
            }

            return RedirectToAction("Detail", MVCController, new { id = obj.TransferId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });


            //  return RedirectToAction("Detail", MVCController, new { id = 1, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TransferHeader ob = uow.Modules.Transfer.Get(id ?? 0);

            return ViewDetail(ob, msg, msgType);
        }

        private ActionResult ViewDetail(TransferHeader ob, string msg, AlertMsgType? msgType)
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


                ViewData["optCustomer"] = uow.Modules.Customer.GetAllAddress();
                ViewData["optCustomerAddress"] = uow.Modules.CustomerAddress.Gets();
                ViewData["optProduct"] = uow.Modules.Product.Gets();
                ViewData["optContact"] = uow.Modules.CustomerContact.Gets();
                ViewData["optEmployee"] = uow.Modules.Employee.Gets();
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

        public ActionResult AddProduct()
        {
            string transferId = Request.Form["TransferId"].ToString();
            var productids = Request.Form["ProductIds"].ToString();  //  ProductId:PriceNet
            string qtys = Request.Form["RequestQtys"].ToString();
            string currentQty= Request.Form["currentQty"].ToString();
            List<TransferDetail> list = new List<TransferDetail>();

          
           if (transferId.Count() > 0)
            {
                int row = 0;
                for(row=0;row < productids.Split(',').Count();row++)
                {
                    TransferDetail   ob = new TransferDetail();
                           ob.TransferId = transferId.ParseInt();
                           ob.ProductId = productids.Split(',')[row].ParseInt();
                           ob.RequestQty = qtys.Split(',')[row].ParseInt();
                    ob.CurrentQty = currentQty.Split(',')[row];
                    ob.Seq = row + 1;

                    list.Add(ob);


                    /*   TransferDetail ob = uow.Modules.Transfer.GetDetail(qid.ParseInt(), row+1);
                       if (ob == null)
                       {
                           ob = new TransferDetail();
                           ob.TransferId = qid.ParseInt();
                           ob.ProductId = productids.Split(',')[row].ParseInt();
                           ob.RequestQty = qtys.Split(',')[row].ParseInt();
                           ob.Seq = row + 1;
                           uow.Modules.Transfer.SetDetail(ob);
                       }
                       else {
                           ob.TransferId = qid.ParseInt();
                           ob.ProductId = productids.Split(',')[row].ParseInt();
                           ob.RequestQty = qtys.Split(',')[row].ParseInt();
                           ob.Seq = row + 1;
                           uow.Modules.Transfer.UpdateDetail(ob);
                       }
                      */




                  //  uow.SaveChanges();
                }

                uow.Modules.Transfer.Add(transferId,list);


            }
            return RedirectToAction("Detail", MVCController, new { id = transferId, tab = "Product", msg = "บันทึกข้อมูลสินค้าเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        [HttpPost]
        //[Authorized]
        public ActionResult DetailDelete()
        {
            int qid = Request.Form["TransferId"].ParseInt();
            try
            {
                int Seq = Request.Form["Seq"].ParseInt();
                TransferDetail ob = uow.Modules.Transfer.GetDetail(qid, Seq);
                if (ob == null)
                { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Transfer.DeleteDetail(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }



        [HttpPost]
        //[Authorized]
        public ActionResult Approve()
        {
            int qid = Request.Form["TransferId"].ParseInt();
            try
            {
                bool ob = uow.Modules.Transfer.TrasferOutApprove(qid);
                return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Master", msg = "ยืนยันเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Master", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }



        [HttpGet]
        public JsonResult GetJobOrderByDate(string startDate,string toDate)
        {

            List<TblJobOrder> result = uow.Modules.JobOrder.GetByDate(startDate, toDate);
            //saleOrder.Customer = uow.Modules.Customer.GetByCondition(saleOrder.CustomerId.Value);
            //  saleOrder.Sale = uow.Modules.Employee.GetByCondition(saleOrder.SaleId.Value);

            // TblCustomer objCustomer = uow.Modules.Customer.Get(Convert.ToInt32(ob.CustomerId));



            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetTransferSOutList()
        {
            List<TransferHeader> lst = new List<TransferHeader>();
            try
            {
                lst = uow.Modules.Transfer.GetList("O");
                foreach (var ls in lst.ToList())
                {
                    
                    ls.StrTransferDate = ls.TransferDate.HasValue ? ls.TransferDate.Value.Day.ToString("00") + "/" + ls.TransferDate.Value.Month.ToString("00") + "/" + ls.TransferDate.Value.Year : "";
                    
                }
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
            return Json(lst);
        }
    }
}
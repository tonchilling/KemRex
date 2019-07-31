using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class InvoiceController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType, string src = "")
        {
            List<TblInvoice> lst = new List<TblInvoice>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Invoice.Count(0, src);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Invoice", "")
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
                ViewData["optPayment"] = uow.Modules.PaymentCondition.Gets();
                ViewBag.Pagination = Pagination;
                lst = uow.Modules.Invoice.Gets(Pagination.Page, Pagination.Size, src);
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
        [HttpPost]
        public ActionResult GetInvoiceList()
        {
            
            List<TblInvoice> lst = new List<TblInvoice>();
            EnmPaymentCondition payCon = new EnmPaymentCondition();
            try
            {
                lst = uow.Modules.Invoice.GetList();
                foreach (var pr in lst)
                {
                    pr.SaleOrder = uow.Modules.SaleOrder.Get(pr.SaleOrderId);
                    pr.StrInvoiceDate = pr.InvoiceDate.Day.ToString("00") + "/" + pr.InvoiceDate.Month.ToString("00") + "/" + pr.InvoiceDate.Year;
                    payCon = uow.Modules.PaymentCondition.Get(pr.SaleOrder.ConditionId.HasValue ? pr.SaleOrder.ConditionId.Value : 0);
                    pr.SaleOrder.ConditionName = payCon.ConditionName;
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
        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TblInvoice ob = uow.Modules.Invoice.Get(id ?? 0);
           
            ob.SaleOrder = uow.Modules.SaleOrder.Get(ob.SaleOrderId);
            return ViewDetail(ob, msg, msgType);
        }

        private string genInvoiceId(string pre)
        {

            var dt = CurrentDate.ToString("ddMMyy");


            pre += CurrentDate.ToString("yy") + "-" + CurrentDate.ToString("MM");

            string Id = uow.Modules.Invoice.GetLastId(pre);
            int runid = 1;
            if (Id != null)
            {
                runid = int.Parse(Id.Substring(Id.Length - 3)) + 1;
            }


            string soNo = pre + "-" + runid.ToString("D3");
            return soNo;
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int sid = Request.Form["SaleOrderId"].ParseInt();
            if (sid > -1)
            {
                int id = Request.Form["InvoiceId"].ParseInt();

                TblInvoice ob = uow.Modules.Invoice.Get(id);
                if (Request.Form["SaleOrderId"] != "")
                    ob.SaleOrderId = int.Parse(Request.Form["SaleOrderId"].ToString());
                ob.SaleOrder = uow.Modules.SaleOrder.Get(ob.SaleOrderId);
                if (ob.InvoiceId <= 0)
                {
                    ob.InvoiceNo = genInvoiceId("DEK");
                    ob.CreatedBy = CurrentUID;
                    ob.UpdatedBy = CurrentUID;
                    ob.CreatedDate = CurrentDate;
                    ob.UpdatedDate = CurrentDate;
                    ob.InvoiceDate = CurrentDate;
                    //ob.DueDate = CurrentDate.AddMonths(1);
                    switch (ob.SaleOrder.ConditionId)
                    {
                        case 1:     //จ่าย 100% หลังติดตั้งงาน
                        case 2: ob.InvoiceAmount = ob.SaleOrder.SummaryNet; //มัดจำ 100 % ก่อนติดตั้งงาน
                            break;
                        case 3: ob.InvoiceAmount = ob.SaleOrder.SummaryNet/2; //2 งวด 50 / 50
                            break;
                        case 4: //3 งวด
                        case 5: ob.InvoiceAmount = 0; //4 งวด
                            break;
                        default: ob.InvoiceAmount = 0;
                            break;
                    }
                }
                else
                {
                    ob.UpdatedBy = CurrentUID;
                    ob.UpdatedDate = CurrentDate;
                    ob.InvoiceAmount = 0;
                    if (Request.Form["InvoiceAmount"] != "")
                        ob.InvoiceAmount = decimal.Parse(Request.Form["InvoiceAmount"].ToString());
                }

                try
                {

                    ob.SaleOrderId = sid;
                    ob.StatusId = Request.Form["StatusId"].ParseInt();
                    ob.InvoiceTerm = Request.Form["InvoiceTerm"].ParseInt();
                    ob.InvoiceRemark = Request.Form["InvoiceRemark"];
                    //string hddIsDeposit = Request.Form["hddIsDeposit"].ToString();
                    //ob.IsDeposit = hddIsDeposit == ""? 0:int.Parse(Request.Form["hddIsDeposit"]);
                   
                    //decimal reamin = Request.Form["remain"].ToString().ParseDecimal();
                    //if (ob.StatusId > 1)
                    //{
                    //    if (ob.InvoiceAmount <= 0)
                    //    {
                    //        return ViewDetail(ob, "กรุณาระบุยอดที่ต้องการเรียกเก็บ", AlertMsgType.Danger);
                    //    }
                    //}
                    //if (ob.InvoiceId > 0)
                    //{
                    //    if (ob.InvoiceAmount > reamin)
                    //    {
                    //        return ViewDetail(ob, "ยอดเรียกเก็บต้อง น้อยกว่าหรือเท่ากับ ยอดที่ยังไม่ได้เรียกเก็บ", AlertMsgType.Danger);
                    //    }
                    //}
                    bool result = false;

                    if (ob.InvoiceId <= 0)
                    {
                        result = uow.Modules.Invoice.Add(ob);
                        int ins = uow.Modules.Invoice.GetInsertId(ob.InvoiceNo);
                        return RedirectToAction("Detail", MVCController, new { id = ins, msg = "สร้างหมายเลขใบแจ้งหนี้เรียบร้อยแล้ว", msgType = AlertMsgType.Success });
                    }
                    else
                    {
                        uow.Modules.Invoice.Set(ob);
                        uow.SaveChanges();
                    }
                    return RedirectToAction("Detail", MVCController, new { id = ob.InvoiceId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
                }
                catch (Exception ex)
                {
                    string msg = ex.GetMessage(true);
                    return ViewDetail(ob, msg, AlertMsgType.Danger);
                }
            }
            else
            {
                return RedirectToAction("Index", MVCController, new { msg = "กรุณาเลือก เลขที่ใบเสนอราคา/ใบสั่งงาน", msgType = AlertMsgType.Danger });
            }

        }

        [HttpPost]
        //[Authorized]
        public ActionResult Delete()
        {
            try
            {
                int id = Request.Form["InvoiceId"].ParseInt();
                TblInvoice ob = uow.Modules.Invoice.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Invoice.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }
        #region Private Action
        private ActionResult ViewDetail(TblInvoice ob, string msg, AlertMsgType? msgType)
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

                List<TblSaleOrder> saleOrderList = uow.Modules.SaleOrder.GetListForInvoiceByCondition("", "", "2");
                List<TblSaleOrder> saleOrderList2 = new List<TblSaleOrder>();
                decimal remain = 0;
                decimal total = 0;
                foreach (TblSaleOrder so in saleOrderList)
                {
                    remain = uow.Modules.Invoice.GetRemain(so.SaleOrderId);
                    total = so.SubTotalNet.HasValue ? so.SubTotalNet.Value : 0;
                    if (total - remain > 0)
                    {
                        saleOrderList2.Add(so);
                    }
                }


                ViewData["optSaleOrder"] = uow.Modules.SaleOrder.Gets();
                //ViewData["optSaleOrder"] = saleOrderList2;
                ViewData["optQuotation"] = uow.Modules.Quotation.Gets();
                ViewData["optPayment"] = uow.Modules.PaymentCondition.Gets();
                ViewData["optRemain"] = uow.Modules.Invoice.GetRemain(ob.SaleOrderId);


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
        #endregion
    }
}
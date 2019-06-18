using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TblInvoice ob = uow.Modules.Invoice.Get(id ?? 0);
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
            int sid = Request.Form["selSaleOrder"].ParseInt();
            if (sid > -1)
            {
                int id = Request.Form["InvoiceId"].ParseInt();

                TblInvoice ob = uow.Modules.Invoice.Get(id);
                if (ob.InvoiceId <= 0)
                {
                    ob.InvoiceNo = genInvoiceId("DEK");
                    ob.CreatedBy = CurrentUID;
                    ob.UpdatedBy = CurrentUID;
                    ob.CreatedDate = CurrentDate;
                    ob.UpdateDate = CurrentDate;
                    ob.InvoiceDate = CurrentDate;
                }
                else
                {
                    ob.UpdatedBy = CurrentUID;
                    ob.UpdateDate = CurrentDate;
                }

                try
                {

                    ob.SaleOrderId = sid;
                    ob.StatusId = Request.Form["StatusId"].ParseInt();
                    ob.InvoiceTerm = Request.Form["InvoiceTerm"].ParseInt();
                    ob.InvoiceRemark = Request.Form["InvoiceRemark"];
                    ob.InvoiceAmount = 0;
                    if (Request.Form["InvoiceAmount"] != "")
                        ob.InvoiceAmount = decimal.Parse(Request.Form["InvoiceAmount"].ToString());


                    uow.Modules.Invoice.Set(ob);
                    uow.SaveChanges();

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

                ViewData["optSaleOrder"] = uow.Modules.SaleOrder.Gets();
                ViewData["optQuotation"] = uow.Modules.Quotation.Gets();

                ViewData["optPayment"] = uow.Modules.PaymentCondition.Gets();

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
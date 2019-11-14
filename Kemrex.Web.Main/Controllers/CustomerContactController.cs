using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class CustomerContactController : KemrexController
    {
        public ActionResult Detail(int cusId, int? id, int? qtid, string msg, AlertMsgType? msgType)
        {
            TblCustomerContact ob = uow.Modules.CustomerContact.Get(id ?? 0);
            if (ob.ContactId <= 0)
            {
                ob.CustomerId = cusId;
                ob.Customer = uow.Modules.Customer.Get(cusId);
            }
            return ViewDetail(ob, qtid, msg, msgType);
        }
    
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int id = Request.Form["ContactId"].ParseInt();
            int? qtid = Request.Form["QtId"].ParseIntNullable();
            TblCustomerContact ob = uow.Modules.CustomerContact.Get(id);
            if (ob.ContactId <= 0)
            {
                ob.CustomerId = Request.Form["CustomerId"].ParseInt();
                ob.Customer = uow.Modules.Customer.Get(ob.CustomerId);
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            ob.ContactName = Request.Form["ContactName"];
            ob.ContactPosition = Request.Form["ContactPosition"];
            ob.ContactPhone = Request.Form["ContactPhone"];
            ob.ContactEmail = Request.Form["ContactEmail"];
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                //Validate model b4 save

                uow.Modules.CustomerContact.Set(ob);
                uow.SaveChanges();
                if ((qtid ?? 0) > 0)
                {
                    return RedirectToAction("Detail", new
                    {
                        controller = "Quotation",
                        msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                        msgType = AlertMsgType.Success,
                        id = qtid.Value
                    });
                }
                return RedirectToAction("Detail", new
                {
                    controller = "Customer",
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                    msgType = AlertMsgType.Success,
                    id = ob.CustomerId
                });
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(ob, qtid, msg, AlertMsgType.Danger);
            }
        }

        [HttpPost]
        [Authorized]
        public ActionResult Delete()
        {
            int cusId = Request.Form["CustomerId"].ParseInt();
            try
            {
                int id = Request.Form["ContactId"].ParseInt();
                TblCustomerContact ob = uow.Modules.CustomerContact.Get(id);
                if (ob == null)
                { return RedirectToAction("Detail", "Customer", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning, id = cusId }); }

                uow.Modules.CustomerContact.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", "Customer", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success, id = cusId });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", "Customer", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger, id = cusId }); }
        }

        public ActionResult loadAddress(int? cusId)
        {
            return View();
        }

        #region Private Action
        private ActionResult ViewDetail(TblCustomerContact ob, int? qtid, string msg, AlertMsgType? msgType)
        {
            try
            {
                if (ob == null)
                { return RedirectToAction("Index", "Customer", new { msg = "ไม่พบข้อมูลที่ต้องการ, กรุณาลองใหม่อีกครั้ง", msgType = AlertMsgType.Danger }); }

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                ViewData["QtId"] = qtid;
                return View(ob);
            }
            catch (Exception ex)
            {
                msg = ex.GetMessage();
                msgType = AlertMsgType.Danger;
                return RedirectToAction("Detail", "Customer", new { msg = ex.GetMessage(), msgType, id = ob.CustomerId });
            }
        }
        #endregion
    }
}
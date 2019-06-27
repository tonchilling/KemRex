using DMN.Standard.Common.Constraints;
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

using IOFile = System.IO.File;

namespace Kemrex.Web.Main.Controllers
{
    public class CustomerController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            string src = "")
        {
            List<TblCustomer> lst = new List<TblCustomer>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Customer.Count(0, src);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Customer", "")
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
                lst = uow.Modules.Customer.Gets(Pagination.Page, Pagination.Size, 0, src);
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
            TblCustomer ob = uow.Modules.Customer.Get(id ?? 0);
            return ViewDetail(ob, msg, msgType);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int id = Request.Form["CustomerId"].ParseInt();
            TblCustomer ob = uow.Modules.Customer.Get(id);
            if (ob.CustomerId <= 0)
            {
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            if (Request.Form["PrefixId"].ParseInt() <= 0)
            {
                ob.PrefixId = null;
                ob.Prefix = null;
            }
            else
            {
                ob.PrefixId = Request.Form["PrefixId"].ParseInt();
                ob.Prefix = uow.Modules.Enum.PrefixGet(ob.PrefixId.Value);
            }
            ob.CustomerName = Request.Form["CustomerName"];
            ob.CustomerTaxId = Request.Form["CustomerTaxId"];
            ob.GroupId = Request.Form["GroupId"].ParseInt();
            ob.Group = uow.Modules.CustomerGroup.Get(ob.GroupId.Value);
            ob.CustomerPhone = Request.Form["CustomerPhone"];
            ob.CustomerEmail = Request.Form["CustomerEmail"];
            ob.CustomerType = 1;
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                if (Request.Files.Count > 0 && Request.Files["CustomerAvatar"] != null && Request.Files["CustomerAvatar"].ContentLength > 0)
                {
                    string oldPath = ob.CustomerAvatar;
                    HttpPostedFileBase uploadedFile = Request.Files["CustomerAvatar"];
                    string FilePath = string.Format("files/customers/{0}{1}", CurrentDate.ParseString(DateFormat._yyyyMMddHHmmssfff), Path.GetExtension(uploadedFile.FileName));
                    if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                    if (!Directory.Exists(Server.MapPath("~/files/customers"))) { Directory.CreateDirectory(Server.MapPath("~/files/customers")); }
                    uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));
                    ob.CustomerAvatar = FilePath;
                    if (!string.IsNullOrWhiteSpace(oldPath) && IOFile.Exists(Server.MapPath("~/" + oldPath)))
                    { IOFile.Delete(Server.MapPath("~/" + oldPath)); }
                }
                //Validate model b4 save

                uow.Modules.Customer.Set(ob);
                uow.SaveChanges();

                return RedirectToAction("Index", new
                {
                    area = "",
                    controller = MVCController,
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                    msgType = AlertMsgType.Success
                });
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(ob, msg, AlertMsgType.Danger);
            }
        }

        [HttpPost]
        [Authorized]
        public ActionResult Delete()
        {
            try
            {
                int id = Request.Form["CustomerId"].ParseInt();
                TblCustomer ob = uow.Modules.Customer.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Customer.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        #region Private Action
        private ActionResult ViewDetail(TblCustomer ob, string msg, AlertMsgType? msgType)
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
                ViewData["optGroup"] = uow.Modules.CustomerGroup.Gets();
                ViewData["optPrefix"] = uow.Modules.Enum.PrefixGets();
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

        [HttpGet]

        public JsonResult GetCustomer(int id)
        {

         
             TblCustomer objCustomer = uow.Modules.Customer.GetByCondition(id);



            return Json(objCustomer, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
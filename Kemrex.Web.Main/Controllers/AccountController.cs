using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common.Extensions.Database;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

using IOFile = System.IO.File;

namespace Kemrex.Web.Main.Controllers
{
    public class AccountController : KemrexController
    {


        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            string src = "", string username = "")
        {
            List<SysAccount> lst = new List<SysAccount>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Account.Counts(SiteId, src, username);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Account", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>()
                    {
                        { "src", src },
                        { "username", username }
                    },
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = uow.Modules.Account.Gets(Pagination.Page, Pagination.Size, SiteId, src, username);
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

       
        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            SysAccount ob = uow.Modules.Account.Get(id ?? 0);
            if (ob.AccountId <= 0)
            {
                ob.FlagStatus = 1;
            }
            return ViewDetail(ob, msg, msgType);
        }

       
        [ActionName("Profile")]
        public ActionResult ProfileMe(string msg, AlertMsgType? msgType)
        {
            int id = Session["sid"].Convert2String().ParseInt();
            SysAccount ob = uow.Modules.Account.Get(id);
            return ViewProfile(ob, msg, msgType);
        }

        //[Authorized]
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult SetStaff()
        //{
        //    int accountId = Request.Form["account_id"].ParseInt();
        //    SysAccount acc = uow.Modules.Account.Get(accountId);
        //    int staffId = Request.Form["staff_id"].ParseInt();
        //    CalcAccountStaff ob = new CalcAccountStaff()
        //    {
        //        AccountId = accountId,
        //        StaffId = staffId
        //    };
        //    if (db.CalcAccountStaff.Where(x => x.AccountId == ob.AccountId && x.StaffId == ob.StaffId).Count() > 0)
        //    {
        //        string msg = "ข้อมูลดังกล่าวมีอยู่แล้วในระบบ";
        //        return ViewDetail(acc, msg, AlertMsgType.Danger);
        //    }
        //    db.CalcAccountStaff.Add(ob);
        //    db.SaveChanges();
        //    return RedirectToAction("Detail", new
        //    {
        //        id = accountId,
        //        controller = "Account",
        //        msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
        //        msgType = AlertMsgType.Success
        //    });
        //}

       
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Profile")]
        public ActionResult SetProfile()
        {
            int accountId = Session["sid"].Convert2String().ParseInt();
            SysAccount ob = uow.Modules.Account.Get(accountId);
            ob.AccountFirstName = Request.Form["account_firstname"];
            ob.AccountLastName = Request.Form["account_lastname"];
            ob.AccountEmail = Request.Form["account_email"];
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                if (!ob.IsValid(out string errMsg))
                { throw new Exception(errMsg); }
                bool clearOld = false;
                string oldAvatar = ob.AccountAvatar;
                if (Request.Files.Count > 0 && Request.Files["AccountAvatar"] != null && Request.Files["AccountAvatar"].ContentLength > 0)
                {
                    HttpPostedFileBase uploadedFile = Request.Files["AccountAvatar"];
                    string FilePath = string.Format("files/avatar/{0}{1}", CurrentDate.ParseString(DateFormat._yyyyMMddHHmmssfff), Path.GetExtension(uploadedFile.FileName));
                    if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                    if (!Directory.Exists(Server.MapPath("~/files/avatar"))) { Directory.CreateDirectory(Server.MapPath("~/files/avatar")); }
                    uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));

                    ob.AccountAvatar = FilePath;
                    clearOld = true;
                }

                if ((ob.AccountId <= 0 && ob.AccountPassword != Request.Form["account_passwordre"])
                    || (ob.AccountId > 0 && !string.IsNullOrWhiteSpace(Request.Form["account_password"]) && Request.Form["account_password"] != Request.Form["account_passwordre"]))
                { throw new Exception("รหัสผ่านไม่ตรงกัน"); }

                if (ob.AccountId > 0 && !string.IsNullOrWhiteSpace(Request.Form["account_password"]))
                { ob.AccountPassword = Crypto.HashPassword(Request.Form["account_password"]); }

                uow.Modules.Account.Set(ob);
                uow.SaveChanges();

                if (clearOld && !string.IsNullOrWhiteSpace(oldAvatar) && IOFile.Exists(Server.MapPath("~/" + oldAvatar)))
                { IOFile.Delete(Server.MapPath("~/" + oldAvatar)); }

                return RedirectToAction("Index", new
                {
                    area = "",
                    controller = "Home",
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                    msgType = AlertMsgType.Success
                });
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage();
                return ViewProfile(ob, msg, AlertMsgType.Danger);
            }
        }

       
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {

            int accountId = Request.Form["account_id"].ParseInt();
            int empId = Request.Form["EmpId"].ParseInt();
            SysAccount ob = uow.Modules.Account.Get(accountId);
            if (ob.AccountId <= 0)
            {
                ob.AccountUsername = Request.Form["account_username"];
                ob.AccountPassword = Request.Form["account_password"];
                ob.AccountEmail = Request.Form["account_email"];
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            ob.AccountFirstName = Request.Form["account_firstname"];
            ob.AccountLastName = Request.Form["account_lastname"];
            ob.FlagStatus = Request.Form["flag_status"].ParseInt();
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            int roleId = Request.Form["role_id"].ParseInt();
            try
            {
                if (!ob.IsValid(out string errMsg))
                { throw new Exception(errMsg); }

                if (Request.Files.Count > 0 && Request.Files["AccountAvatar"] != null && Request.Files["AccountAvatar"].ContentLength > 0)
                {
                    HttpPostedFileBase uploadedFile = Request.Files["AccountAvatar"];
                    string FilePath = string.Format("files/avatar/{0}{1}", CurrentDate.ParseString(DateFormat._yyyyMMddHHmmssfff), Path.GetExtension(uploadedFile.FileName));
                    if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                    if (!Directory.Exists(Server.MapPath("~/files/avatar"))) { Directory.CreateDirectory(Server.MapPath("~/files/avatar")); }
                    uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));

                    ob.AccountAvatar = FilePath;
                }

                if ((ob.AccountId <= 0 && ob.AccountPassword != Request.Form["account_passwordre"])
                    || (ob.AccountId > 0 && !string.IsNullOrWhiteSpace(Request.Form["account_password"]) && Request.Form["account_password"] != Request.Form["account_passwordre"]))
                { throw new Exception("รหัสผ่านไม่ตรงกัน"); }

                //if (!ob.FlagSystem)
                //{
                //    SysAccountRole roleMapped = ob.AccountId > 0 ?
                //        ((from d in uow.db.SysAccountRole
                //          join r in uow.db.SysRole on d.RoleId equals r.RoleId
                //          where
                //              d.AccountId == ob.AccountId
                //              && r.SiteId == SITE_ID
                //          select d).FirstOrDefault() ?? new SysAccountRole() { SysAccount = ob }) :
                //        new SysAccountRole() { SysAccount = ob };
                //    roleMapped.RoleId = Request.Form["role_id"].ParseInt();
                //    if (roleMapped.Id <= 0) { uow.db.SysAccountRole.Add(roleMapped); }
                //    else { uow.db.Entry(roleMapped).State = System.Data.Entity.EntityState.Modified; }
                //}

               
                if (ob.AccountId <= 0)
                { ob.AccountPassword = Crypto.HashPassword(ob.AccountPassword); }
                else if (ob.AccountId > 0 && !string.IsNullOrWhiteSpace(Request.Form["account_password"]))
                { ob.AccountPassword = Crypto.HashPassword(Request.Form["account_password"]); }

               
                uow.Modules.Account.Set(ob);

             
              //  employee.UpdatedDate = DateTime.Now;
              
                uow.Modules.Account.SetRole(SiteId, roleId, ob);
                uow.SaveChanges();

                if (empId > 0)
                {
                    TblEmployee employee = uow.Modules.Employee.Get(empId);

                    employee.AccountId = ob.AccountId;

                    uow.Modules.Employee.Set(employee);
                }
                uow.SaveChanges();
                return RedirectToAction("Index", new
                {
                    area = "",
                    controller = "Account",
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                    msgType = AlertMsgType.Success
                });
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage();
                return ViewDetail(ob, msg, AlertMsgType.Danger);
            }
        }

        [HttpPost]
    
        public ActionResult Delete()
        {
            try
            {
                long id = Request.Form["AccountId"].ParseLong();
                SysAccount ob = uow.Modules.Account.Get(id);
              
                if (ob == null)
                { return RedirectToAction("Index", "Account", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Account.DeleteBySQL(ob);
               // uow.SaveChanges();
                return RedirectToAction("Index", "Account", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", "Account", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        [HttpPost, ActionName("GetAccountList")]
        public JsonResult GetAccountList()
        {

            List<SysAccount> AccountList = uow.Modules.Account.GetNewList();
            // ob.TblSaleOrderDetail = uow.Modules.SaleOrderDetail.Gets(id ?? 0);
            // TblCustomer objCustomer = uow.Modules.Customer.Get(Convert.ToInt32(ob.CustomerId));



            return Json(AccountList, JsonRequestBehavior.AllowGet);
        }

        #region Private Action
        private ActionResult ViewDetail(SysAccount ob, string msg, AlertMsgType? msgType)
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
                ViewData["optRole"] = uow.Modules.Role.Gets(1, 0, SiteId);
                ViewData["optRoleCal"] = uow.Modules.Role.Gets(1, 0, 2);
                return View("Detail", ob);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new
                {
                    area = "",
                    controller = "Account",
                    msg = ex.GetMessage(),
                    msgType = AlertMsgType.Danger
                });
            }
        }

        private ActionResult ViewProfile(SysAccount ob, string msg, AlertMsgType? msgType)
        {
            try
            {
                if (Session["sid"] == null || ob == null)
                { throw new Exception("กรุณาเข้าสู่ระบบใหม่อีกครั้ง"); }

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                return View("Profile", ob);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new
                {
                    controller = "Home",
                    msg = ex.GetMessage(),
                    msgType = AlertMsgType.Danger
                });
            }
        }
        #endregion
    }
}
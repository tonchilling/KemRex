using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using FileIO = System.IO.File;
using Kemrex.Database;
using Kemrex.FWCommon;
using Kemrex.SoilCalculator.Web.ActionFilters;
using Kemrex.SoilCalculator.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class AccountController : BaseController
    {
        public const int SITE_ID = 3;
        public UnitOfWork uow;
        public int CurrentUID => (Session["sid"].Convert2String().ParseIntNullable() ?? 0);
        public SysAccount CurrentUser => uow.Account.Get(CurrentUID);
        public DateTime CurrentDateTime = DateTime.Now;
        public AccountController()
        {
            uow = new UnitOfWork();
        }

        [BackendAuthorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            string src = "", string username = "")
        {
            SysRole role = CurrentUser.Role(SITE_ID).Role();
            ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 2910 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 2910 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            List<SysAccount> lst = new List<SysAccount>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Account.Counts(src, username);
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
                lst = uow.Account.Gets(Pagination.Page, Pagination.Size, src, username);
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
        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            SysAccount ob = uow.Account.Get(id ?? 0);
            if (ob.AccountId <= 0)
            {
                ob.FlagStatus = 1;
            }
            return ViewDetail(ob, msg, msgType);
        }

        [BackendAuthorized]
        [ActionName("Profile")]
        public ActionResult ProfileMe(string msg, AlertMsgType? msgType)
        {
            int id = Session["sid"].Convert2String().ParseInt();
            SysAccount ob = uow.Account.Get(id);
            return ViewProfile(ob, msg, msgType);
        }

        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SetStaff()
        {
            int accountId = Request.Form["account_id"].ParseInt();
            SysAccount acc = uow.Account.Get(accountId);
            int staffId = Request.Form["staff_id"].ParseInt();
            CalcAccountStaff ob = new CalcAccountStaff()
            {
                AccountId = accountId,
                StaffId = staffId
            };
            if (db.CalcAccountStaff.Where(x => x.AccountId == ob.AccountId && x.StaffId == ob.StaffId).Count() > 0)
            {
                string msg = "ข้อมูลดังกล่าวมีอยู่แล้วในระบบ";
                return ViewDetail(acc, msg, AlertMsgType.Danger);
            }
            db.CalcAccountStaff.Add(ob);
            db.SaveChanges();
            return RedirectToAction("Detail", new
            {
                id = accountId,
                controller = "Account",
                msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                msgType = AlertMsgType.Success
            });
        }

        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Profile")]
        public ActionResult SetProfile()
        {
            int accountId = Session["sid"].Convert2String().ParseInt();
            SysAccount ob = uow.Account.Get(accountId);
            ob.AccountFirstName = Request.Form["account_firstname"];
            ob.AccountLastName = Request.Form["account_lastname"];
            ob.AccountEmail = Request.Form["account_email"];
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDateTime;
            try
            {
                if (!ob.ValidateModel(out string errMsg))
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

                uow.Account.Set(ob);
                uow.SaveChanges();

                if (clearOld && !string.IsNullOrWhiteSpace(oldAvatar) && FileIO.Exists(Server.MapPath("~/" + oldAvatar)))
                { FileIO.Delete(Server.MapPath("~/" + oldAvatar)); }

                return RedirectToAction("Index", new
                {
                    area = "",
                    controller = "Home",
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
                return ViewProfile(ob, msg, AlertMsgType.Danger);
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage();
                return ViewProfile(ob, msg, AlertMsgType.Danger);
            }
        }

        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int accountId = Request.Form["account_id"].ParseInt();
            SysAccount ob = uow.Account.Get(accountId);
            if (ob.AccountId <= 0)
            {
                ob.AccountPassword = Request.Form["account_password"];
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDateTime;
            }
            ob.AccountFirstName = Request.Form["account_firstname"];
            ob.AccountLastName = Request.Form["account_lastname"];
            ob.AccountUsername = "usr" + CurrentDateTime.ToString("yyyyMMddHHmmss"); //Request.Form["account_username"];
            ob.AccountEmail = Request.Form["account_email"];
            ob.FlagStatus = Request.Form["flag_status"].ParseInt();
            ob.FlagAdminCalc = Request.Form["flag_admin_cal"].ParseBoolean();
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDateTime;
            try
            {
                if (!ob.ValidateModel(out string errMsg))
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

                if (!ob.FlagSystem)
                {
                    SysAccountRole roleMapped = ob.AccountId > 0 ?
                        ((from d in uow.db.SysAccountRole
                          join r in uow.db.SysRole on d.RoleId equals r.RoleId
                          where
                              d.AccountId == ob.AccountId
                              && r.SiteId == SITE_ID
                          select d).FirstOrDefault() ?? new SysAccountRole() { SysAccount = ob }) :
                        new SysAccountRole() { SysAccount = ob };
                    roleMapped.RoleId = Request.Form["role_id"].ParseInt();
                    if (roleMapped.Id <= 0) { uow.db.SysAccountRole.Add(roleMapped); }
                    else { uow.db.Entry(roleMapped).State = System.Data.Entity.EntityState.Modified; }
                }

                if (ob.AccountId <= 0)
                { ob.AccountPassword = Crypto.HashPassword(ob.AccountPassword); }
                else if (ob.AccountId > 0 && !string.IsNullOrWhiteSpace(Request.Form["account_password"]))
                { ob.AccountPassword = Crypto.HashPassword(Request.Form["account_password"]); }

                uow.Account.Set(ob);
                if (ob.FlagAdminCalc && ob.AccountId > 0)
                {
                    var delData = db.CalcAccountStaff.Where(x => x.AccountId == ob.AccountId);
                    db.CalcAccountStaff.RemoveRange(delData);
                    db.SaveChanges();
                }
                uow.SaveChanges();

                return RedirectToAction("Index", new {
                    area = "",
                    controller = "Account",
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
        public ActionResult Delete(int id)
        {
            try
            {
                SysRole role = CurrentUser.Role(3).Role();
                bool canDelete = role.SysRolePermission.Where(x => x.MenuId == 3910 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
                if (!canDelete) { return RedirectToAction("Index", "Account", new { msg = "ไม่มีสิทธิ์ในการลบข้อมูล", msgType = AlertMsgType.Danger }); }

                SysAccount ob = db.SysAccount.Find(id);
                if (ob == null)
                { return RedirectToAction("Index", "Account", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                var accountRoles = db.SysAccountRole.Where(x => x.AccountId == id);
                db.SysAccountRole.RemoveRange(accountRoles);
                db.SysAccount.Remove(ob);
                db.SaveChanges();
                return RedirectToAction("Index", "Account", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", "Account", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
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
                ViewData["optRole"] = uow.Role.Gets(1, 0, 3);
                ViewData["optRoleCal"] = uow.Role.Gets(1, 0, 3);
                ViewData["optStaff"] = (from d in db.SysAccount
                                        where d.AccountId != ob.AccountId
                                            && !db.CalcAccountStaff.Where(x=>x.AccountId==ob.AccountId).Select(x=>x.AccountId).Contains(d.AccountId)
                                        select d).ToList();
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
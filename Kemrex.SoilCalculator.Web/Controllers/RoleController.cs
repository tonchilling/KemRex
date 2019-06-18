using DMN.Standard.Common.Extensions;
using Kemrex.Database;
using Kemrex.FWCommon;
using Kemrex.SoilCalculator.Web.ActionFilters;
using Kemrex.SoilCalculator.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class RoleController : BaseController
    {
        public const int SITE_ID = 3;
        public UnitOfWork uow;
        public int CurrentUID => (Session["sid"].Convert2String().ParseIntNullable() ?? 0);
        public SysAccount CurrentUser => uow.Account.Get(CurrentUID);
        public DateTime CurrentDateTime = DateTime.Now;
        public RoleController()
        {
            uow = new UnitOfWork();
        }

        [BackendAuthorized]
        public ActionResult Detail(int? id, int? siteId, string msg, AlertMsgType? msgType)
        {
            SysRole ob = uow.Role.Get(id ?? 0);
            if (ob.RoleId <= 0)
            {
                ob.SiteId = siteId ?? SITE_ID;
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDateTime.Date;
            }
            return ViewDetail(ob, msg, msgType);
        }
        [BackendAuthorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType)
        {
            SysRole role = CurrentUser.Role(3).Role();
            ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3920 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 3920 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            List<SysRole> lst = new List<SysRole>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Role.Counts(SITE_ID);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Role", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = uow.Role.Gets(Pagination.Page, Pagination.Size, SITE_ID);
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
        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public ActionResult SetData()
        {
            int roleId = Request.Form["role_id"].ParseInt();
            SysRole ob = uow.Role.Get(roleId);
            if (ob.RoleId <= 0)
            {
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDateTime;
            }
            ob.SiteId = SITE_ID;
            ob.RoleName = Request.Form["role_name"];
            ob.RoleDescription = Request.Form["role_description"];
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDateTime;
            try
            {
                if (!ob.ValidateModel(out string errMsg))
                { throw new Exception(errMsg); }

                if (!ob.FlagSystem)
                {
                    ob.FlagActive = Request.Form["flag_active"].ParseBoolean();
                    List<SysMenu> menus = uow.System.GetMenuBySiteId(ob.SiteId, 0);

                    foreach (SysMenu menu in menus)
                    {
                        foreach (SysMenuPermission permission in menu.SysMenuPermission)
                        {
                            SysRolePermission rolePermission = uow.Role.GetPermission(ob.RoleId, menu.MenuId, permission.PermissionId);
                            rolePermission.SysRole = ob;
                            rolePermission.PermissionFlag = Request.Form[string.Format("permission-{0}-{1}", menu.MenuId, permission.PermissionId)].ParseBoolean();
                            uow.Role.SetPermission(rolePermission);
                        }
                    }
                }

                uow.Role.Set(ob);
                uow.SaveChanges();

                return RedirectToAction("Index", new { area = "", controller = "Role", msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
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
                bool canDelete = role.SysRolePermission.Where(x => x.MenuId == 3920 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
                if (!canDelete) { return RedirectToAction("Index", "Role", new { msg = "ไม่มีสิทธิ์ในการลบข้อมูล", msgType = AlertMsgType.Danger }); }

                SysRole ob = db.SysRole.Find(id);
                if (ob == null)
                { return RedirectToAction("Index", "Role", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }
                else if (db.SysAccountRole.Where(x => x.RoleId == id).Count() > 0)
                { return RedirectToAction("Index", "Role", new { msg = "ข้อมูลนี้ถูกใช้งานอยู่ ไม่สามารถลบข้อมูลได้", msgType = AlertMsgType.Warning }); }

                var rolePerm = db.SysRolePermission.Where(x => x.RoleId == id);
                db.SysRolePermission.RemoveRange(rolePerm);
                db.SysRole.Remove(ob);
                db.SaveChanges();
                return RedirectToAction("Index", "Role", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch(Exception ex)
            { return RedirectToAction("Index", "Role", new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        #region Private Action
        private ActionResult ViewDetail(SysRole ob, string msg, AlertMsgType? msgType)
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
                ViewBag.Menues = uow.System.GetMenuBySiteId(ob.SiteId);
                return View(ob);
            }
            catch (Exception ex)
            { return RedirectToAction("Index", new { area = "", controller = "Role", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }
        #endregion
    }
}
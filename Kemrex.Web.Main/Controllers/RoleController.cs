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
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class RoleController : KemrexController
    {
        [Authorized]
        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            SysRole ob = uow.Modules.Role.Get(id ?? 0);
            if (ob.RoleId <= 0)
            {
                ob.SiteId = SiteId;
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            return ViewDetail(ob, msg, msgType);
        }

        [Authorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType)
        {
            List<SysRole> lst = new List<SysRole>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Role.Counts(SiteId);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Role", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = uow.Modules.Role.Gets(Pagination.Page, Pagination.Size, SiteId);
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

        [Authorized]
        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public ActionResult SetData()
        {
            int roleId = Request.Form["role_id"].ParseInt();
            SysRole ob = uow.Modules.Role.Get(roleId);
            if (ob.RoleId <= 0)
            {
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            ob.SiteId = SiteId;
            ob.RoleName = Request.Form["role_name"];
            ob.RoleDescription = Request.Form["role_description"];
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                if (!ob.IsValid(out string errMsg))
                { throw new Exception(errMsg); }

                if (!ob.FlagSystem)
                {
                    ob.FlagActive = Request.Form["flag_active"].ParseBoolean();
                    List<SysMenu> menus = uow.Modules.System.GetMenuBase(ob.SiteId);

                    foreach (SysMenu menu in menus)
                    {
                        foreach (SysMenuPermission permission in menu.SysMenuPermission)
                        {
                            SysRolePermission rolePermission = uow.Modules.RolePermission.Get(ob.RoleId, menu.MenuId, permission.PermissionId);
                            rolePermission.Role = ob;
                            rolePermission.PermissionFlag = Request.Form[string.Format("permission-{0}-{1}", menu.MenuId, permission.PermissionId)].ParseBoolean();
                            uow.Modules.RolePermission.Set(rolePermission);
                        }
                    }
                }

                uow.Modules.Role.Set(ob);
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
        [Authorized]
        public ActionResult Delete()
        {
            try
            {
                int id = Request.Form["role_id"].ParseInt();
                SysRole ob = uow.Modules.Role.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", "Role", new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }
                else if (!uow.Modules.Role.IsUsed(id))
                { return RedirectToAction("Index", "Role", new { msg = "ข้อมูลนี้ถูกใช้งานอยู่ ไม่สามารถลบข้อมูลได้", msgType = AlertMsgType.Warning }); }

                uow.Modules.RolePermission.DeleteByRole(id);
                uow.Modules.Role.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", "Role", new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
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
                ViewBag.Menues = uow.Modules.System.GetMenuBase(SiteId);
                return View(ob);
            }
            catch (Exception ex)
            { return RedirectToAction("Index", new { area = "", controller = "Role", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }
        #endregion
    }
}
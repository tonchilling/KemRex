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
using Kemrex.Web.Common.Models.Layouts;


using DMN.Standard.Common.Utils;
using Kemrex.Core.Common;
using Kemrex.Core.Common.Constraints;
using Kemrex.Core.Common.Helper;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

            List<SysRolePermission> result = new List<SysRolePermission>(); 
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

                    MenuLayoutModel md = new MenuLayoutModel();
                    ob.FlagActive = Request.Form["flag_active"].ParseBoolean();


                     List<SysMenu> menus = uow.Modules.System.GetMenuBase(ob.SiteId);


                    md.Menus = new List<MenuModel>();
                    foreach (SysMenu menu in menus)
                    { md.Menus.Add(ConvertToMenuModel(menu)); }

                    int permissionId = 1;
                    SysRolePermission rolePermission = null;
                    foreach (MenuModel menu in md.Menus)
                    {
                        rolePermission = new SysRolePermission();
                        rolePermission.RoleId = ob.RoleId;
                        rolePermission.MenuId = menu.MenuId;
                        rolePermission.PermissionId = 1;
                        rolePermission.PermissionFlag = Request.Form[string.Format("permission-{0}-{1}", menu.MenuId, rolePermission.PermissionId)].ParseBoolean();
                        result.Add(rolePermission);

                        foreach (MenuModel permission in menu.SubMenus)
                        {
                           

                            for (permissionId = 1; permissionId <= 3; permissionId++)
                            {
                                 rolePermission = new SysRolePermission();
                                rolePermission.RoleId = ob.RoleId;
                                rolePermission.MenuId = permission.MenuId;
                                rolePermission.PermissionId = permissionId;
                                rolePermission.PermissionFlag = Request.Form[string.Format("permission-{0}-{1}", permission.MenuId, permissionId)].ParseBoolean();

                                result.Add(rolePermission);
                            }
                         //   uow.Modules.RolePermission.Set(rolePermission);
                        }
                    }
                }
                uow.Modules.RolePermission.Add(result);
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

        private MenuModel ConvertToMenuModel(SysMenu ob)
        {
            MenuModel rs = new MenuModel()
            {
                MenuId = ob.MenuId,
                MenuIcon = ob.MenuIcon,
                MenuName = ob.MenuName,
                MenuOrder = ob.MenuOrder,
                MvcArea = ob.MvcArea,
                MvcController = ob.MvcController,
                MvcAction = ob.MvcAction,
                SubMenus = ob.InverseParent.Select(x => ConvertToMenuModel(x)).OrderBy(o => o.MenuOrder).ToList()
            };
            return rs;
        }
        #endregion
    }
}
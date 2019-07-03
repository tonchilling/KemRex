using DMN.Standard.Common.Extensions;
using DMN.Standard.Common.Utils;
using Kemrex.Core.Common;
using Kemrex.Core.Common.Constraints;
using Kemrex.Core.Common.Helper;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.Models;
using Kemrex.Web.Common.Models.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kemrex.Web.Common.Controllers
{
    public class KemrexController : BaseController
    {
        public readonly int SiteId;
        public LogUtil logger;
        public UnitOfWork uow;

        public long CurrentUID
        { get { return SystemHelper.GetConfigurationKey(ConfigKey.DEBUG_TESTMODE).ParseBoolean() ? 1 : Session["sid"].Convert2String().ParseLong(1); } } // For development only

        public SysAccount CurrentUser => uow.Modules.Account.Get(CurrentUID);
        public DateTime CurrentDate { get { return DateTime.Now; } }

        public KemrexController()
        {
            string constr = SystemHelper.GetConfigurationKey(ConfigKey.CONNECTION_DB);
            if (logger == null) { logger = new LogUtil(SystemHelper.GetConfigurationKey(ConfigKey.LOG_PATH)); }
            uow = new UnitOfWork(constr);
            SiteId = SystemHelper.GetConfigurationKey(ConfigKey.APP_SITE_ID).ParseInt();
        }

        public ActionResult MenuSystem(string name = "_lyMenu")
        {
            MenuLayoutModel md = new MenuLayoutModel();
            md.ActiveMenuId = uow.Modules.System.GetMenuActiveList(MVCArea, MVCController, MVCAction);
            md.Menus = new List<MenuModel>();
            foreach(SysMenu menu in uow.Modules.System.GetMenuBase(SiteId))
            { md.Menus.Add(ConvertToMenuModel(menu)); }
            return PartialView(name, md);
        }

        public MenuLayoutModel GetMenus()
        {
            MenuLayoutModel md = new MenuLayoutModel();
            md.ActiveMenuId = uow.Modules.System.GetMenuActiveList(MVCArea, MVCController, MVCAction);
            md.Menus = new List<MenuModel>();
            foreach (SysMenu menu in uow.Modules.System.GetMenuBase(SiteId))
            { md.Menus.Add(ConvertToMenuModel(menu)); }
            return md;
        }

        public MenuLayoutModel GetMenuByRole()
        {
            List<SysMenu> sysMenu = new List<SysMenu>();
            MenuLayoutModel md = new MenuLayoutModel();
            md.ActiveMenuId = uow.Modules.System.GetMenuActiveList(MVCArea, MVCController, MVCAction);
            md.Menus = new List<MenuModel>();

            sysMenu = uow.Modules.RolePermission.GetMenuByRole(CurrentUser.SysAccountRole.RoleId);
            foreach (SysMenu menu in sysMenu.Where(o => o.ParentId == 0 && o.View == 1))
            { md.Menus.Add(ConvertToMenuModel(menu, sysMenu)); }
            return md;
        }


        #region Business Function

        public string CustomerAddressText(TblCustomerAddress ob)
        {
            if ((ob.SubDistrict == null && ob.SubDistrictId > 0) || (ob.SubDistrictId > 0 && ob.SubDistrict.SubDistrictId != ob.SubDistrictId))
            { ob.SubDistrict = uow.Modules.SubDistrict.Get(ob.SubDistrictId.Value); }
            return ob.AddressText;
        }

        #endregion Business Function

        #region Utility functions
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
                SubMenus = ob.InverseParent.Select(x => ConvertToMenuModel(x)).OrderBy(o=>o.MenuOrder).ToList()
            };
            return rs;
        }

        private MenuModel ConvertToMenuModel2(SysMenu ob)
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
                //  SubMenus = ob.InverseParent.Select(x => ConvertToMenuModel(x)).OrderBy(o => o.MenuOrder).ToList()
            };
            return rs;
        }
        private MenuModel ConvertToMenuModel(SysMenu ob, List<SysMenu> menuAll)
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
                SubMenus = menuAll.Where(o => o.ParentId == ob.MenuId).Select(x => ConvertToMenuModel2(x)).OrderBy(o => o.MenuOrder).ToList()
            };
            return rs;
        }
        #endregion
    }
}
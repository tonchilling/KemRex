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
using Kemrex.Core.Common.Modules;

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


        public AccountPermission GetPermission(long? UserId)
        {
            Team checkteam = null;
            AccountPermission permission = new AccountPermission();

            
             checkteam = uow.Modules.TeamSale.CheckTeamSale(UserId.Value);

            if (UserId.Value == 1 || checkteam != null) //admin
            {
                permission = GetPermissionSale(CurrentUser.AccountId, UserId.HasValue ? UserId.Value : 0);
                permission.TeamType = TeamType.Sale;
            }
            else {
                checkteam = uow.Modules.TeamOperation.CheckTeamOperation(UserId.Value); //check team for non-manager

                permission = GetPermissionOperation(UserId.HasValue ? UserId.Value : 0,null);
                permission.TeamType = TeamType.Operation;
            }

            if (UserId.Value == 1)
            {
                permission.TeamType = TeamType.Admin;
            }





            return permission;
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
        public AccountPermission GetPermissionSale(long accountId, long accountId2)
        {
            AccountPermission ap = new AccountPermission();
            if (accountId == 1) //admin
            {
                ap.IsManager = true;
                ap.IsTeam = true;
                ap.IsEdit = true;
                ap.IsAdminTeam = true;
                return ap;
            }
            TeamSale manager = uow.Modules.TeamSale.Manager(accountId);  //check is manager
            if (accountId2 == 0)
            {
                if (manager != null) ap.IsManager = true;
                else ap.IsManager = false;
                ap.IsTeam = true;
                ap.IsEdit = true;
                ap.IsAdminTeam = false;
                return ap;
            }
            Team checkteam = uow.Modules.TeamSale.CheckTeamSale(accountId);
            Team checkteam2 = uow.Modules.TeamSale.CheckTeamSale(accountId2);
            if (manager != null)
            {
                ap.IsManager = true;
                if (checkteam2 == null)
                {
                    ap.IsEdit = false;   //owner manager can edit
                    ap.IsAdminTeam = false;
                }
                else
                {
                    if (manager.ManagerId == checkteam2.ManagerId) ////team manager for owner transaction
                    {
                        ap.IsTeam = true;
                        ap.IsEdit = true;   //owner manager can edit
                        ap.IsAdminTeam = false;
                    }
                    else
                    {
                        ap.IsTeam = false;
                        ap.IsEdit = false;   //owner manager can edit
                        ap.IsAdminTeam = false;
                    }
                }
            }
            else
            {
                ap.IsManager = false;
                if (accountId == accountId2)  //check account login = owner transaction
                {
                    ap.IsTeam = true;
                    ap.IsEdit = true;    //owner can edit
                    ap.IsAdminTeam = false;
                }
                else
                {
                    if (checkteam == null || checkteam2 == null)
                    {
                        ap.IsTeam = false;
                        ap.IsEdit = false;
                        ap.IsAdminTeam = false;
                    }
                    else
                    {
                        if (checkteam.TeamId == checkteam2.TeamId)  //check team account login = team owner transaction
                        {
                            ap.IsTeam = true;
                            ap.IsEdit = false;     //team same but cannot edit
                            ap.IsAdminTeam = false;
                        }
                        else
                        {
                            ap.IsTeam = false;
                            ap.IsEdit = false;
                            ap.IsAdminTeam = false;
                        }
                    }
                }
            }
            return ap;
        }
        public AccountPermission GetPermissionOperation(long accountId, TeamOperation jobTeam)
        {
            AccountPermission ap = new AccountPermission();
            if (accountId == 1) //admin
            {
                ap.IsManager = true;
                ap.IsTeam = true;
                ap.IsEdit = true;
                ap.IsAdminTeam = true;
                return ap;
            }
            List<TeamOperation> manager = uow.Modules.TeamOperation.Manager(accountId);  //check manager
            Team checkteam = uow.Modules.TeamOperation.CheckTeamOperation(accountId); //check team for non-manager
           
            //// Team Operation Admin
            if (checkteam != null)
            {
                if (checkteam.TeamId == 1)  ///teamId 1 only
                {
                    if (manager != null) ap.IsManager = true;
                    else ap.IsManager = false;
                    ap.IsTeam = false;
                    ap.IsEdit = true;
                    ap.IsAdminTeam = true;
                    return ap;
                }
            }
            //no jobTeam 
            if (jobTeam == null)   //This job are not assignment to team
            {
                if (manager != null) ap.IsManager = true;
                else ap.IsManager = false;
                ap.IsTeam = false;
                ap.IsEdit = false;
                ap.IsAdminTeam = false;
                return ap;
            }
            else  //this job assignment team
            {
                if (manager != null) 
                {
                    ap.IsManager = true;
                    if (checkteam != null)
                    {
                        if (checkteam.TeamId == jobTeam.TeamId)  //team curenent userid  = job Team
                        {
                            ap.IsTeam = true;
                            ap.IsEdit = true;   //owner manager can edit
                            ap.IsAdminTeam = false;
                        }
                        else //team curenent userid  != job Team
                        {
                            ap.IsTeam = false;
                            ap.IsEdit = false;   //manager but team not match jobteam
                            ap.IsAdminTeam = false;
                        }
                    }
                    else
                    {
                        foreach (TeamOperation m in manager)
                        {
                            if (m.ManagerId == jobTeam.ManagerId)
                            {
                                ap.IsTeam = true;
                                ap.IsEdit = true;   //owner manager can edit
                                ap.IsAdminTeam = false;
                                return ap;
                            }
                        }
                        
                        ap.IsTeam = false;
                        ap.IsEdit = false;   //other manager can't edit
                        ap.IsAdminTeam = false;
                                             
                    }

                }
                else
                {
                    ap.IsManager = false;
                    if (checkteam.TeamId == jobTeam.TeamId)  //team curenent userid  = job Team
                    {
                        ap.IsTeam = true;
                        ap.IsEdit = false;   //owner team
                        ap.IsAdminTeam = false;
                    }
                    else  //team curenent userid  != job Team
                    {
                        ap.IsTeam = false;
                        ap.IsEdit = false;   //manager but team not match jobteam
                        ap.IsAdminTeam = false;
                    }
                }
            }
            return ap;
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


        public TransactionStatus GetTransactionStatus()
        {
            TransactionStatus md = new TransactionStatus();
            md= uow.Modules.System.GetAllStatus(CurrentUser.AccountId.ToString());
            return md;
        }
        #endregion
    }
}
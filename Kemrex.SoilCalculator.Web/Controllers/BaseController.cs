using DMN.Standard.Common.Extensions;
using Kemrex.Database;
using Kemrex.FWCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class BaseController : Controller
    {
        private dbContext _db { get; set; }
        public dbContext db
        {
            get
            {
                if (_db == null) { _db = new dbContext(); }
                return _db;
            }
        }
        
        private SysAccount _currentUser { get; set; }
        public string MVCArea { get { return Request.RequestContext.RouteData.DataTokens["area"]?.ToString(); } }
        public string MVCController { get { return Request.RequestContext.RouteData.Values["controller"].ToString(); } }
        public string MVCAction { get { return Request.RequestContext.RouteData.Values["action"].ToString(); } }
        public DateTime CurrentDate { get { return DateTime.Now; } }
        public SysAccount UserSigned()
        {
            if (Session["sid"] != null && (_currentUser == null || Session["sid"].Convert2String().ParseInt() != _currentUser.AccountId))
            {
                UnitOfWork uw = new UnitOfWork();
                long uid = long.TryParse(Session["sid"]?.ToString(), out uid) ? uid : 0;
                _currentUser = uw.Account.Get(uid);
            }
            return _currentUser;
        }

        public ActionResult MenuSystem(int siteId, string name = "_lyMenu")
        {
            UnitOfWork uw = new UnitOfWork();
            List<SysMenu> md = new List<SysMenu>();
            if (Session["sid"] != null)
            {
                long uid = long.TryParse(Session["sid"]?.ToString(), out uid) ? uid : 0;
                SysAccount usr = uw.Account.Get(uid);
                SysAccountRole role = usr.Role(siteId);
                int roleId = role.RoleId;
                var data = (from d in db.SysMenu
                            join r in db.SysRolePermission on d.MenuId equals r.MenuId
                            where
                                d.MenuLevel == 1
                                && d.SiteId == siteId
                                && d.FlagActive
                                && r.RoleId == roleId
                                && r.PermissionId == 1
                                && r.PermissionFlag
                            orderby d.MenuOrder ascending
                            select d);
                md = data.ToList();
                ViewData["RoleId"] = roleId;
            }
            //if (ViewData["MenuCurrent"] != null) { md.Current = (int)ViewData["MenuCurrent"]; }
            //md.Data = data.ToList();
            return PartialView(name, md);
        }
    }
}
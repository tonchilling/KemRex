using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class SysMenu
    {
        private dbContext _db { get; set; }
        private List<SysMenu> _subMenus { get; set; }
        public SysMenu(bool withDefault = true) : this()
        {

            if (withDefault)
            {
                MenuId = 0;
                SiteId = 1;
                ParentId = null;
                MenuLevel = 1;
                MenuName = string.Empty;
                MenuIcon = string.Empty;
                MenuOrder = 9999;
                MvcArea = string.Empty;
                MvcController = string.Empty;
                MvcAction = string.Empty;
                FlagActive = true;
                UpdatedBy = 0;
                UpdatedDate = DateTime.Now;
            }
        }
        public SysMenu(dbContext db, bool withDefault = true) : this(withDefault)
        {
            _db = db;
        }

        public List<SysMenu> SubMenus(bool refresh = false)
        {
            using (dbContext db = new dbContext())
            {
                if (_subMenus == null || refresh)
                {
                    var data = db.SysMenu.Where(x => x.ParentId == MenuId);
                    _subMenus = data.Count() <= 0 ? new List<SysMenu>() : data.ToList();
                }
                return _subMenus;
            }
        }
        public List<SysMenu> SubMenus(int roleId)
        {
            if (roleId <= 0) { return SubMenus(); }
            using (dbContext db = new dbContext())
            {

                var data = (from d in db.SysMenu
                            join r in db.SysRolePermission on d.MenuId equals r.MenuId
                            where
                                d.MenuLevel == (MenuLevel + 1)
                                && d.ParentId == MenuId
                                && d.SiteId == SiteId
                                && d.FlagActive
                                && r.RoleId == roleId
                                && r.PermissionId == 1
                                && r.PermissionFlag
                            orderby d.MenuOrder ascending
                            select d);
                return data.Count() <= 0 ? new List<SysMenu>() : data.ToList();
            }
        }

        private List<SysMenuPermission> _Permissions { get; set; }
        public List<SysMenuPermission> Permissions(bool refresh = false)
        {
            if (_Permissions == null || refresh)
            {
                using (dbContext db = new dbContext())
                {
                    var data = (from d in db.SysMenuPermission where d.MenuId == MenuId select d);
                    _Permissions = data.Count() <= 0 ? new List<SysMenuPermission>() : data.ToList();
                }
            }
            return _Permissions;
        }
    }
}

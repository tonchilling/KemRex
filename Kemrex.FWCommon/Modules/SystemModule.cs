using Kemrex.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.FWCommon.Modules
{
    public class SystemModule
    {
        private readonly dbContext db;
        public SystemModule(dbContext context)
        {
            db = context;
        }
        public List<SysMenu> GetMenuBySiteId(int siteId, int levelFilter = 1)
        {
            var data = db.SysMenu
                        .Include(x => x.SysMenuPermission)
                        .Where(x => x.SiteId == siteId && (x.MenuLevel == levelFilter || levelFilter == 0))
                        .OrderBy(x => x.MenuOrder);
            return data == null ? new List<SysMenu>() : data.ToList();
        }
        public List<SysMenu> GetMenuByRole(int roleId)
        {
            int siteId = (db.SysRole.Find(roleId) ?? new SysRole()).SiteId;
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
            return data.ToList() ?? new List<SysMenu>();
        }
        public SysParameter GetParameter(string code)
        { return db.SysParameter.Find(code); }
    }
}

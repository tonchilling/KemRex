using Kemrex.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.FWCommon.Modules
{
    public class RoleModule
    {
        private readonly dbContext db;
        public RoleModule(dbContext context)
        {
            db = context;
        }

        public int Counts(int siteId, string src = "")
        {
            var data = db.SysRole.AsQueryable();
            data = Filter(data, siteId, src);
            return data.Count();
        }

        public void Delete(SysRole ob)
        {
            if (IsExist(ob.RoleId))
            { db.SysRole.Remove(ob); }
        }

        public SysRole Get(int id)
        {
            return db.SysRole
                .Where(x => x.RoleId == id)
                .FirstOrDefault() ?? new SysRole();
        }

        public List<SysRole> Gets(int page = 1, int size = 0
            , int siteId = 0, string src = "")
        {
            var data = db.SysRole
                .AsQueryable();
            data = Filter(data, siteId, src);
            data = data
                .OrderBy(x => x.SiteId)
                .ThenBy(x => x.RoleName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<SysRole> Filter(IQueryable<SysRole> data, int siteId, string src)
        {
            if (siteId > 0)
            { data = data.Where(x => x.SiteId == siteId); }
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.RoleName.Contains(src)); }
            return data;
        }

        public bool IsExist(int id)
        { return db.SysRole.Where(x => x.RoleId == id).Count() > 0 ? true : false; }

        public void Set(SysRole ob)
        {
            if (ob.RoleId <= 0)
            { db.SysRole.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }


        #region Role Permission
        public SysRolePermission GetPermission(int roleId, int menuId, int permissionId)
        {
            return db.SysRolePermission
                .Where(x =>
                    x.RoleId == roleId
                    && x.MenuId == menuId
                    && x.PermissionId == permissionId)
                .FirstOrDefault() ?? new SysRolePermission() { MenuId = menuId, PermissionId = permissionId };
        }
        public SysRolePermission GetPermissionsByMenu(int roleId, int menuId)
        {
            return db.SysRolePermission
                .Where(x =>
                    x.RoleId == roleId
                    && x.MenuId == menuId)
                .FirstOrDefault() ?? new SysRolePermission();
        }
        public List<SysRolePermission> GetsPermissionsByRole(int roleId)
        {
            var data = db.SysRolePermission
                .Where(x => x.RoleId == roleId);
            return data.ToList();
        }

        public void SetPermission(SysRolePermission ob)
        {
            if (db.SysRolePermission.Where(x =>
                    x.RoleId == ob.RoleId
                    && x.MenuId == ob.MenuId
                    && x.PermissionId == ob.PermissionId).Count() <= 0)
            { db.SysRolePermission.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
        #endregion
    }
}

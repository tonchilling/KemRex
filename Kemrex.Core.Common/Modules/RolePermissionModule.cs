using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Modules
{
    public class RolePermissionModule : IModule<SysRolePermission>
    {
        private readonly mainContext db;

        public RolePermissionModule(mainContext context)
        {
            db = context;
        }

        public int Counts(int roleId = 0)
        {
            var data = db.SysRolePermission.AsQueryable();
            data = Filter(data, roleId);
            return data.Count();
        }

        public void Delete(SysRolePermission ob)
        {
            if (IsExist(ob.RoleId, ob.MenuId, ob.PermissionId))
            { db.SysRolePermission.Remove(ob); }
        }

        public void DeleteByRole(int roleId)
        {
            var data = db.SysRolePermission
                .Where(x => x.RoleId == roleId);
            if (data.Count() > 0)
            { db.SysRolePermission.RemoveRange(data); }
        }

        public SysRolePermission Get(int roleId, int menuId, int permissionId)
        {
            return db.SysRolePermission
                .Where(x =>
                    x.RoleId == roleId
                    && x.MenuId == menuId
                    && x.PermissionId == permissionId)
                .Include(x => x.Menu)
                .Include(x => x.Role)
                .FirstOrDefault() ?? new SysRolePermission() { MenuId = menuId, PermissionId = permissionId };
        }

        public List<SysRolePermission> Gets(int page = 1, int size = 0
            , int roleId = 0)
        {
            var data = db.SysRolePermission
                .Include(x => x.Menu)
                .AsQueryable();
            data = Filter(data, roleId);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<SysRolePermission> Filter(IQueryable<SysRolePermission> data
            , int roleId = 0)
        {
            if (roleId != 0) { data = data.Where(x => x.RoleId == roleId); }
            return data;
        }

        public bool IsExist(int roleId, int menuId, int permissionId)
        {
            return db.SysRolePermission
                  .Where(x =>
                      x.RoleId == roleId
                      && x.MenuId == menuId
                      && x.PermissionId == permissionId)
                  .Count() > 0 ? true : false;
        }

        public void Set(SysRolePermission ob)
        {
            if (ob.RoleId <= 0)
            { db.SysRolePermission.Add(ob); }
            else { db.SysRolePermission.Update(ob); }
        }
    }
}

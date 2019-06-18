using DMN.Standard.Common.Extensions;
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
    public class RoleModule : IModule<SysRole, int>
    {
        private readonly mainContext db;

        public RoleModule(mainContext context)
        {
            db = context;
        }

        public int Counts(int siteId = 0, string src = "", int flagActive = -1)
        {
            var data = db.SysRole.AsQueryable();
            data = Filter(data, siteId, src, flagActive);
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
                .Include(x => x.SysRolePermission)
                    .ThenInclude(x => x.Menu)
                .FirstOrDefault() ?? new SysRole();
        }

        public List<SysRole> Gets(int page = 1, int size = 0
            , int siteId = 0, string src = "", int flagActive = -1)
        {
            var data = db.SysRole
                .Include(x => x.SysRolePermission)
                    .ThenInclude(x => x.Menu)
                .AsQueryable();
            data = Filter(data, siteId, src, flagActive);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<SysRole> Filter(IQueryable<SysRole> data
            , int siteId = 0, string src = "", int flagActive = -1)
        {
            if (siteId != 0) { data = data.Where(x => x.SiteId == siteId); }
            if (!string.IsNullOrWhiteSpace(src)) { data = data.Where(x => x.RoleName.Contains(src)); }
            if (flagActive != -1) { data = data.Where(x => x.FlagActive == flagActive.Convert2String().ParseBoolean(false)); }
            return data;
        }

        public bool IsExist(int id)
        { return db.SysRole.Where(x => x.RoleId == id).Count() > 0 ? true : false; }

        public bool IsUsed(int id)
        {
            bool isUsed = db.SysAccountRole.Where(x => x.RoleId == id).Count() > 0 ? true : false;
            return isUsed;
        }

        public void Set(SysRole ob)
        {
            if (ob.RoleId <= 0)
            { db.SysRole.Add(ob); }
            else { db.SysRole.Update(ob); }
        }
    }
}

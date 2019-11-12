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
    public class EmployeeUserPermissionModule : IModule<TblEmployeeUserPermission, int>
    {
        private readonly mainContext db;
        public EmployeeUserPermissionModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblEmployeeUserPermission.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblEmployeeUserPermission ob)
        {
            if (IsExist(ob.EmpId))
            { db.TblEmployeeUserPermission.Remove(ob); }
        }

        public TblEmployeeUserPermission Get(int id)
        {
            return db.TblEmployeeUserPermission
                .Where(x => x.EmpId == id)

                .FirstOrDefault() ?? new TblEmployeeUserPermission() { };
        }

        public List<TblEmployeeUserPermission> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblEmployeeUserPermission
                .AsQueryable();
            data = Filter(data, src).OrderBy(x => x.EmpId);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<TblEmployeeUserPermission> Filter(IQueryable<TblEmployeeUserPermission> data, string src)
        {
            if (!string.IsNullOrWhiteSpace(src)) { data = data.Where(x => x.EmpId.ToString().Contains(src)); }
            return data;
        }

        public bool IsExist(int id)
        { return db.TblEmployeeUserPermission.Where(x => x.EmpId == id).Count() > 0 ? true : false; }

        public void Set(TblEmployeeUserPermission ob)
        {
            if (ob.EmpId <= 0 )
            { db.TblEmployeeUserPermission.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }

        public void Set(List<TblEmployeeUserPermission> ob)
        {
            if (ob != null && ob.Count>0)
            {
                db.TblEmployeeUserPermission.RemoveRange(db.TblEmployeeUserPermission.Where(o => o.FunId == ob[0].FunId && o.EmpId== ob[0].EmpId));
                db.TblEmployeeUserPermission.AddRange(ob);
            }
        }

    }
}

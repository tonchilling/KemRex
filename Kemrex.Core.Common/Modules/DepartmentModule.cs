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
    public class DepartmentModule : IModule<TblDepartment, int>
    {
        private readonly mainContext db;
        public DepartmentModule(mainContext context)
        {
            db = context;
        }

        public int Counts(string src = "")
        {
            var data = db.TblDepartment.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblDepartment ob)
        {
            if (IsExist(ob.DepartmentId))
            { db.TblDepartment.Remove(ob); }
        }

        public TblDepartment Get(int id)
        {
            return db.TblDepartment
                .Where(x => x.DepartmentId == id)
                .Include(x => x.TblDepartmentPosition)
                .FirstOrDefault() ?? new TblDepartment() { TblDepartmentPosition = new List<TblDepartmentPosition>() };
        }

        public List<TblDepartment> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblDepartment
                .AsQueryable();
            data = Filter(data, src);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<TblDepartment> Filter(IQueryable<TblDepartment> data, string src)
        {
            if (!string.IsNullOrWhiteSpace(src)) { data = data.Where(x => x.DepartmentName.Contains(src)); }
            return data;
        }

        public bool IsExist(int id)
        { return db.TblDepartment.Where(x => x.DepartmentId == id).Count() > 0 ? true : false; }

        public void Set(TblDepartment ob)
        {
            if (ob.DepartmentId <= 0)
            { db.TblDepartment.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

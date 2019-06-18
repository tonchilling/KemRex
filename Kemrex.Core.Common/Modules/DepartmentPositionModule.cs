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
    public class DepartmentPositionModule : IModule<TblDepartmentPosition, int>
    {
        private readonly mainContext db;
        public DepartmentPositionModule(mainContext context)
        {
            db = context;
        }

        public int Counts(int departmentId = 0, string src = "")
        {
            var data = db.TblDepartmentPosition.AsQueryable();
            data = Filter(data, departmentId, src);
            return data.Count();
        }

        public void Delete(TblDepartmentPosition ob)
        {
            if (IsExist(ob.PositionId))
            { db.TblDepartmentPosition.Remove(ob); }
        }

        public TblDepartmentPosition Get(int id)
        {
            return db.TblDepartmentPosition
                .Where(x => x.PositionId == id)
                .Include(x => x.Department)
                .FirstOrDefault() ?? new TblDepartmentPosition() { Department = new TblDepartment() };
        }

        public List<TblDepartmentPosition> Gets(int page = 1, int size = 0
            , int departmentId = 0, string src = "")
        {
            var data = db.TblDepartmentPosition
                .AsQueryable();
            data = Filter(data, departmentId, src);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<TblDepartmentPosition> Filter(IQueryable<TblDepartmentPosition> data, int departmentId, string src)
        {
            if (departmentId > 0) { data = data.Where(x => x.DepartmentId == departmentId); }
            if (!string.IsNullOrWhiteSpace(src)) { data = data.Where(x => x.PositionName.Contains(src)); }
            return data;
        }

        public bool IsExist(int id)
        { return db.TblDepartmentPosition.Where(x => x.PositionId == id).Count() > 0 ? true : false; }

        public void Set(TblDepartmentPosition ob)
        {
            if (ob.PositionId <= 0)
            { db.TblDepartmentPosition.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

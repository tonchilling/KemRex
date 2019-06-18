using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class UnitModule : IModule<TblUnit, int>
    {
        private readonly mainContext db;
        public UnitModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblUnit.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblUnit ob)
        {
            if (IsExist(ob.UnitId))
            { db.TblUnit.Remove(ob); }
        }

        private IQueryable<TblUnit> Filter(IQueryable<TblUnit> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.UnitName.Contains(src)); }
            return data;
        }

        public TblUnit Get(int id)
        {
            return db.TblUnit
                .Where(x => x.UnitId == id).FirstOrDefault() ?? new TblUnit();
        }

        public List<TblUnit> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblUnit
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.UnitName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblUnit.Where(x => x.UnitId == id).Count() > 0 ? true : false; }

        public void Set(TblUnit ob)
        {
            if (ob.UnitId == 0)
            { db.TblUnit.Add(ob); }
            else { db.TblUnit.Update(ob); }
        }
    }
}

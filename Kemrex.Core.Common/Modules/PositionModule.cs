using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class PositionModule : IModule<TblPosition, int>
    {
        private readonly mainContext db;
        public PositionModule(mainContext context)
        {
            db = context;
        }

        public int Counts(string src = "")
        {
            var data = db.TblPosition.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblPosition ob)
        {
            if (IsExist(ob.PositionId))
            { db.TblPosition.Remove(ob); }
        }

        public TblPosition Get(int id)
        {
            return db.TblPosition
                .Where(x => x.PositionId == id)
                .FirstOrDefault() ?? new TblPosition();
        }

        public List<TblPosition> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblPosition
                .AsQueryable();
            data = Filter(data, src);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<TblPosition> Filter(IQueryable<TblPosition> data, string src)
        {
            if (!string.IsNullOrWhiteSpace(src)) { data = data.Where(x => x.PositionName.Contains(src)); }
            return data;
        }

        public bool IsExist(int id)
        { return db.TblPosition.Where(x => x.PositionId == id).Count() > 0 ? true : false; }

        public void Set(TblPosition ob)
        {
            if (ob.PositionId <= 0)
            { db.TblPosition.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

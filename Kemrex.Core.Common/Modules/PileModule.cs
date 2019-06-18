using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class PileModule : IModule<TblPile, int>
    {
        private readonly mainContext db;
        public PileModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblPile.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblPile ob)
        {
            if (IsExist(ob.PileId))
            { db.TblPile.Remove(ob); }
        }

        private IQueryable<TblPile> Filter(IQueryable<TblPile> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.PileName.Contains(src)); }
            return data;
        }

        public TblPile Get(int id)
        {
            return db.TblPile
                .Include(x => x.Series)
                .Where(x => x.PileId == id).FirstOrDefault() ?? new TblPile() { Series = new TblPileSeries() };
        }

        public List<TblPile> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblPile
                .Include(x => x.Series)
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.Series.SeriesOrder)
                    .ThenBy(x => x.PileSeriesOrder);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblPile.Where(x => x.PileId == id).Count() > 0 ? true : false; }

        public void Set(TblPile ob)
        {
            if (ob.PileId == 0)
            { db.TblPile.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

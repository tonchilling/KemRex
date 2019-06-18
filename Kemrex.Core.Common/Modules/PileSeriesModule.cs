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
    public class PileSeriesModule : IModule<TblPileSeries, int>
    {
        private readonly mainContext db;
        public PileSeriesModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblPileSeries.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblPileSeries ob)
        {
            if (IsExist(ob.SeriesId))
            { db.TblPileSeries.Remove(ob); }
        }

        private IQueryable<TblPileSeries> Filter(IQueryable<TblPileSeries> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.SeriesName.Contains(src)); }
            return data;
        }

        public TblPileSeries Get(int id)
        {
            return db.TblPileSeries
                .Include(x => x.TblPile)
                .Where(x => x.SeriesId == id).FirstOrDefault() ?? new TblPileSeries() { TblPile = new List<TblPile>() };
        }

        public List<TblPileSeries> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblPileSeries
                .Include(x => x.TblPile)
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.SeriesOrder);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblPileSeries.Where(x => x.SeriesId == id).Count() > 0 ? true : false; }

        public void Set(TblPileSeries ob)
        {
            if (ob.SeriesId == 0)
            { db.TblPileSeries.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

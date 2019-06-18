using Kemrex.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.FWCommon.Modules
{
    public class ModelInfoModule
    {
        private dbContext db;
        public ModelInfoModule(dbContext _db)
        {
            db = _db;
        }

        public TblPile Get(int id)
        {
            return db.TblPile
                  .Include(x => x.TblPileSeries)
                  .Where(x => x.PileId == id).FirstOrDefault(); }

        public List<TblPileSeries> GetModelGroups()
        {
            var data = db.TblPileSeries.OrderBy(x => x.SeriesOrder);
            return data.ToList();
        }

        public List<TblPile> GetModel(int seriesId = 0)
        {
            var data = db.TblPile
                .AsQueryable();
            if (seriesId > 0)
            { data = data.Where(x => x.SeriesId == seriesId).OrderBy(x => x.PileSeriesOrder); }
            return data.ToList();
        }
    }
}

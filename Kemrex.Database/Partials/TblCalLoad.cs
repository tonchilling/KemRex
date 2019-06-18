using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class TblCalLoad
    {
        public TblPile PileModel()
        {
            TblPile result;
            using (dbContext db = new dbContext())
            {
                var data = db.TblPile
                    .Include(x => x.TblPileSeries)
                    .Where(x => x.PileId == ModelId)
                    .FirstOrDefault();
                result = data == null ? new TblPile() { TblPileSeries = new TblPileSeries() } : data;
            }
            return result;
        }
    }
}

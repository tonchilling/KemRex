using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class TblKptStation
    {
        public int BlowCountDepth(int depth)
        {
            int result = 0;
            if (StationId > 0)
            {
                using (dbContext db = new dbContext())
                {
                    var data = db.TblKptStationDetail.Where(x => x.StationId == StationId && x.StationDepth == depth).FirstOrDefault();
                    if (data != null) { result = data.StationBlowCount; }
                }
            }
            return result;
        }

        [NotMapped]
        public int StationEndDepth
        {
            get
            {
                int rs = 0;
                if (TblKptStationDetail != null)
                {
                    int maxDepth = TblKptStationDetail.Where(x => x.StationBlowCount > 0).Count() > 0 ?
                        TblKptStationDetail.Where(x => x.StationBlowCount > 0).Max(x => x.StationDepth) : 0;
                    rs = (maxDepth / 5) + ((maxDepth % 5) > 0 ? 1 : 0);
                }
                return rs;
            }
        }

        [NotMapped]
        protected int _SumCount { get; set; }
        [NotMapped]
        protected decimal _SumQu { get; set; }
        private void EndDepthSummary()
        {
            _SumCount = 0;
            _SumQu = 0;
            if (TblKptStationDetail != null && StationEndDepth > 0)
            {
                for (int j = (5 * (StationEndDepth - 1)) + 1; j <= (5 * (StationEndDepth - 1)) + 5; j++)
                {
                    var tmp = TblKptStationDetail.Where(x => x.StationDepth == j).FirstOrDefault();
                    int blowCount = tmp.StationBlowCount;
                    int blowCountN = (int)(blowCount > 15 ? 15 + Math.Floor(0.5 * (blowCount - 15)) : blowCount);
                    decimal qu = blowCountN <= 0 ? 0 : (decimal)1.92 * (blowCountN + (decimal)0.954);
                    qu = Math.Floor(qu * 100) / 100;
                    _SumCount += blowCount;
                    _SumQu += qu;
                }
            }
        }

        [NotMapped]
        public decimal StationAvgBlowCount
        {
            get
            {
                if (_SumCount <= 0) { EndDepthSummary(); }
                return Math.Floor(((decimal)_SumCount / 5) * 100) / 100;
            }
        }
        [NotMapped]
        public decimal StationAvgQu
        {
            get
            {
                if (_SumQu <= 0) { EndDepthSummary(); }
                return Math.Floor((_SumQu / 5) * 100) / 100;
            }
        }
        [NotMapped]
        public decimal StationCohesion { get { return Math.Floor((StationAvgQu / 2) * 100) / 100; } }
    }
}

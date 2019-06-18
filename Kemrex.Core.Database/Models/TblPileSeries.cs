using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblPileSeries
    {
        public TblPileSeries()
        {
            TblPile = new HashSet<TblPile>();
        }

        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string SeriesImage { get; set; }
        public int SeriesOrder { get; set; }

        public virtual ICollection<TblPile> TblPile { get; set; }
    }
}

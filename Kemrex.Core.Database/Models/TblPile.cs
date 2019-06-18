using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblPile
    {
        public int PileId { get; set; }
        public int? SeriesId { get; set; }
        public string PileName { get; set; }
        public int PileLength { get; set; }
        public decimal PileDia { get; set; }
        public decimal PileBlade { get; set; }
        public decimal PileSpiralLength { get; set; }
        public decimal PileSpiralDepth { get; set; }
        public decimal PileFlangeWidth { get; set; }
        public decimal PileFlangeLength { get; set; }
        public float? PilePrice { get; set; }
        public int PileSeriesOrder { get; set; }

        public virtual TblPileSeries Series { get; set; }
    }
}

namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblPile")]
    public partial class TblPile
    {
        [Key]
        public int PileId { get; set; }

        public int? SeriesId { get; set; }

        [Required]
        [StringLength(100)]
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

        public virtual TblPileSeries TblPileSeries { get; set; }
    }
}

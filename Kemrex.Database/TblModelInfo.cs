namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblModelInfo")]
    public partial class TblModelInfo
    {
        [Key]
        public int ModelId { get; set; }

        public int? SeriesId { get; set; }

        [Required]
        [StringLength(50)]
        public string ModelGroup { get; set; }

        [Required]
        [StringLength(100)]
        public string ModelName { get; set; }

        public int ModelLength { get; set; }

        public decimal ModelDia { get; set; }

        public decimal ModelBlade { get; set; }

        public decimal ModelSpiralLength { get; set; }

        public decimal ModelSpiralDepth { get; set; }

        public decimal ModelFlangeWidth { get; set; }

        public decimal ModelFlangeLength { get; set; }

        public int ModelSeriesOrder { get; set; }

        public virtual TblModelSeries TblModelSeries { get; set; }
    }
}

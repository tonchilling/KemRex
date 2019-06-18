namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblCalLoad")]
    public partial class TblCalLoad
    {
        [Key]
        public int CalId { get; set; }

        [StringLength(200)]
        public string ProjectName { get; set; }

        public string CalRemark { get; set; }

        public decimal InputC { get; set; }

        public decimal InputDegree { get; set; }

        public decimal InputSafeLoad { get; set; }

        public int ModelId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

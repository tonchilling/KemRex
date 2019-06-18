namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblProductType")]
    public partial class TblProductType
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [StringLength(200)]
        public string TypeName { get; set; }

        public string TypeDetail { get; set; }

        public int TypeOrder { get; set; }

        public bool FlagActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

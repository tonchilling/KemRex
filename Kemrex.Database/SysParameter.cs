namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysParameter")]
    public partial class SysParameter
    {
        [Key]
        [StringLength(100)]
        public string ParamName { get; set; }

        [Required]
        public string ParamValue { get; set; }

        [Required]
        [StringLength(50)]
        public string ParamType { get; set; }

        public int? ParamLength { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

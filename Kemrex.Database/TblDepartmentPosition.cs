namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblDepartmentPosition")]
    public partial class TblDepartmentPosition
    {
        [Key]
        public int PositionId { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        [StringLength(200)]
        public string PositionName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual TblDepartment TblDepartment { get; set; }
    }
}

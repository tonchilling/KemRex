namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblEmployee")]
    public partial class TblEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblEmployee()
        {
            TblEmployee1 = new HashSet<TblEmployee>();
            TblQuotation = new HashSet<TblQuotation>();
            TblSaleOrder = new HashSet<TblSaleOrder>();
        }

        [Key]
        public int EmpId { get; set; }

        [Required]
        [StringLength(20)]
        public string EmpCode { get; set; }

        public long? AccountId { get; set; }

        public int? PrefixId { get; set; }

        public int? EmpTypeId { get; set; }

        [Required]
        [StringLength(500)]
        public string EmpNameTH { get; set; }

        [Required]
        [StringLength(500)]
        public string EmpNameEN { get; set; }

        [StringLength(13)]
        public string EmpPID { get; set; }

        [Required]
        [StringLength(10)]
        public string EmpMobile { get; set; }

        [StringLength(250)]
        public string EmpEmail { get; set; }

        public int? DepartmentId { get; set; }

        public int? PositionId { get; set; }

        public int? LeadId { get; set; }

        public DateTime? EmpApplyDate { get; set; }

        public DateTime? EmpPromoteDate { get; set; }

        [StringLength(500)]
        public string EmpAddress { get; set; }

        [StringLength(10)]
        public string EmpPostcode { get; set; }

        [StringLength(300)]
        public string EmpSignature { get; set; }

        public string EmpRemark { get; set; }

        public int StatusId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual EnmPrefix EnmPrefix { get; set; }

        public virtual EnmStatusEmployee EnmStatusEmployee { get; set; }

        public virtual SysAccount SysAccount { get; set; }

        public virtual TblDepartment TblDepartment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblEmployee> TblEmployee1 { get; set; }

        public virtual TblEmployee TblEmployee2 { get; set; }

        public virtual TblPosition TblPosition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblQuotation> TblQuotation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSaleOrder> TblSaleOrder { get; set; }
    }
}

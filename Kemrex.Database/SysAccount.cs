namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysAccount")]
    public partial class SysAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysAccount()
        {
            CalcAccountStaff = new HashSet<CalcAccountStaff>();
            CalcAccountStaff1 = new HashSet<CalcAccountStaff>();
            SysAccountRole = new HashSet<SysAccountRole>();
            TblEmployee = new HashSet<TblEmployee>();
        }

        [Key]
        public long AccountId { get; set; }

        public string AccountAvatar { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountUsername { get; set; }

        [StringLength(100)]
        public string AccountPassword { get; set; }

        [Required]
        [StringLength(200)]
        public string AccountFirstName { get; set; }

        [StringLength(200)]
        public string AccountLastName { get; set; }

        [Required]
        [StringLength(200)]
        public string AccountEmail { get; set; }

        public string AccountRemark { get; set; }

        public int FlagStatus { get; set; }

        public bool FlagSystem { get; set; }

        public bool FlagAdminCalc { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalcAccountStaff> CalcAccountStaff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalcAccountStaff> CalcAccountStaff1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysAccountRole> SysAccountRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblEmployee> TblEmployee { get; set; }
    }
}

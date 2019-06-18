namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysRole")]
    public partial class SysRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysRole()
        {
            SysAccountRole = new HashSet<SysAccountRole>();
            SysRolePermission = new HashSet<SysRolePermission>();
        }

        [Key]
        public int RoleId { get; set; }

        public int SiteId { get; set; }

        [Required]
        [StringLength(150)]
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool FlagSystem { get; set; }

        public bool FlagActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysAccountRole> SysAccountRole { get; set; }

        public virtual SysSite SysSite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysRolePermission> SysRolePermission { get; set; }
    }
}

namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysSite")]
    public partial class SysSite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysSite()
        {
            SysMenu = new HashSet<SysMenu>();
            SysRole = new HashSet<SysRole>();
        }

        [Key]
        public int SiteId { get; set; }

        [Required]
        [StringLength(100)]
        public string SiteName { get; set; }

        public string SiteDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysMenu> SysMenu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysRole> SysRole { get; set; }
    }
}

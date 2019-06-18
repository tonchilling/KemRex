namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysMenu")]
    public partial class SysMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysMenu()
        {
            SysMenu1 = new HashSet<SysMenu>();
            SysMenuPermission = new HashSet<SysMenuPermission>();
            SysRolePermission = new HashSet<SysRolePermission>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuId { get; set; }

        public int SiteId { get; set; }

        public int MenuLevel { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuName { get; set; }

        [StringLength(500)]
        public string MenuIcon { get; set; }

        public int MenuOrder { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(100)]
        public string MvcArea { get; set; }

        [Required]
        [StringLength(100)]
        public string MvcController { get; set; }

        [Required]
        [StringLength(100)]
        public string MvcAction { get; set; }

        public bool FlagActive { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysMenu> SysMenu1 { get; set; }

        public virtual SysMenu SysMenu2 { get; set; }

        public virtual SysSite SysSite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysMenuPermission> SysMenuPermission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysRolePermission> SysRolePermission { get; set; }
    }
}

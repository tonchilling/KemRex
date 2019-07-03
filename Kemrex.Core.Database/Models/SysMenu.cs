using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{
    public partial class SysMenu
    {
        public SysMenu()
        {
            InverseParent = new HashSet<SysMenu>();
            SysMenuActive = new HashSet<SysMenuActive>();
            SysMenuPermission = new HashSet<SysMenuPermission>();
            SysRolePermission = new HashSet<SysRolePermission>();
        }

        public int MenuId { get; set; }
        public int SiteId { get; set; }
        public int MenuLevel { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public int MenuOrder { get; set; }
        public int? ParentId { get; set; }
        public string MvcArea { get; set; }
        public string MvcController { get; set; }
        public string MvcAction { get; set; }
        public bool? FlagActive { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        [NotMapped]
        public int View { get; set; }
        [NotMapped]
        public int Edit { get; set; }
        [NotMapped]
        public int Delete { get; set; }

        public virtual SysMenu Parent { get; set; }
        public virtual SysSite Site { get; set; }
        public virtual ICollection<SysMenu> InverseParent { get; set; }
        public virtual ICollection<SysMenuActive> SysMenuActive { get; set; }
        public virtual ICollection<SysMenuPermission> SysMenuPermission { get; set; }
        public virtual ICollection<SysRolePermission> SysRolePermission { get; set; }
    }
}

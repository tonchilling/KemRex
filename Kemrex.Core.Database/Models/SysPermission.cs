using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysPermission
    {
        public SysPermission()
        {
            SysMenuPermission = new HashSet<SysMenuPermission>();
            SysRolePermission = new HashSet<SysRolePermission>();
        }

        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionRemark { get; set; }

        public virtual ICollection<SysMenuPermission> SysMenuPermission { get; set; }
        public virtual ICollection<SysRolePermission> SysRolePermission { get; set; }
    }
}

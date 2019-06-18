using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysRolePermission
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int PermissionId { get; set; }
        public bool PermissionFlag { get; set; }

        public virtual SysMenu Menu { get; set; }
        public virtual SysPermission Permission { get; set; }
        public virtual SysRole Role { get; set; }
    }
}

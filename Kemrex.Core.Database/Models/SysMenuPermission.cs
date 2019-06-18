using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysMenuPermission
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int PermissionId { get; set; }

        public virtual SysMenu Menu { get; set; }
        public virtual SysPermission Permission { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysRole
    {
        public SysRole()
        {
            SysAccountRole = new HashSet<SysAccountRole>();
            SysRolePermission = new HashSet<SysRolePermission>();
        }

        public int RoleId { get; set; }
        public int SiteId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool FlagSystem { get; set; }
        public bool? FlagActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual SysSite Site { get; set; }
        public virtual ICollection<SysAccountRole> SysAccountRole { get; set; }
        public virtual ICollection<SysRolePermission> SysRolePermission { get; set; }
    }
}

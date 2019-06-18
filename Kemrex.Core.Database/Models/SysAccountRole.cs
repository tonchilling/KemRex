using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysAccountRole
    {
        public int Id { get; set; }
        public long AccountId { get; set; }
        public int RoleId { get; set; }

        public virtual SysAccount Account { get; set; }
        public virtual SysRole Role { get; set; }
    }
}

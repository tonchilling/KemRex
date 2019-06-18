using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysSite
    {
        public SysSite()
        {
            SysMenu = new HashSet<SysMenu>();
            SysRole = new HashSet<SysRole>();
        }

        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteDetail { get; set; }

        public virtual ICollection<SysMenu> SysMenu { get; set; }
        public virtual ICollection<SysRole> SysRole { get; set; }
    }
}

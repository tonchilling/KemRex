using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysMenuActive
    {
        public int Id { get; set; }
        public string MvcArea { get; set; }
        public string MvcController { get; set; }
        public string MvcAction { get; set; }
        public int MenuId { get; set; }

        public virtual SysMenu Menu { get; set; }
    }
}

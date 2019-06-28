using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysCategoryType
    {
        public SysCategoryType()
        {
            SysCategory = new HashSet<SysCategory>();
        }

        public int CategoryTypeId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<SysCategory> SysCategory { get; set; }
    }
}

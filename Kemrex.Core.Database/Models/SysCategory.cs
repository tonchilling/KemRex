using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysCategory
    {
        public SysCategory()
        {
            TblJobOrderProjectType = new HashSet<TblJobOrderProjectType>();
        }

        public int CategoryId { get; set; }
        public int? CategoryTypeId { get; set; }
        public string CategoryName { get; set; }

        public virtual SysCategoryType CategoryType { get; set; }
        public virtual ICollection<TblJobOrderProjectType> TblJobOrderProjectType { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblProductModel
    {
        public TblProductModel()
        {
            TblProduct = new HashSet<TblProduct>();
        }

        public int ModelId { get; set; }
        public int CategoryId { get; set; }
        public string ModelName { get; set; }
        public int ModelOrder { get; set; }
        public bool? FlagActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TblProductCategory Category { get; set; }
        public virtual ICollection<TblProduct> TblProduct { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblProductCategory
    {
        public TblProductCategory()
        {
            TblProduct = new HashSet<TblProduct>();
            TblProductModel = new HashSet<TblProductModel>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDetail { get; set; }
        public string Accessory { get; set; }
        public int CategoryOrder { get; set; }
        public bool? FlagActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TblProduct> TblProduct { get; set; }
        public virtual ICollection<TblProductModel> TblProductModel { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    [Serializable]
    public partial class TblUnit
    {
        public TblUnit()
        {
            TblProduct = new HashSet<TblProduct>();
        }

        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitDetail { get; set; }
        public bool? FlagActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TblProduct> TblProduct { get; set; }
    }
}

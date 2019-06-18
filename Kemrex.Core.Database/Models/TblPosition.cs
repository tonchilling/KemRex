using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblPosition
    {
        public TblPosition()
        {
            TblEmployee = new HashSet<TblEmployee>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TblEmployee> TblEmployee { get; set; }
    }
}

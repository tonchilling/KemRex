using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblProductType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeDetail { get; set; }
        public int TypeOrder { get; set; }
        public bool? FlagActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

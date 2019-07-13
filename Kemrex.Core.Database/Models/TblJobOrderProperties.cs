using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderProperties
    {
        public int JobOrderId { get; set; }
        public int No { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }

        public virtual TblProduct Product { get; set; }


    }
}

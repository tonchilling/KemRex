using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderDetail
    {
        public int JobOrderId { get; set; }
        public int No { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }

        public virtual TblJobOrder JobOrder { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TransferDetail
    {
        public string TransferNo { get; set; }
        public short Seq { get; set; }
        public int? ProductId { get; set; }
        public string CurrentQty { get; set; }
        public int? RequestQty { get; set; }
        public string RequestUnit { get; set; }
        public decimal? RequestUnitFactor { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual TblProduct Product { get; set; }
    }
}

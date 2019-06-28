using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TransferHeader
    {
        public string TransferNo { get; set; }
        public string TrnasferType { get; set; }
        public string TransferDate { get; set; }
        public string TransferTime { get; set; }
        public string EmpId { get; set; }
        public string BillNo { get; set; }
        public string TransferStatus { get; set; }
        public string Note1 { get; set; }
        public DateTime? LastModified { get; set; }

        public List<TransferDetail> TransferDetail { get; set; }
    }
}

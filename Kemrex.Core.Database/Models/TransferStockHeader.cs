using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TransferStockHeader
    {
        public int TransferStockId { get; set; }
        public string TransferNo { get; set; }
        public string TransferType { get; set; }
        public DateTime? TransferDate { get; set; }
        public string TransferTime { get; set; }
        public string ReceiveTo { get; set; }
        public string Reason { get; set; }
        public int? CarType { get; set; }
        public string Company { get; set; }
        public string CarNo { get; set; }
        public string CarBrand { get; set; }
        public int? SendToDepartment { get; set; }
        public string Remark { get; set; }
        public string EmpId { get; set; }
        public string BillNo { get; set; }
        public int? TransferStatus { get; set; }
        public string Note1 { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? Status { get; set; }

        public List<TransferStockDetail> TransferStockDetail { get; set; }
    }
}

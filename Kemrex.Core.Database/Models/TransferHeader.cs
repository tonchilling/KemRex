using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{
    public partial class TransferHeader
    {

        public int TransferId { get; set; }
        public string TransferNo { get; set; }
        public string TransferType { get; set; }
        public DateTime? TransferDate { get; set; }
        [NotMapped]
        public string StrTransferDate { get; set; }
        
        public string TransferTime { get; set; }
        public int JobOrderId { get; set; }

       
        public int RefTransferId { get; set; }
        public string  ReceiveTo { get; set; }
        public string Reason { get; set; }
        public int CarType { get; set; }
        public string Company { get; set; }
        public string CarNo { get; set; }
        public string CarBrand{ get; set; }
        public int SendToDepartment { get; set; }
        public string Remark { get; set; }
        public string EmpId { get; set; }
        public string BillNo { get; set; }
        public int TransferStatus { get; set; }
        [NotMapped]
        public string StrTransferStatus { get; set; }
        public string Note1 { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ApprovedBy { get; set; }

        [NotMapped]
        public virtual TblJobOrder JobOrder { get; set; }
        [NotMapped]
        public virtual List<TransferDetail> TransferDetail { get; set; }

        [NotMapped]
        public virtual RefTransferHeader RefTransfer { get; set; }
    }


    public partial class RefTransferHeader
    {

        public int TransferId { get; set; }
        public string TransferNo { get; set; }
        public string TransferType { get; set; }
        public DateTime? TransferDate { get; set; }
        public string TransferTime { get; set; }
        public int JobOrderId { get; set; }
        public int RefTransferId { get; set; }
        public string ReceiveTo { get; set; }
        public string Reason { get; set; }
        public int CarType { get; set; }
        public string Company { get; set; }
        public string CarNo { get; set; }
        public string CarBrand { get; set; }
        public int SendToDepartment { get; set; }
        public string Remark { get; set; }
        public string EmpId { get; set; }
        public string BillNo { get; set; }
        public int TransferStatus { get; set; }
        public string Note1 { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [NotMapped]
        public virtual TblJobOrder JobOrder { get; set; }
        [NotMapped]
        public virtual List<TransferDetail> TransferDetail { get; set; }

        [NotMapped]
        public virtual List<TransferRefHeader> TransferRefHeader { get; set; }


    }
}

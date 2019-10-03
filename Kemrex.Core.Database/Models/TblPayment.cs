using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{

    public partial class TblPayment
    {
        public int PaymentId { get; set; }
        public string PaymentNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        [NotMapped]
        public string StrPaymentDate { get; set; }
        public int? InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal PaymentAmount { get; set; }
        public int StatusId { get; set; }
        public string BankPayFrom { get; set; }
        public string BankPayFromBranch { get; set; }
        public int? AcctReceiveId { get; set; }
        public virtual TblBankAccount AcctReceive { get; set; }
        public string PaySlipPath { get; set; }
        [NotMapped]
        public string StrPaySlipPath { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public string StrUpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public string StrCreatedDate { get; set; }
        public long ApprovedBy { get; set; }
    }
}

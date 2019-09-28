using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{

    public partial class TblPayment
    {
        public int PaymentId { get; set; }
        public string PaymentNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal PaymentAmount { get; set; }
        public int StatusId { get; set; }
        public string BankPayFrom { get; set; }
        public string BankPayFromBranch { get; set; }
        public int AcctReceiveId { get; set; }
        public virtual TblBankAccount AcctReceive { get; set; }
        public string PaySlipPath { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ApprovedBy { get; set; }
    }
}

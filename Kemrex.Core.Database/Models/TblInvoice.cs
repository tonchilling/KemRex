using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblInvoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int SaleOrderId { get; set; }
        public string InvoiceRemark { get; set; }
        public int InvoiceTerm { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public int? StatusId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal DepositAmount { get; set; }
        public virtual TblSaleOrder SaleOrder { get; set; }
    }
}

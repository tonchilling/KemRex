using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{
    public partial class TblQuotation
    {
        public TblQuotation()
        {
            TblQuotationDetail = new HashSet<TblQuotationDetail>();
        }

        public int QuotationId { get; set; }
        public string QuotationNo { get; set; }
        public DateTime QuotationDate { get; set; }

        [NotMapped]
        public string strQuotationDate { get; set; }
        public int QuotationValidDay { get; set; }
        public int? ConditionId { get; set; }
        public DateTime? OperationStartDate { get; set; }
        public DateTime? OperationEndDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int QuotationCreditDay { get; set; }
        public int SaleId { get; set; }
        public string SaleName { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContractName { get; set; }
        public string ContractEmail { get; set; }
        public string ContractPhone { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string QuotationRemark { get; set; }
        public decimal? SubTotalNet { get; set; }
        public decimal? SubTotalVat { get; set; }
        public decimal? SubTotalTot { get; set; }
        public decimal? DiscountNet { get; set; }
        public decimal? DiscountVat { get; set; }
        public decimal? DiscountTot { get; set; }
        public decimal DiscountCash { get; set; }
        public decimal? SummaryNet { get; set; }
        public decimal? SummaryVat { get; set; }
        public decimal? SummaryTot { get; set; }
        public int StatusId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedReason { get; set; }

        public virtual TblCustomer Customer { get; set; }
        public virtual TblEmployee Sale { get; set; }
        public virtual EnmStatusQuotation Status { get; set; }
        public virtual ICollection<TblQuotationDetail> TblQuotationDetail { get; set; }
    }
}

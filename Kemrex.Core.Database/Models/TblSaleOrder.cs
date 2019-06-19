using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblSaleOrder
    {
        public TblSaleOrder()
        {
            TblInvoice = new HashSet<TblInvoice>();
            TblSaleOrderAttachment = new HashSet<TblSaleOrderAttachment>();
            TblSaleOrderDetail = new HashSet<TblSaleOrderDetail>();
        }

        public int SaleOrderId { get; set; }
        public string SaleOrderNo { get; set; }
        public DateTime? SaleOrderDate { get; set; }
        public DateTime? OperationStartDate { get; set; }
        public DateTime? OperationEndDate { get; set; }
        public string QuotationNo { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContractName { get; set; }
        public int? ConditionId { get; set; }
        public string PoNo { get; set; }
        public int? SaleId { get; set; }
        public string SaleName { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string SaleOrderRemark { get; set; }
        public int StatusId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CancelReason { get; set; }
        public decimal? SubTotalNet { get; set; }
        public decimal? SubTotalVat { get; set; }
        public decimal? SubTotalTot { get; set; }
        public decimal? DiscountNet { get; set; }
        public decimal? DiscountVat { get; set; }
        public decimal? DiscountTot { get; set; }
        public decimal? DiscountCash { get; set; }
        public decimal? SummaryNet { get; set; }
        public decimal? SummaryVat { get; set; }
        public decimal? SummaryTot { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int SaleOrderCreditDay { get; set; }

        public  List<TblCustomer> Customers { get; set; }

        public virtual TblCustomer Customer { get; set; }
        public virtual TblEmployee Sale { get; set; }
        public virtual ICollection<TblInvoice> TblInvoice { get; set; }
        public virtual ICollection<TblSaleOrderAttachment> TblSaleOrderAttachment { get; set; }
        public virtual ICollection<TblSaleOrderDetail> TblSaleOrderDetail { get; set; }
    }
}

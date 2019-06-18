namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblSaleOrder")]
    public partial class TblSaleOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblSaleOrder()
        {
            TblInvoice = new HashSet<TblInvoice>();
            TblSaleOrderAttachment = new HashSet<TblSaleOrderAttachment>();
            TblSaleOrderDetail = new HashSet<TblSaleOrderDetail>();
        }

        [Key]
        public int SaleOrderId { get; set; }

        [Required]
        [StringLength(20)]
        public string SaleOrderNo { get; set; }

        public DateTime? SaleOrderDate { get; set; }

        [StringLength(20)]
        public string QuotationNo { get; set; }

        public int? CustomerId { get; set; }

        [StringLength(500)]
        public string CustomerName { get; set; }

        [StringLength(500)]
        public string ContractName { get; set; }

        public int? ConditionId { get; set; }

        [StringLength(50)]
        public string PoNo { get; set; }

        public int? SaleId { get; set; }

        [StringLength(500)]
        public string SaleName { get; set; }

        public string BillingAddress { get; set; }

        public string ShippingAddress { get; set; }

        public string SaleOrderRemark { get; set; }

        public int StatusId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CancelReason { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SubTotalNet { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SubTotalVat { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SubTotalTot { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? DiscountNet { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? DiscountVat { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? DiscountTot { get; set; }

        public decimal? DiscountCash { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SummaryNet { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SummaryVat { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SummaryTot { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public int SaleOrderCreditDay { get; set; }

        public virtual TblCustomer TblCustomer { get; set; }

        public virtual TblEmployee TblEmployee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblInvoice> TblInvoice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSaleOrderAttachment> TblSaleOrderAttachment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSaleOrderDetail> TblSaleOrderDetail { get; set; }
    }
}

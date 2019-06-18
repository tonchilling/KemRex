namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblQuotation")]
    public partial class TblQuotation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblQuotation()
        {
            TblQuotationDetail = new HashSet<TblQuotationDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuotationId { get; set; }

        [Required]
        [StringLength(20)]
        public string QuotationNo { get; set; }

        public DateTime QuotationDate { get; set; }

        public int QuotationValidDay { get; set; }

        public int? ConditionId { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public int QuotationCreditDay { get; set; }

        public int SaleId { get; set; }

        [Required]
        [StringLength(500)]
        public string SaleName { get; set; }

        public int? CustomerId { get; set; }

        [StringLength(500)]
        public string CustomerName { get; set; }

        [StringLength(500)]
        public string ContractName { get; set; }

        [StringLength(500)]
        public string ContractEmail { get; set; }

        [StringLength(500)]
        public string ContractPhone { get; set; }

        public string BillingAddress { get; set; }

        public string ShippingAddress { get; set; }

        public string QuotationRemark { get; set; }

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

        public decimal DiscountCash { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SummaryNet { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SummaryVat { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SummaryTot { get; set; }

        public int StatusId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string DeletedReason { get; set; }

        public virtual EnmStatusQuotation EnmStatusQuotation { get; set; }

        public virtual TblCustomer TblCustomer { get; set; }

        public virtual TblEmployee TblEmployee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblQuotationDetail> TblQuotationDetail { get; set; }
    }
}

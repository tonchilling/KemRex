namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblProduct")]
    public partial class TblProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblProduct()
        {
            TblQuotationDetail = new HashSet<TblQuotationDetail>();
            TblSaleOrderDetail = new HashSet<TblSaleOrderDetail>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(20)]
        public string ProductCode { get; set; }

        public int CategoryId { get; set; }

        public int? ModelId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductNameBilling { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductNameTrade { get; set; }

        public int UnitId { get; set; }

        public decimal CostNet { get; set; }

        public decimal CostVat { get; set; }

        public decimal CostTot { get; set; }

        public decimal PriceNet { get; set; }

        public decimal PriceVat { get; set; }

        public decimal PriceTot { get; set; }

        public int QtyStock { get; set; }

        public bool FlagVat { get; set; }

        public bool FlagActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual TblProductCategory TblProductCategory { get; set; }

        public virtual TblProductModel TblProductModel { get; set; }

        public virtual TblUnit TblUnit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblQuotationDetail> TblQuotationDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSaleOrderDetail> TblSaleOrderDetail { get; set; }
    }
}

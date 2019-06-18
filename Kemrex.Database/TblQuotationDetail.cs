namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblQuotationDetail")]
    public partial class TblQuotationDetail
    {
        public int Id { get; set; }

        public int QuotationId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal PriceNet { get; set; }

        public decimal PriceVat { get; set; }

        public decimal PriceTot { get; set; }

        public decimal DiscountNet { get; set; }

        public decimal DiscountVat { get; set; }

        public decimal DiscountTot { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? TotalNet { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? TotalVat { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? TotalTot { get; set; }

        public string Remark { get; set; }

        public virtual TblProduct TblProduct { get; set; }

        public virtual TblQuotation TblQuotation { get; set; }
    }
}

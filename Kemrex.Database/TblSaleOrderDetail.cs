namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblSaleOrderDetail")]
    public partial class TblSaleOrderDetail
    {
        public int Id { get; set; }

        public int SaleOrderId { get; set; }

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

        [StringLength(10)]
        public string Remark { get; set; }

        public virtual TblProduct TblProduct { get; set; }

        public virtual TblSaleOrder TblSaleOrder { get; set; }
    }
}

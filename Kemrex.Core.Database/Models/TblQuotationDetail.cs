using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    [Serializable]
    public partial class TblQuotationDetail
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public int ProductId { get; set; }
        public int? WHId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceNet { get; set; }
        public decimal PriceVat { get; set; }
        public decimal PriceTot { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountNet { get; set; }
        public decimal DiscountVat { get; set; }
        public decimal DiscountTot { get; set; }
        public decimal? TotalNet { get; set; }
        public decimal? TotalVat { get; set; }
        public decimal? TotalTot { get; set; }
        public string Remark { get; set; }
        public int CalType { get; set; }


        public virtual TblWareHouse WareHouse { get; set; }
        public virtual TblProduct Product { get; set; }
        public virtual TblQuotation Quotation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblQuotationDetail = new HashSet<TblQuotationDetail>();
            TblSaleOrderDetail = new HashSet<TblSaleOrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int CategoryId { get; set; }
        public int? ModelId { get; set; }
        public string ProductName { get; set; }
        public string ProductNameBilling { get; set; }
        public string ProductNameTrade { get; set; }
        public int UnitId { get; set; }
        public decimal CostNet { get; set; }
        public decimal CostVat { get; set; }
        public decimal CostTot { get; set; }
        public decimal PriceNet { get; set; }
        public decimal PriceVat { get; set; }
        public decimal PriceTot { get; set; }
        public int QtyStock { get; set; }
        public bool? FlagVat { get; set; }
        public bool? FlagActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TblProductCategory Category { get; set; }
        public virtual TblProductModel Model { get; set; }
        public virtual TblUnit Unit { get; set; }
        public virtual ICollection<TblQuotationDetail> TblQuotationDetail { get; set; }
        public virtual ICollection<TblSaleOrderDetail> TblSaleOrderDetail { get; set; }
    }
}

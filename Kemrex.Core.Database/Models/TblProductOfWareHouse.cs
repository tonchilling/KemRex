using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{
    public partial class TblProductOfWareHouse
    {
        public int ProductId { get; set; }
        [NotMapped]
        public bool IsUpdate { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        public int Whid { get; set; }

        [NotMapped]
        public string WareHouseName { get; set; }
        public int? QtyStock { get; set; }
        public decimal? CostNet { get; set; }
        public decimal? CostVat { get; set; }
        public decimal? CostTot { get; set; }
        public decimal? PriceNet { get; set; }
        public decimal? PriceVat { get; set; }
        public decimal? PriceTot { get; set; }
        public int? FlagActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

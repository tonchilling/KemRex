using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrder
    {
        public int JobOrderId { get; set; }
        public string JobOrderNo { get; set; }
        public int? SaleOrderId { get; set; }
        public string JobName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartWorkingTime { get; set; }
        public string EndWorkingTime { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string ProjectName { get; set; }
        public int? ProductId { get; set; }
        public int? ProductQty { get; set; }
        public decimal? ProductWeight { get; set; }
        public string ProductSaftyFactory { get; set; }
        public string Adapter { get; set; }
        public string Other { get; set; }
        public string HouseNo { get; set; }
        public string VillageNo { get; set; }
        public int? SubDistrictId { get; set; }
        public string Reason { get; set; }
        public string Solution { get; set; }
        public int? TeamId { get; set; }

        public virtual TblSaleOrder SaleOrder { get; set; }
        public virtual SysSubDistrict SubDistrict { get; set; }
        public virtual TeamOperation Team { get; set; }
        public virtual List<TblJobOrderAttachmentType> AttachmentType { get; set; }
        public virtual List<TblJobOrderEquipmentType> EquipmentType { get; set; }
        public virtual List<TblJobOrderLandType> LandType { get; set; }
        public virtual List<TblJobOrderObstructionType> ObstructionType { get; set; }
        public virtual List<TblJobOrderProjectType> ProjectType { get; set; }
        public virtual List<TblJobOrderUndergroundType> UndergroundType { get; set; }
    }
}

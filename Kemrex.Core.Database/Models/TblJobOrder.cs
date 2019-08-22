using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrder
    {
        public int JobOrderId { get; set; }
        public string JobOrderNo { get; set; }
        public int? SaleOrderId { get; set; }
        public string JobName { get; set; }
        public DateTime? StartDate { get; set; }
        [NotMapped]
        public virtual string StartDateToString { get; set; }
        public DateTime? EndDate { get; set; }
        [NotMapped]
        public virtual string EndDateToString { get; set; }
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
        public string Road { get; set; }
        public string Address { get; set; }
        public string Solution { get; set; }
        public int? StatusId { get; set; }

        public DateTime? SurveyDate { get; set; }
        public int? SurveyEmployee { get; set; }
        [NotMapped]
        public string Status { get; set; }
        public int? TeamId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ApprovedBy { get; set; }
        public long? ClosedBy { get; set; }
        [NotMapped]
        public virtual string CreateDateToString { get; set; }
        public virtual TblSaleOrder SaleOrder { get; set; }
        public virtual SysSubDistrict SubDistrict { get; set; }
        public virtual TeamOperation Team { get; set; }
        public virtual List<TblJobOrderDetail> JobOrderDetail { get; set; }
        public virtual List<TblJobOrderProperties> TblJobOrderProperties { get; set; }
        public virtual List<TblJobOrderAttachmentType> AttachmentType { get; set; }
        public virtual List<TblJobOrderEquipmentType> EquipmentType { get; set; }
        public virtual List<TblJobOrderLandType> LandType { get; set; }
        public virtual List<TblJobOrderObstructionType> ObstructionType { get; set; }
        public virtual List<TblJobOrderProjectType> ProjectType { get; set; }
        public virtual List<TblJobOrderUndergroundType> UndergroundType { get; set; }
        public virtual List<TblJobOrderSurveyDetail> SurveyDetail { get; set; }
    }
}

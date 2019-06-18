using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblKpt
    {
        public TblKpt()
        {
            TblKptAttachment = new HashSet<TblKptAttachment>();
            TblKptDetail = new HashSet<TblKptDetail>();
            TblKptStation = new HashSet<TblKptStation>();
        }

        public int KptId { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string KptLatitude { get; set; }
        public string KptLongtitude { get; set; }
        public string KptLocation { get; set; }
        public int? SubDistrictId { get; set; }
        public string KptStation { get; set; }
        public DateTime KptDate { get; set; }
        public string TestBy { get; set; }
        public string KptRemark { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual SysSubDistrict SubDistrict { get; set; }
        public virtual ICollection<TblKptAttachment> TblKptAttachment { get; set; }
        public virtual ICollection<TblKptDetail> TblKptDetail { get; set; }
        public virtual ICollection<TblKptStation> TblKptStation { get; set; }
    }
}

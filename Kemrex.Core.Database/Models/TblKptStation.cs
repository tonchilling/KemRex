using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblKptStation
    {
        public TblKptStation()
        {
            TblKptStationAttachment = new HashSet<TblKptStationAttachment>();
            TblKptStationDetail = new HashSet<TblKptStationDetail>();
        }

        public int StationId { get; set; }
        public int KptId { get; set; }
        public string StationName { get; set; }
        public string StationTestBy { get; set; }
        public string StationRemark { get; set; }
        public int StationOrder { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TblKpt Kpt { get; set; }
        public virtual ICollection<TblKptStationAttachment> TblKptStationAttachment { get; set; }
        public virtual ICollection<TblKptStationDetail> TblKptStationDetail { get; set; }
    }
}

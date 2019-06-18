using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysDistrict
    {
        public SysDistrict()
        {
            SysPostcode = new HashSet<SysPostcode>();
            SysSubDistrict = new HashSet<SysSubDistrict>();
        }

        public int DistrictId { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictNameTh { get; set; }
        public string DistrictNameEn { get; set; }
        public int? StateId { get; set; }

        public virtual SysState State { get; set; }
        public virtual ICollection<SysPostcode> SysPostcode { get; set; }
        public virtual ICollection<SysSubDistrict> SysSubDistrict { get; set; }
    }
}

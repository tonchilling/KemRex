using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysState
    {
        public SysState()
        {
            SysDistrict = new HashSet<SysDistrict>();
        }

        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateNameTh { get; set; }
        public string StateNameEn { get; set; }
        public int? GeoId { get; set; }

        public virtual SysGeography Geo { get; set; }
        public virtual ICollection<SysDistrict> SysDistrict { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysGeography
    {
        public SysGeography()
        {
            SysState = new HashSet<SysState>();
        }

        public int GeoId { get; set; }
        public string GeoNameTh { get; set; }
        public string GeoNameEn { get; set; }

        public virtual ICollection<SysState> SysState { get; set; }
    }
}

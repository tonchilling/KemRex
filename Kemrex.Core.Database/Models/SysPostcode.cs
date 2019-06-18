using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysPostcode
    {
        public int DistrictId { get; set; }
        public string Postcode { get; set; }

        public virtual SysDistrict District { get; set; }
    }
}

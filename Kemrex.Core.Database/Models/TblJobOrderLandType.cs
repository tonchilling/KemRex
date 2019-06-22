using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderLandType
    {
        public int JobOrderId { get; set; }
        public int LandTypeId { get; set; }
        public string Caption { get; set; }

        public virtual SysCategory LandType { get; set; }
    }
}

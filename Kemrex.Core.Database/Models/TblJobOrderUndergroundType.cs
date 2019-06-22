using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderUndergroundType
    {
        public int JobOrderId { get; set; }
        public int UndergroundTypeId { get; set; }
        public string Caption { get; set; }

        public virtual SysCategory UndergroundType { get; set; }
    }
}

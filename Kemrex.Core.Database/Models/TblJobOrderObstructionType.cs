using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderObstructionType
    {
        public int JobOrderId { get; set; }
        public int ObstructionTypeId { get; set; }
        public string Caption { get; set; }

        public virtual SysCategory ObstructionType { get; set; }
    }
}

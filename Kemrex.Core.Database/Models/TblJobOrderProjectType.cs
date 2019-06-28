using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderProjectType
    {
        public int JobOrderId { get; set; }
        public int ProjectTypeId { get; set; }
        public string Caption { get; set; }

        public virtual SysCategory ProjectType { get; set; }
    }
}

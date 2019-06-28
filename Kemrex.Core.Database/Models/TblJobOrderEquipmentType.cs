using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderEquipmentType
    {
        public int JobOrderId { get; set; }
        public int EquipmentTypeId { get; set; }
        public string Caption { get; set; }

        public virtual SysCategory EquipmentType { get; set; }
    }
}

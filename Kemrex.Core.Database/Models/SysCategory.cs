using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysCategory
    {
        public SysCategory()
        {
            TblJobOrderAttachmentType = new HashSet<TblJobOrderAttachmentType>();
            TblJobOrderEquipmentType = new HashSet<TblJobOrderEquipmentType>();
            TblJobOrderLandType = new HashSet<TblJobOrderLandType>();
            TblJobOrderObstructionType = new HashSet<TblJobOrderObstructionType>();
            TblJobOrderProjectType = new HashSet<TblJobOrderProjectType>();
            TblJobOrderUndergroundType = new HashSet<TblJobOrderUndergroundType>();
        }

        public int CategoryId { get; set; }
        public int? CategoryTypeId { get; set; }
        public string CategoryName { get; set; }

        public virtual SysCategoryType CategoryType { get; set; }
        public virtual ICollection<TblJobOrderAttachmentType> TblJobOrderAttachmentType { get; set; }
        public virtual ICollection<TblJobOrderEquipmentType> TblJobOrderEquipmentType { get; set; }
        public virtual ICollection<TblJobOrderLandType> TblJobOrderLandType { get; set; }
        public virtual ICollection<TblJobOrderObstructionType> TblJobOrderObstructionType { get; set; }
        public virtual ICollection<TblJobOrderProjectType> TblJobOrderProjectType { get; set; }
        public virtual ICollection<TblJobOrderUndergroundType> TblJobOrderUndergroundType { get; set; }
    }
}

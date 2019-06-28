using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderAttachmentType
    {
        public int JobOrderId { get; set; }
        public int AttachmentTypeId { get; set; }
        public string Caption { get; set; }

        public virtual SysCategory AttachmentType { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblKptAttachment
    {
        public int AttachId { get; set; }
        public int KptId { get; set; }
        public string AttachName { get; set; }
        public string AttachPath { get; set; }

        public virtual TblKpt Kpt { get; set; }
    }
}

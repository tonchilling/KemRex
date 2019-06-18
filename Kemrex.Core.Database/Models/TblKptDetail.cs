using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblKptDetail
    {
        public long Id { get; set; }
        public int KptId { get; set; }
        public int KptDepth { get; set; }
        public int BlowCount { get; set; }

        public virtual TblKpt Kpt { get; set; }
    }
}

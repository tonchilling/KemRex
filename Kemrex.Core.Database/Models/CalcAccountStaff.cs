using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class CalcAccountStaff
    {
        public long AccountId { get; set; }
        public long StaffId { get; set; }
        public string StaffRemark { get; set; }

        public virtual SysAccount Account { get; set; }
        public virtual SysAccount Staff { get; set; }
    }
}

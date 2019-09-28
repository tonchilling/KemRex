using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TeamOperationDetail
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public long AccountId { get; set; }
        public string TeamRemark { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Approve { get; set; }
        public virtual SysAccount Account { get; set; }
        public virtual TeamOperation Team { get; set; }
    }
}

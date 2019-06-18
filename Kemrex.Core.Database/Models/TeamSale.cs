using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TeamSale
    {
        public TeamSale()
        {
            TeamSaleDetail = new HashSet<TeamSaleDetail>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public long ManagerId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual SysAccount Manager { get; set; }
        public virtual ICollection<TeamSaleDetail> TeamSaleDetail { get; set; }
    }
}

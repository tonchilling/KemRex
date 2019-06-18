using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblAccountTeam
    {
        public TblAccountTeam()
        {
            TblAccountTeamMember = new HashSet<TblAccountTeamMember>();
        }

        public int TeamId { get; set; }
        public int SiteId { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public int? TeamLeadId { get; set; }
        public int TeamType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TblAccountTeamMember> TblAccountTeamMember { get; set; }
    }
}

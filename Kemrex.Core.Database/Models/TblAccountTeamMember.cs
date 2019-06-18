using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblAccountTeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int AccountId { get; set; }

        public virtual TblAccountTeam Team { get; set; }
    }
}

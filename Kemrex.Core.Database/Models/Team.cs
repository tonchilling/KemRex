using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class Team
    {
        
        public int EmpId { get; set; }
        public long AccountId { get; set; }
        public int TeamId { get; set; }
        public long ManagerId { get; set; }
        
    }

}

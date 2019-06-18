using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class EnmEmployeeStatus
    {
        public EnmEmployeeStatus()
        {
            TblEmployee = new HashSet<TblEmployee>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<TblEmployee> TblEmployee { get; set; }
    }
}

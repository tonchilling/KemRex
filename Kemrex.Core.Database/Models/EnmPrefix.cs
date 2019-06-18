using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class EnmPrefix
    {
        public EnmPrefix()
        {
            TblCustomer = new HashSet<TblCustomer>();
            TblEmployee = new HashSet<TblEmployee>();
        }

        public int PrefixId { get; set; }
        public string PrefixNameTh { get; set; }
        public string PrefixNameEn { get; set; }

        public virtual ICollection<TblCustomer> TblCustomer { get; set; }
        public virtual ICollection<TblEmployee> TblEmployee { get; set; }
    }
}

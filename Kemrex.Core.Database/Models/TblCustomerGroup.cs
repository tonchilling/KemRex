using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblCustomerGroup
    {
        public TblCustomerGroup()
        {
            TblCustomer = new HashSet<TblCustomer>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupOrder { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TblCustomer> TblCustomer { get; set; }
    }
}

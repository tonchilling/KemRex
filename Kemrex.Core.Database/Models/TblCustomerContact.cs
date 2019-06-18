using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblCustomerContact
    {
        public int ContactId { get; set; }
        public int CustomerId { get; set; }
        public string ContactName { get; set; }
        public string ContactPosition { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TblCustomer Customer { get; set; }
    }
}

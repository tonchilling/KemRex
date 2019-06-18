using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblCustomerAddress
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public string AddressName { get; set; }
        public string AddressValue { get; set; }
        public int? SubDistrictId { get; set; }
        public string AddressPostcode { get; set; }
        public string AddressContact { get; set; }
        public string AddressContactEmail { get; set; }
        public string AddressContactPhone { get; set; }
        public int AddressOrder { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TblCustomer Customer { get; set; }
        public virtual SysSubDistrict SubDistrict { get; set; }
    }
}

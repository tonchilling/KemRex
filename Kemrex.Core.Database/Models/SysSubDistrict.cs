using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysSubDistrict
    {
        public SysSubDistrict()
        {
            TblCustomerAddress = new HashSet<TblCustomerAddress>();
            TblJobOrder = new HashSet<TblJobOrder>();
            TblKpt = new HashSet<TblKpt>();
        }

        public int SubDistrictId { get; set; }
        public string SubDistrictCode { get; set; }
        public string SubDistrictNameTh { get; set; }
        public string SubDistrictNameEn { get; set; }
        public int? DistrictId { get; set; }
        public string Postcode { get; set; }

        public virtual SysDistrict District { get; set; }
        public virtual ICollection<TblCustomerAddress> TblCustomerAddress { get; set; }
        public virtual ICollection<TblJobOrder> TblJobOrder { get; set; }
        public virtual ICollection<TblKpt> TblKpt { get; set; }
    }
}

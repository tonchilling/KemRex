using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Database.Models
{
    public partial class TblCustomerAddress
    {
        [NotMapped]
        public string AddressText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(AddressValue);
                if (SubDistrictId.HasValue && SubDistrict != null)
                {
                    bool capital = (SubDistrict.District != null && SubDistrict.District.StateId == 1);
                    sb.Append(" " + (capital ? "แขวง" : "ต.") + SubDistrict.SubDistrictNameTh);
                    if (SubDistrict.District != null)
                    {
                        sb.Append(" " + (capital ? "เขต" : "อ.") + SubDistrict.District.DistrictNameTh);
                        if (SubDistrict.District.State != null)
                        { sb.Append(" " + (capital ? "" : "จ.") + SubDistrict.District.State.StateNameTh); }
                    }
                }
                return sb.ToString();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class SysSubDistrict
    {
        public SysDistrict District()
        {
            SysDistrict result;
            using (dbContext db = new dbContext())
            {
                var data = db.SysDistrict.Where(x => x.DistrictId == DistrictId).FirstOrDefault();
                result = data == null ? new SysDistrict() : data;
            }
            return result;
        }
    }
}

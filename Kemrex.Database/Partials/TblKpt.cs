using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class TblKpt
    {
        public int BlowCountDepth(int depth)
        {
            int result = 0;
            if (KptId > 0)
            {
                using (dbContext db = new dbContext())
                {
                    var data = db.TblKptDetail.Where(x => x.KptId == KptId && x.KptDepth == depth).FirstOrDefault();
                    if (data != null) { result = data.BlowCount; }
                }
            }
            return result;
        }

        public SysSubDistrict SubDistrict()
        {
            SysSubDistrict result;
            using (dbContext db = new dbContext())
            {
                var data = db.SysSubDistrict.Where(x => x.SubDistrictId == SubDistrictId).FirstOrDefault();
                result = data == null ? new SysSubDistrict() : data;
            }
            return result;
        }
    }
}

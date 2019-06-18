using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class SysDistrict
    {
        public SysState State()
        {
            SysState result;
            using (dbContext db = new dbContext())
            {
                var data = db.SysState.Where(x => x.StateId == StateId).FirstOrDefault();
                result = data == null ? new SysState() : data;
            }
            return result;
        }
    }
}

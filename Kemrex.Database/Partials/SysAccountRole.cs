using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class SysAccountRole
    {
        public SysRole Role()
        {
            using (dbContext db = new dbContext())
            {
                var data = db.SysRole
                    .Where(x => x.RoleId == RoleId)
                    .Include(x => x.SysRolePermission)
                    .FirstOrDefault();
                return data == null ? new SysRole() { SysRolePermission = new List<SysRolePermission>() } : data;
            }
        }
    }
}

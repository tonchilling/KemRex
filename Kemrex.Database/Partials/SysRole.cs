using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class SysRole
    {
        public bool ValidateModel(out string errorMessage)
        {
            List<string> err = new List<string>();
            if (string.IsNullOrWhiteSpace(RoleName))
            { err.Add("กรุณาระบุชื่อสิทธิ์การใช้งาน"); }
            if (SiteId <= 0)
            { err.Add("ระบบไม่สามารถระบุส่วนของสิทธิ์การใช้งานได้, กรุณาลองใหม่อีกครั้ง"); }

            if (err.Count() > 0)
            {
                errorMessage = "กรุณาตรวจสอบข้อมูลต่อไปนี้";
                foreach (string s in err)
                { errorMessage += "{\n}- " + s; }
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        private List<SysRolePermission> _Permission { get; set; }
        public List<SysRolePermission> Permissions(bool refresh = false)
        {
            if (_Permission == null || refresh)
            {
                using (dbContext db = new dbContext())
                {
                    var data = (from d in db.SysRolePermission where d.RoleId == RoleId select d);
                    _Permission = data.Count() <= 0 ? new List<SysRolePermission>() : data.ToList();
                }
            }
            return _Permission;
        }
    }
}

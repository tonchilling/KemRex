using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Database
{
    public partial class SysAccount
    {
        public string AccountName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(AccountFirstName);
                if (!string.IsNullOrWhiteSpace(AccountLastName))
                { sb.Append(" " + AccountLastName); }
                return sb.ToString();
            }
        }
        
        public SysAccountRole Role(int siteId, bool refresh = false)
        {
            using (dbContext db = new dbContext())
            {
                var data = (from d in db.SysAccountRole
                            join r in db.SysRole on d.RoleId equals r.RoleId
                            where
                                d.AccountId == AccountId
                                && r.SiteId == siteId
                            select d).FirstOrDefault();
                return data == null ? new SysAccountRole() : data;
            }
        }
        public List<CalcAccountStaff> Staffs()
        {
            using (dbContext db = new dbContext())
            {
                var data = db.CalcAccountStaff
                    .Include("SysAccount1")
                    .Where(x => x.AccountId == AccountId);
                return data.ToList();
            }
        }

        public bool ValidateModel(out string errorMessage)
        {
            List<string> err = new List<string>();
            if (string.IsNullOrWhiteSpace(AccountUsername))
            { err.Add("กรุณาระบุชื่อบัญชี"); }
            if (string.IsNullOrWhiteSpace(AccountFirstName))
            { err.Add("กรุณาระบุชื่อผู้ใช้งาน"); }
            if (string.IsNullOrWhiteSpace(AccountPassword))
            { err.Add("กรุณาระบุรหัสผ่าน"); }
            if (string.IsNullOrWhiteSpace(AccountEmail))
            { err.Add("กรุณาระบุอีเมล์"); }

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
    }
}

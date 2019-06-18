using Kemrex.Core.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Extensions.Database
{
    public static class TeamOperationExtension
    {
        public static bool IsValid(this TeamOperation ob, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (ob == null)
            {
                errorMsg = "ไม่พบข้อมูล";
                return false;
            }
            else
            {
                List<string> err = new List<string>();
                if (string.IsNullOrWhiteSpace(ob.TeamName))
                { err.Add("กรุณาระบุชื่อทีม"); }
                if (ob.ManagerId <= 0)
                { err.Add("กรุณากำหนดหัวหน้าทีม"); }

                if (err.Count() > 0)
                {
                    errorMsg = "กรุณาตรวจสอบข้อมูลต่อไปนี้";
                    foreach (string s in err)
                    { errorMsg += @"{\n}- " + s; }
                    return false;
                }
                return true;
            }
        }

        public static bool IsValid(this TeamOperationDetail ob, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (ob == null)
            {
                errorMsg = "ไม่พบข้อมูล";
                return false;
            }
            else
            {
                List<string> err = new List<string>();
                if (ob.TeamId <= 0)
                { err.Add("ไม่สามารถระบุทีมได้, กรณาลองใหม่อีกครั้ง"); }
                if (ob.AccountId <= 0)
                { err.Add("กรุณากำหนดสมาชิก"); }

                if (err.Count() > 0)
                {
                    errorMsg = "กรุณาตรวจสอบข้อมูลต่อไปนี้";
                    foreach (string s in err)
                    { errorMsg += @"{\n}- " + s; }
                    return false;
                }
                return true;
            }
        }
    }
}

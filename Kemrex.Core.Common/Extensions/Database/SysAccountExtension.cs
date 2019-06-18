using Kemrex.Core.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Extensions.Database
{
    public static class SysAccountExtension
    {
        public static bool IsValid(this SysAccount ob, out string errorMsg)
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
                if (string.IsNullOrWhiteSpace(ob.AccountUsername))
                { err.Add("กรุณาระบุชื่อบัญชี"); }
                if (string.IsNullOrWhiteSpace(ob.AccountFirstName))
                { err.Add("กรุณาระบุชื่อผู้ใช้งาน"); }
                if (string.IsNullOrWhiteSpace(ob.AccountPassword))
                { err.Add("กรุณาระบุรหัสผ่าน"); }
                if (string.IsNullOrWhiteSpace(ob.AccountEmail))
                { err.Add("กรุณาระบุอีเมล์"); }
                if (err.Count > 0)
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

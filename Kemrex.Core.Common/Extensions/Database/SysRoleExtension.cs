using Kemrex.Core.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Extensions.Database
{
    public static class SysRoleExtension
    {
        public static bool IsValid(this SysRole ob, out string errorMsg)
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
                if (string.IsNullOrWhiteSpace(ob.RoleName))
                { err.Add("กรุณาระบุชื่อสิทธิ์การใช้งาน"); }
                if (ob.SiteId <= 0)
                { err.Add("ระบบไม่สามารถระบุส่วนของสิทธิ์การใช้งานได้, กรุณาลองใหม่อีกครั้ง"); }

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

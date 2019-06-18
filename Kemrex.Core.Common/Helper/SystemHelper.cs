using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Helper
{
    public static class SystemHelper
    {
        public static string GetConfigurationKey(string key, string defaultValue = "")
        { return ConfigurationManager.AppSettings[key] ?? defaultValue; }

        public static string GetMonthNameThai(int month)
        {
            string[] monthName = new string[] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
            if (month <= 0 || month > 12) { return ""; }
            return monthName[month - 1];
        }
    }
}

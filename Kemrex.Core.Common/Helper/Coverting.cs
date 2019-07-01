using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Kemrex.Core.Common.Helper
{
  public static  class Converting
    {
        public static string ToDDMMYYYY(DateTime dt)
        {
            string result = "";

            try
            {
                if(dt!=null)
                result = string.Format("{0}/{1}/{2}", dt.Day, dt.Month, dt.Year);
            }catch
            { }
            finally{ }
            return result;

        }

        public static string ToYYYYMMDD(DateTime dt)
        {
            string result = "";

            try
            {
                if (dt != null)
                    result = string.Format("{0}-{1}-{2}", dt.Year, dt.Month.ToString("##00"), dt.Day.ToString("##00"));
            }
            catch
            { }
            finally { }
            return result;

        }

        public static string ToDDMMYYYY(DateTime? dt)
        {
            string result = "";

            try
            {
                if (dt != null)
                    result = string.Format("{0}/{1}/{2}", dt.Value.Day.ToString("##00"), dt.Value.Month.ToString("##00"), dt.Value.Year);
            }
            catch
            { }
            finally { }
            return result;

        }

        public static DateTime StringToDate(string strDate,string format)
        {
            DateTime result =new DateTime() ;
            CultureInfo enUS = new CultureInfo("en-US");
            try
            {
                if (strDate != "")
                    result = DateTime.ParseExact(strDate, format, enUS);
            }
            catch
            { }
            finally { }
            return result;

        }

        public static string SaleOrderStatus(int StatusId)
        {
            string result = "ร่าง";

            try
            {
                switch (StatusId)
                {
                    case 1: result = "เปิด"; break;
                    case 2: result = "ปิด"; break;
                    case 3: result = "ยกเลิก"; break;
                }
            }
            catch
            { }
            finally { }
            return result;

        }
    }
}

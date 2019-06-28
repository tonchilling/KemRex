using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static string ToDDMMYYYY(DateTime? dt)
        {
            string result = "";

            try
            {
                if (dt != null)
                    result = string.Format("{0}/{1}/{2}", dt.Value.Day, dt.Value.Month, dt.Value.Year);
            }
            catch
            { }
            finally { }
            return result;

        }
    }
}

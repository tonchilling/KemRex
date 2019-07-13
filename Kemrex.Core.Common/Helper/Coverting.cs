using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Globalization;

namespace Kemrex.Core.Common.Helper
{
    public static class Converting
    {
        static string[] mL = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        static string[] mS = { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };


        public static string ToDDMMYYYY(DateTime dt)
        {
            string result = "";

            try
            {
                if (dt != null)
                    result = string.Format("{0}/{1}/{2}", dt.Day, dt.Month, dt.Year);
            }
            catch
            { }
            finally { }
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

        public static DateTime StringToDate(string strDate, string format)
        {
            DateTime result = new DateTime();
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

        public static string JobOrderStatus(string StatusId)
        {
            string result = "ร่าง";

            try
            {
                switch (StatusId)
                {
                    case "1": result = "เปิด"; break;
                    case "2": result = "ปิด"; break;
                    case "3": result = "ยกเลิก"; break;
                }
            }
            catch
            { }
            finally { }
            return result;

        }

        public static string TransferStatus(string StatusId)
        {
            string result = "ร่าง";

            try
            {
                switch (StatusId)
                {
                    case "1": result = "ปิด"; break;
                    case "2": result = "ยกเลิก"; break;
  
                }
            }
            catch
            { }
            finally { }
            return result;

        }

        public static string DateOfDDMM(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0} {1}", dt.Day.ToString(), mS[dt.Month]);
            }
            catch { }
            finally { }
            return result;
        }
        public static string DateOfDDMMLong(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0} {1}", dt.Day.ToString(), mL[dt.Month - 1]);
            }
            catch { }
            finally { }
            return result;
        }


        public static int ToYYYYMM(string mm, string yyyy)
        {
            int result = 0;

            try
            {


                result = Convert.ToInt32(string.Format("{0}{1}", yyyy, mm));
            }
            catch { }
            finally { }
            return result;
        }
        public static int ToYYYYMM(string yyyyMM)
        {
            int result = 0;

            try
            {


                result = Convert.ToInt32(yyyyMM);
            }
            catch { }
            finally { }
            return result;
        }



        public static DataTable ConvertObjectToDataTable<T>(IEnumerable<T> list)
        {

            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;

        }
        public static List<T> ConvertDataReaderToObjList<T>(SqlDataReader reader)
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();
            try
            {
                var columnNames = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    columnNames.Add(reader.GetName(i));


                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {



                        if (columnNames.Find(delegate (string colName)
                        {
                            return colName.ToLower().Equals(property.Name.ToLower());
                        }) != null)
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
            }
            catch (Exception ex)
            {

                string err = ex.ToString();

            }
            finally
            {

            }
            return results;
        }


        public static string DateOfyyyyMMdd(DateTime date)
        {

            return string.Format("{0}{1}{2}", date.Year.ToString()
                                            , date.Month.ToString("##00")
                                            , date.Day.ToString("##00"));
        }

        public static string FormatCurrency(string data)
        {
            string result = "";
            try
            {

                result = Convert.ToDecimal(data).ToString("##,##0.00");
            }

            catch
            {
                result = data;
            }
            finally
            {

            }

            return result;


        }

        public static int ToInt(string data)
        {
            int result = 0;
            try
            {

                result = Convert.ToInt32(data);
            }

            catch
            {

            }
            finally
            {

            }

            return result;


        }
        public static double ToDoble(string data)
        {
            double result = 0;
            try
            {

                result = Convert.ToDouble(data);
            }

            catch
            {

            }
            finally
            {

            }

            return result;


        }
        public static string FormatInt(string data)
        {
            string result = "";
            try
            {

                result = Convert.ToDecimal(data).ToString("##,##0");
            }

            catch
            {
                result = data;
            }
            finally
            {

            }

            return result;


        }




        public static bool ToBoolean(string data)
        {
            bool result = false;
            try
            {

                result = Convert.ToBoolean(data);
            }

            catch
            {

            }
            finally
            {

            }

            return result;


        }


        /// <summary>
        /// Formate datetime string from dd/MM/yyyy to yyyyMMdd
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateOfyyyyMMdd(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                dt = DateTime.ParseExact(date,
                                   "dd/MM/yyyy",
                                   new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0}{1}{2}", dt.Year.ToString()
                                               , dt.Month.ToString("##00")
                                               , dt.Day.ToString("##00"));
            }
            catch { }
            finally { }
            return result;
        }

        /// <summary>
        /// Formate datetime string from yyyyMMdd to  MM/dd/yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateOfMM_dd_yyyy(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date,
                                  "dd/MM/yyyy",
                                    new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0}/{1}/{2}", dt.Month.ToString("##00")
                                               , dt.Day.ToString("##00")
                                               , dt.Year.ToString());
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }

        public static string DateOfdd_MM_yyyy(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date,
                                   "MM/dd/yyyy",
                                    new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0}/{1}/{2}", dt.Day.ToString("##00")
                                               , dt.Month.ToString("##00")
                                               , dt.Year.ToString());
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }

        /// <summary>
        /// Get colum name excel by index
        /// </summary>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public static string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }
    }
}

using Kemrex.Core.Common.Models;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Kemrex.Core.Database.Models;
namespace Kemrex.Core.Common.Modules
{
    public class SystemModule
    {
        private readonly mainContext db;
        private readonly decimal VatRate = 7;
        private WebDB webdb;
        public SystemModule(mainContext context)
        {
            db = context;
            webdb = new WebDB();
        }

        public List<SysMenu> GetMenuBase(int siteId)
        {
            var data = db.SysMenu
                .Where(x =>
                    x.SiteId == siteId
                    && x.MenuLevel == 1)
                .Include(x => x.InverseParent)
                    .ThenInclude(x => x.InverseParent)
                        .ThenInclude(x => x.SysMenuPermission)
                .Include(x => x.InverseParent)
                    .ThenInclude(x => x.SysMenuPermission)
                .Include(x => x.SysMenuPermission);
            return data.ToList() ?? new List<SysMenu>();
        }

     
        public List<SysMenu> GetMenus(int siteId)
        {
            var data = db.SysMenu
                .Where(x => x.SiteId == siteId)
                .Include(x => x.InverseParent)
                    .ThenInclude(x => x.InverseParent)
                        .ThenInclude(x => x.SysMenuPermission)
                .Include(x => x.InverseParent)
                    .ThenInclude(x => x.SysMenuPermission)
                .Include(x => x.SysMenuPermission);
            return data.ToList() ?? new List<SysMenu>();
        }



        public List<int> GetMenuActiveList(string MVCArea, string MVCController, string MVCAction)
        {
            List<int> result = new List<int>();
            var data = db.SysMenuActive
                .Where(x =>
                    x.MvcArea == MVCArea
                    && x.MvcController == MVCController
                    && x.MvcAction == MVCAction)
                .Include(x => x.Menu)
                    .ThenInclude(x => x.Parent)
                        .ThenInclude(x => x.Parent)
                .FirstOrDefault();
            if (data != null)
            {
                result.Add(data.MenuId);
                if (data.Menu.Parent != null)
                {
                    var parent = data.Menu.Parent;
                    result.Add(parent.MenuId);
                    if (parent.Parent != null)
                    { result.Add(parent.Parent.MenuId); }
                }
            }
            return result;
        }

        public List<Select2Model> DropDownStateList(string src = "")
        {
            return (from t in db.SysSubDistrict
                    join d in db.SysDistrict on t.DistrictId equals d.DistrictId
                    join p in db.SysState on d.StateId equals p.StateId
                    where
                        (
                            src == ""
                            || t.SubDistrictNameTh.Contains(src)
                            || d.DistrictNameTh.Contains(src)
                            || p.StateNameTh.Contains(src)
                        )
                    select new Select2Model()
                    {
                        id = t.SubDistrictId.ToString(),
                        text = string.Concat(t.SubDistrictNameTh, " > ", d.DistrictNameTh, " > ", p.StateNameTh)
                    }).ToList() ?? new List<Select2Model>();
        }

        public decimal GetVatFromNet(decimal Net)
        {
            return decimal.Round(((Net * VatRate) / 100), 2);
        }

        public decimal GetVatFromTot(decimal Tot)
        {
            return Tot - decimal.Round(((Tot * 100) / (100 + VatRate)), 2);
        }



        public TransactionStatus GetAllStatus(string AccountId)
        {
            string sql = "sp_GetAllStatus";
            List<SqlParameter> paramList = new List<SqlParameter>();

            TransactionStatus dto = null;
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
                webdb.OpenConnection();
                paramList.Add(new SqlParameter("@AccountId", AccountId));
               
                //connect.Open();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = webdb.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    dto = new TransactionStatus();
                    dto.QuoState1 = reader["QuoState1"].ToString();
                    dto.QuoState2 = reader["QuoState2"].ToString();
                    dto.QuoState3 = reader["QuoState3"].ToString();
                    dto.QuoState4 = reader["QuoState4"].ToString();

                    dto.SOState1 = reader["SOState1"].ToString();
                    dto.SOState2 = reader["SOState2"].ToString();
                    dto.SOState3 = reader["SOState3"].ToString();

                    dto.InvState1 = reader["InvState1"].ToString();
                    dto.InvState2 = reader["InvState2"].ToString();
                    dto.InvState3 = reader["InvState3"].ToString();
                    dto.InvState4 = reader["InvState4"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (webdb.Connection.State == ConnectionState.Open)
                {
                    webdb.CloseConnection();
                }
            }



            return dto;
        }
    }
}

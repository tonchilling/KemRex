using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Kemrex.Core.Common.Helper;
namespace Kemrex.Core.Common.Modules
{
    public class RolePermissionModule : IModule<SysRolePermission>
    {
        private readonly mainContext db;
        private WebDB webdb;
        public RolePermissionModule(mainContext context)
        {
            db = context;
            webdb = new WebDB();
        }

        public int Counts(int roleId = 0)
        {
            var data = db.SysRolePermission.AsQueryable();
            data = Filter(data, roleId);
            return data.Count();
        }

        public void Delete(SysRolePermission ob)
        {
            if (IsExist(ob.RoleId, ob.MenuId, ob.PermissionId))
            { db.SysRolePermission.Remove(ob); }
        }

        public void DeleteByRole(int roleId)
        {
            var data = db.SysRolePermission
                .Where(x => x.RoleId == roleId);
            if (data.Count() > 0)
            { db.SysRolePermission.RemoveRange(data); }
        }

        public SysRolePermission Get(int roleId, int menuId, int permissionId)
        {
            return db.SysRolePermission
                .Where(x =>
                    x.RoleId == roleId
                    && x.MenuId == menuId
                    && x.PermissionId == permissionId)
                .Include(x => x.Menu)
                .Include(x => x.Role)
                .FirstOrDefault() ?? new SysRolePermission() { MenuId = menuId, PermissionId = permissionId };
        }

        public List<SysRolePermission> Gets(int page = 1, int size = 0
            , int roleId = 0)
        {
            var data = db.SysRolePermission
                .Include(x => x.Menu)
                .AsQueryable();
            data = Filter(data, roleId);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<SysRolePermission> Filter(IQueryable<SysRolePermission> data
            , int roleId = 0)
        {
            if (roleId != 0) { data = data.Where(x => x.RoleId == roleId); }
            return data;
        }

        public bool IsExist(int roleId, int menuId, int permissionId)
        {
            return db.SysRolePermission
                  .Where(x =>
                      x.RoleId == roleId
                      && x.MenuId == menuId
                      && x.PermissionId == permissionId)
                  .Count() > 0 ? true : false;
        }

        public bool Add(SysRolePermission dto)
        {
            bool result = false;
            string sql = "sp_SysRolePermission_Insert";


            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@RoleId", dto.RoleId));
            paramList.Add(new SqlParameter("@MenuId", dto.MenuId));
            paramList.Add(new SqlParameter("@PermissionId", dto.PermissionId));
            paramList.Add(new SqlParameter("@PermissionFlag", dto.PermissionFlag));




            try
            {

                result = webdb.ExcecuteNonQuery(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("EV_Master_ConditionDAO.Insert::" + ex.ToString());
            }
            finally
            { }





            return result;

        }
        public List<SysMenu> GetMenuByRole(int roleId)
        {
            string sql = "GetMenuActive";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<SysMenu> list = new List<SysMenu>();
            SysMenu dto = null;
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
                webdb.OpenConnection();
                paramList.Add(new SqlParameter("@RoleId", roleId));

                //connect.Open();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = webdb.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    dto = new SysMenu();
                    dto.MenuId = Converting.ToInt(reader["MenuId"].ToString());
                    dto.SiteId = Converting.ToInt(reader["SiteId"].ToString());
                    dto.MenuName = reader["MenuName"].ToString();
                    dto.MenuIcon = reader["MenuIcon"].ToString();
                    dto.MenuOrder = Converting.ToInt(reader["MenuOrder"].ToString());
                    dto.ParentId = Converting.ToInt(reader["ParentId"].ToString());
                    dto.MvcArea = reader["MvcArea"].ToString();
                    dto.MvcController = reader["MvcController"].ToString();
                    dto.MvcAction = reader["MvcAction"].ToString();
                    dto.FlagActive = Converting.ToBoolean(reader["FlagActive"].ToString());
                    dto.View = Converting.ToInt(reader["View"].ToString());
                    dto.Edit = Converting.ToInt(reader["Edit"].ToString());
                    dto.Delete = Converting.ToInt(reader["Delete"].ToString());
                    list.Add(dto);

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



            return list;
        }



        public bool Add(List<SysRolePermission> dtoList)
        {
            bool result = false;
            string sql = "sp_SysRolePermission_Insert";


            List<SqlParameter> paramList = new List<SqlParameter>();






            try
            {
                foreach (var dto in dtoList)
                {
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@RoleId", dto.RoleId));
                    paramList.Add(new SqlParameter("@MenuId", dto.MenuId));
                    paramList.Add(new SqlParameter("@PermissionId", dto.PermissionId));
                    paramList.Add(new SqlParameter("@PermissionFlag", dto.PermissionFlag ? 1 : 0));
                    result = webdb.ExcecuteNonQuery(sql, paramList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SysRolePermission.Insert::" + ex.ToString());
            }
            finally
            { }





            return result;

        }
        public void Set(SysRolePermission ob)
        {
            if (ob.RoleId <= 0)
            { db.SysRolePermission.Add(ob); }
            else { db.SysRolePermission.Update(ob); }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }
}

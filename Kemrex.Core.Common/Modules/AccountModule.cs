using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Modules
{
    public class AccountModule : IModule<SysAccount, long>
    {
        private readonly mainContext db;

        public AccountModule(mainContext context)
        {
            db = context;
        }

        public int Counts(int siteId = 0, string src = "", string username = "", string email = "")
        {
            var data = db.SysAccount.AsQueryable();
            data = Filter(data, siteId, src, username, email);
            return data.Count();
        }

        public void Delete(SysAccount ob)
        {
            if (IsExist(ob.AccountId))
            { db.SysAccount.Remove(ob); }
        }

        public SysAccount Get(long id)
        {
            SysAccount account = db.SysAccount.Where(e => e.AccountId == id).Include(e => e.TblEmployee).FirstOrDefault();
            if (account != null)
            {
                account.SysAccountRole = db.SysAccountRole.Where(e => e.AccountId == id).FirstOrDefault();

                if (account.TblEmployee != null)
                {
                    if (account.TblEmployee.DepartmentId != null)
                        account.TblEmployee.Department = db.TblDepartment.Where(o => o.DepartmentId == account.TblEmployee.DepartmentId).FirstOrDefault();
                    if (account.TblEmployee.PositionId != null)
                        account.TblEmployee.Position = db.TblPosition.Where(o => o.PositionId == account.TblEmployee.PositionId).FirstOrDefault();
                }
            }
            return account ?? new SysAccount();
        }

        public SysAccount GetByUsernameOrEmail(string src)
        {
            return db.SysAccount
                .Where(x => x.AccountUsername == src || x.AccountEmail == src)
                .FirstOrDefault();
        }

        public List<SysAccount> Gets(int page = 1, int size = 0, int siteId = 0
            , string src = "", string username = "", string email = "")
        {
            var data = db.SysAccount.Include(o=>o.TblEmployee).Include(o=>o.SysAccountRole)
                .AsQueryable();
            foreach (SysAccount obj in data)
            {
                obj.SysAccountRole = db.SysAccountRole.Where(o => o.AccountId == obj.AccountId).Include(x=>x.Role).FirstOrDefault();
                     }

            /*
               public long AccountId { get; set; }
        public string AccountAvatar { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public string AccountFirstName { get; set; }
        public string AccountLastName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountRemark { get; set; }
             */

            data = Filter(data, siteId, src, username, email);
            if (size > 0)
                
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public List<SysAccount> GetList()
        {
            var data = (from ac in db.SysAccount
                          .Include(a => a.AccountName)
                          .Include(b => b.TblEmployee)
                          .Include(c => c.SysAccountRole)
                        select new SysAccount
                        {
                            AccountId = ac.AccountId,
                            AccountUsername = ac.AccountUsername,
                            AccountFirstName = ac.AccountFirstName,
                            AccountLastName = ac.AccountLastName,
                            TblEmployee = ac.TblEmployee,
                            SysAccountRole = ac.SysAccountRole
                        }).ToList();
                        foreach (SysAccount obj in data)
                        {
                            if (obj.SysAccountRole != null)
                            {
                                obj.SysAccountRole.Role = (from r in db.SysRole
                                                           where r.RoleId == obj.SysAccountRole.RoleId
                                                           select new SysRole
                                                           {
                                                               RoleName = r.RoleName,
                                                               RoleDescription = r.RoleDescription
                                                               

                                                           }).FirstOrDefault();
                            }
                        
                        }


            return data;
        }
        private IQueryable<SysAccount> Filter(IQueryable<SysAccount> data, int siteId
            , string src, string username, string email)
        {
            if (siteId > 0) {
                var role = db.SysRole.Where(x => x.SiteId == siteId);
                var acc = db.SysAccountRole.Where(x => role.Select(y => y.RoleId).Contains(x.RoleId));
                data = data.Where(x => acc.Select(y => y.AccountId).Contains(x.AccountId));

                
            }
            if (!string.IsNullOrWhiteSpace(src))
            {
                data = data.Where(x =>
                    x.AccountFirstName.Contains(src)
                    || x.AccountLastName.Contains(src));
            }
            if (!string.IsNullOrWhiteSpace(username)) { data = data.Where(x => x.AccountUsername.Contains(username)); }
            if (!string.IsNullOrWhiteSpace(email)) { data = data.Where(x => x.AccountEmail.Contains(email)); }
            return data;
        }

        public bool IsExist(long id)
        { return db.SysAccount.Where(x => x.AccountId == id).Count() > 0 ? true : false; }

        public void Set(SysAccount ob)
        {
            if (ob.AccountId <= 0)
            { db.SysAccount.Add(ob); }
            else { db.SysAccount.Update(ob); }
        }

        public void SetRole(int siteId, int roleId, SysAccount ob)
        {
            SysAccountRole role = ob.AccountId > 0 ?
                        ((from d in db.SysAccountRole
                          join r in db.SysRole on d.RoleId equals r.RoleId
                          where
                              d.AccountId == ob.AccountId
                              && r.SiteId == siteId
                          select d).FirstOrDefault() ?? new SysAccountRole() { Account = ob }) :
                        new SysAccountRole() { Account = ob };
            role.RoleId = roleId;
            if (role.Id <= 0)
            { db.SysAccountRole.Add(role); }
            else { db.SysAccountRole.Update(role); }
        }

        #region Business Functions
        
        public bool IsExistsUsername(int accountId, string username)
        {
            bool result = true;
            if (db.SysAccount.Where(x => x.AccountUsername == username && x.AccountId != accountId).Count() == 0) { result = false; }
            return result;
        }

        public bool IsExistsEmail(int accountId, string email)
        {
            bool result = true;
            if (db.SysAccount.Where(x => x.AccountEmail == email && x.AccountId != accountId).Count() == 0) { result = false; }
            return result;
        }

        #endregion Business Functions
    }
}
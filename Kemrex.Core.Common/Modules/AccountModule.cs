﻿using Kemrex.Core.Common.Interfaces;
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
            return db.SysAccount
                .Where(x => x.AccountId == id)
                .FirstOrDefault() ?? new SysAccount();
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
            var data = db.SysAccount
                .AsQueryable();
            data = Filter(data, siteId, src, username, email);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
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
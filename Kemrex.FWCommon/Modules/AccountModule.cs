using Kemrex.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.FWCommon.Modules
{
    public class AccountModule
    {
        private readonly dbContext db;
        public AccountModule(dbContext context)
        {
            db = context;
        }

        public int Counts(string src = "", string username = "", string email = "")
        {
            var data = db.SysAccount.AsQueryable();
            data = Filter(data, src, username, email);
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

        public List<SysAccount> Gets(int page = 1, int size = 0
            , string src = "", string username = "", string email = "")
        {
            var data = db.SysAccount
                .AsQueryable();
            data = Filter(data, src, username, email);
            data = data.OrderBy(x => x.AccountEmail);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<SysAccount> Filter(IQueryable<SysAccount> data, string src, string username, string email)
        {
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
            else { db.Entry(ob).State = EntityState.Modified; }
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
        #endregion
    }
}

using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class BankAccountModule
    {
        private readonly mainContext db;
        public BankAccountModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblBankAccount.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblBankAccount ob)
        {
            if (IsExist(ob.AcctId))
            { db.TblBankAccount.Remove(ob); }
        }

        private IQueryable<TblBankAccount> Filter(IQueryable<TblBankAccount> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.AcctNo.Contains(src)); }
            return data;
        }

        public TblBankAccount Get(int id)
        {
            return db.TblBankAccount
                .Where(x => x.AcctId == id).FirstOrDefault() ?? new TblBankAccount();
        }

        public List<TblBankAccount> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblBankAccount
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.AcctId);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblBankAccount.Where(x => x.AcctId == id).Count() > 0 ? true : false; }

        public void Set(TblBankAccount ob)
        {
            if (ob.AcctId == 0)
            { db.TblBankAccount.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

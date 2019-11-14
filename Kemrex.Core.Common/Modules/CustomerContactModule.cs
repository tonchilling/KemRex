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
    public class CustomerContactModule : IModule<TblCustomerContact, int>
    {
        private readonly mainContext db;
        public CustomerContactModule(mainContext context)
        {
            db = context;
        }
        public int Count(int cusId = 0, string src = "")
        {
            var data = db.TblCustomerContact.AsQueryable();
            data = Filter(data, cusId, src);
            return data.Count();
        }
        public List<TblCustomerContact> Gets(int page = 1, int size = 0
       , int cusId = 0, string src = "")
        {
            var data = db.TblCustomerContact
                .AsQueryable();
            data = Filter(data, cusId, src);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        private IQueryable<TblCustomerContact> Filter(IQueryable<TblCustomerContact> data, int cusId, string src)
        {
            if (cusId > 0) { data = data.Where(x => x.CustomerId == cusId); }
            if (!string.IsNullOrWhiteSpace(src)) {
                data = data.Where(x =>
                    x.ContactName.Contains(src)
                    || x.ContactEmail.Contains(src)
                    || x.ContactPhone.Contains(src));
            }
            return data;
        }
        public void Delete(TblCustomerContact ob)
        {
            if (IsExist(ob.ContactId))
            { db.TblCustomerContact.Remove(ob); }
        }

        public TblCustomerContact Get(int id)
        {
            return db.TblCustomerContact
                .Where(x => x.ContactId == id)
                .Include(x => x.Customer)
                .FirstOrDefault() ?? new TblCustomerContact() { Customer = new TblCustomer() };
        }

        public bool IsExist(int id)
        { return db.TblCustomerContact.Where(x => x.ContactId == id).Count() > 0 ? true : false; }

        public void Set(TblCustomerContact ob)
        {
            if (ob.ContactId <= 0)
            { db.TblCustomerContact.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

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
    public class CustomerGroupModule : IModule<TblCustomerGroup, int>
    {
        private readonly mainContext db;
        public CustomerGroupModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblCustomerGroup.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblCustomerGroup ob)
        {
            if (IsExist(ob.GroupId))
            { db.TblCustomerGroup.Remove(ob); }
        }

        public TblCustomerGroup Get(int id)
        {
            return db.TblCustomerGroup
                .Where(x => x.GroupId == id)
                .Include(x => x.TblCustomer)
                .FirstOrDefault() ?? new TblCustomerGroup() { TblCustomer = new List<TblCustomer>() };
        }

        public List<TblCustomerGroup> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblCustomerGroup
                .AsQueryable();
            data = Filter(data, src).OrderBy(x => x.GroupOrder);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<TblCustomerGroup> Filter(IQueryable<TblCustomerGroup> data, string src)
        {
            if (!string.IsNullOrWhiteSpace(src)) { data = data.Where(x => x.GroupName.Contains(src)); }
            return data;
        }

        public bool IsExist(int id)
        { return db.TblCustomerGroup.Where(x => x.GroupId == id).Count() > 0 ? true : false; }

        public void Set(TblCustomerGroup ob)
        {
            if (ob.GroupId <= 0)
            { db.TblCustomerGroup.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

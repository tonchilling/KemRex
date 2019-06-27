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
    public class CustomerModule : IModule<TblCustomer, int>
    {
        private readonly mainContext db;
        public CustomerModule(mainContext context)
        {
            db = context;
        }

        public int Count(int groupId = 0, string src = "")
        {
            var data = db.TblCustomer.AsQueryable();
            data = Filter(data, groupId, src);
            return data.Count();
        }

        public void Delete(TblCustomer ob)
        {
            if (IsExist(ob.CustomerId))
            { db.TblCustomer.Remove(ob); }
        }

        public TblCustomer Get(int id)
        {
            return db.TblCustomer
                .Where(x => x.CustomerId == id)
                .Include(x => x.Group)
                .Include(x => x.TblCustomerAddress)
                .Include(x => x.TblCustomerContact)
                .FirstOrDefault() ?? new TblCustomer()
                {
                    Group = new TblCustomerGroup(),
                    TblCustomerAddress = new List<TblCustomerAddress>(),
                    TblCustomerContact = new List<TblCustomerContact>()
                };
        }
        public TblCustomer GetByCondition(int id)
        {
            return db.TblCustomer
                .Where(x => x.CustomerId == id)
              
                .FirstOrDefault() ?? new TblCustomer()
                {
                    Group = new TblCustomerGroup(),
                    TblCustomerAddress = new List<TblCustomerAddress>(),
                    TblCustomerContact = new List<TblCustomerContact>()
                };
        }

        public List<TblCustomer> GetAll()
        {
            var data= db.TblCustomer
                .Include(x => x.Group)
                .Include(x => x.TblCustomerAddress)
                .Include(x => x.TblCustomerContact)
                .AsQueryable();              

            return data.ToList();
        }
        public List<TblCustomer> GetAllAddress()
        {
            var data = db.TblCustomer                
                .AsQueryable();

            var query = (from qd in data.Include(qd=>qd.TblCustomerAddress)
                         orderby qd.CustomerName
                         select qd);
            return query.ToList();
        }


        public List<TblCustomer> Gets(int page = 1, int size = 0
            , int groupId = 0, string src = "")
        {
            var data = db.TblCustomer
                .AsQueryable();
            data = Filter(data, groupId, src);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        private IQueryable<TblCustomer> Filter(IQueryable<TblCustomer> data, int groupId, string src)
        {
            if (groupId > 0) { data = data.Where(x => x.GroupId == groupId); }
            if (!string.IsNullOrWhiteSpace(src)) { data = data.Where(x => x.CustomerName.Contains(src)); }
            return data;
        }

        public bool IsExist(int id)
        { return db.TblCustomer.Where(x => x.CustomerId == id).Count() > 0 ? true : false; }

        public void Set(TblCustomer ob)
        {
            if (ob.CustomerId <= 0)
            { db.TblCustomer.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

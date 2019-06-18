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
    public class CustomerAddressModule : IModule<TblCustomerAddress, int>
    {
        private readonly mainContext db;
        public CustomerAddressModule(mainContext context)
        {
            db = context;
        }
        public int Count(int cusId = 0, string src = "")
        {
            var data = db.TblCustomerAddress.AsQueryable();
            data = Filter(data, cusId, src);
            return data.Count();
        }
        public List<TblCustomerAddress> Gets(int page = 1, int size = 0
       , int cusId = 0, string src = "")
        {
            var data = db.TblCustomerAddress
                .Include(x => x.Customer)
                .Include(x => x.SubDistrict).ThenInclude(x => x.District).ThenInclude(x => x.State)
                .OrderBy(x => x.AddressOrder)
                .AsQueryable();
            data = Filter(data, cusId, src);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        private IQueryable<TblCustomerAddress> Filter(IQueryable<TblCustomerAddress> data, int cusId, string src)
        {
            if (cusId > 0) { data = data.Where(x => x.CustomerId == cusId); }
            if (!string.IsNullOrWhiteSpace(src)) { data = data.Where(x => x.AddressName.Contains(src)); }
            return data;
        }
        public void Delete(TblCustomerAddress ob)
        {
            if (IsExist(ob.AddressId))
            { db.TblCustomerAddress.Remove(ob); }
        }

        public TblCustomerAddress Get(int id)
        {
            return db.TblCustomerAddress
                .Where(x => x.AddressId == id)
                .Include(x => x.Customer)
                .Include(x => x.SubDistrict).ThenInclude(x => x.District).ThenInclude(x => x.State)
                .FirstOrDefault() ?? new TblCustomerAddress()
                {
                    Customer = new TblCustomer(),
                    SubDistrict = new SysSubDistrict()
                    {
                        District = new SysDistrict()
                        { State = new SysState() }
                    }
                };
        }

        public bool IsExist(int id)
        { return db.TblCustomerAddress.Where(x => x.AddressId == id).Count() > 0 ? true : false; }

        public void Set(TblCustomerAddress ob)
        {
            if (ob.AddressId <= 0)
            { db.TblCustomerAddress.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class PaymentModule
    {
        private readonly mainContext db;
        public PaymentModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblPayment.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblPayment ob)
        {
            if (IsExist(ob.PaymentId))
            { db.TblPayment.Remove(ob); }
        }

        private IQueryable<TblPayment> Filter(IQueryable<TblPayment> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.CustomerName.Contains(src)); }
            return data;
        }

        public TblPayment Get(int id)
        {
            return db.TblPayment
                .Where(x => x.PaymentId == id).FirstOrDefault() ?? new TblPayment();
        }

        public List<TblPayment> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblPayment
                .AsQueryable();
            data = Filter(data, src)
                .OrderByDescending(x => x.PaymentDate);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblPayment.Where(x => x.PaymentId == id).Count() > 0 ? true : false; }

        public void Set(TblPayment ob)
        {
            if (ob.PaymentId == 0)
            { db.TblPayment.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
        public List<TblPayment> GetList()
        {
            var data = db.TblPayment
                       .OrderByDescending(c => c.PaymentDate)
               .AsQueryable();
            return data.ToList();
        }
    }
}

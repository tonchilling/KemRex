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
   public class SaleOrderModule : IModule<TblSaleOrder, int>
    {
        private readonly mainContext db;
        public SaleOrderModule(mainContext context)
        {
            db = context;
        }
        public void Delete(TblSaleOrder ob)
        {
            if (IsExist(ob.SaleOrderId))
            { db.TblSaleOrder.Remove(ob); }
        }
        public int Count(int groupId = 0, string src = "")
        {
            var data = db.TblSaleOrder.AsQueryable();
            return data.Count();
        }
        public string GetLastId(string pre)
        {
            return (from q in db.TblSaleOrder
                    .Where(n=>n.SaleOrderNo.Contains(pre))
                    select q.SaleOrderNo).Max();
        }
        public TblSaleOrder Get(int id)
        {
            return db.TblSaleOrder
                   .Where(x => x.SaleOrderId == id)
                   .FirstOrDefault() ?? new TblSaleOrder()
                   {
                       CreatedDate = DateTime.Now
                   };
        }
        public List<TblSaleOrder> Gets(int page = 1, int size = 0, int month = 0, string src = "")
        {
            var data = db.TblSaleOrder
                        .OrderByDescending(c => c.SaleOrderId)
                .AsQueryable();

            if (month > 0) { data = data.Where(x => x.SaleOrderDate.Value.Month == month); }

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public bool IsExist(int id)
        {
             return db.TblSaleOrder.Where(x => x.SaleOrderId == id).Count() > 0 ? true : false; 
        }

        public void Set(TblSaleOrder ob)
        {
            if (ob.SaleOrderId <= 0)
            { db.TblSaleOrder.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

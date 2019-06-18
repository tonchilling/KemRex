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
    public class SaleOrderDetailModule : IModule<TblSaleOrderDetail, int>
    {
        private readonly mainContext db;
        public SaleOrderDetailModule(mainContext context)
        {
            db = context;
        }
        public void Delete(TblSaleOrderDetail ob)
        {
            if (IsExist(ob.Id))
            { db.TblSaleOrderDetail.Remove(ob); }
        }

        public void DeleteId(int id)
        {
            var ob = db.TblSaleOrderDetail.Where(t => t.SaleOrderId == id).ToList();
            db.TblSaleOrderDetail.RemoveRange(ob);
        }
        public TblSaleOrderDetail Get(int id)
        {
            return db.TblSaleOrderDetail
                .Where(x => x.Id == id)
                .FirstOrDefault() ?? new TblSaleOrderDetail();
        }
        public List<TblSaleOrderDetail> Gets(int SaleOrderId = 0)
        {
            var data = db.TblSaleOrderDetail
              .AsQueryable();
            // data = Filter(data, groupId, src);
            var query = (from qd in data
                         join p in db.TblProduct
                         on qd.ProductId equals p.ProductId
                         where qd.SaleOrderId == SaleOrderId
                         select qd);
            return query.ToList();
        }
        public bool IsExist(int id)
        { return db.TblSaleOrderDetail.Where(x => x.Id == id).Count() > 0 ? true : false; }

        public void Set(TblSaleOrderDetail ob)
        {
            if (ob.Id <= 0)
            { db.TblSaleOrderDetail.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

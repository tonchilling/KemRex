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

        public TblSaleOrderDetail Get(int SaleorderId = 0, int ProductId = 0, int WhId = 0)
        {
            return db.TblSaleOrderDetail
                .Where(x => x.SaleOrderId == SaleorderId && x.ProductId == ProductId && x.WHId == WhId)
                .FirstOrDefault() ?? new TblSaleOrderDetail();
        }

        public List<TblSaleOrderDetail> Gets(int SaleOrderId = 0)
        {
          
            // data = Filter(data, groupId, src);
            var query = (from qd in db.TblSaleOrderDetail
                         join p in db.TblProduct
                         on qd.ProductId equals p.ProductId
                         join w in db.TblWareHouse
                          on qd.WHId equals w.Whid

                         where qd.SaleOrderId == SaleOrderId
                         select new TblSaleOrderDetail
                         {

                            Id=qd.Id,
        SaleOrderId =qd.SaleOrderId,
         ProductId =qd.ProductId,
       Quantity =qd.Quantity,
       WHId =qd.WHId,
       PriceNet =qd.PriceNet,
       PriceVat = qd.PriceVat,
        PriceTot= qd.PriceTot,
       DiscountNet =qd.DiscountNet,
       DiscountVat  =qd.DiscountVat,
        DiscountTot =qd.DiscountTot,
        Discount=qd.Discount,
       TotalNet  = qd.TotalNet,
       TotalVat = qd.TotalVat,
       TotalTot = qd.TotalTot,
         Remark = qd.Remark,
       CalType=qd.CalType,
      WareHouse = w,
       Product = p
                         });
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

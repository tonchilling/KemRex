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

        public TblSaleOrder GetFull(int id)
        {
            TblSaleOrder tblSaleOrder= db.TblSaleOrder
                   .Where(x => x.SaleOrderId == id)
                   .FirstOrDefault() ?? new TblSaleOrder()
                   {
                       CreatedDate = DateTime.Now
                   };

           
            //var customerID = Convert.ToInt32(tblSaleOrder.CustomerId);
            ////tblSaleOrder.Customers = new List<TblCustomer>();
            //var customerDto = db.TblCustomer.Where(cus => cus.CustomerId == customerID).FirstOrDefault() ?? new TblCustomer()
            //{
            //    CreatedDate = DateTime.Now
            //};

              tblSaleOrder.Customer =  (from customer in db.TblCustomer where customer.CustomerId == tblSaleOrder.CustomerId select customer).FirstOrDefault();
            tblSaleOrder.TblSaleOrderDetail = (from q in db.TblSaleOrderDetail.Include(x => x.Product)
                                               where q.SaleOrderId == id select new TblSaleOrderDetail
            {
                Id = q.Id,
        SaleOrderId = q.SaleOrderId,
                ProductId = q.ProductId,
                Quantity = q.Quantity,
                PriceNet = q.PriceNet,
                PriceVat = q.PriceVat,
                PriceTot = q.PriceTot,
                DiscountNet = q.DiscountNet,
                DiscountVat = q.DiscountVat,
                DiscountTot = q.DiscountTot,
                TotalNet = q.TotalNet,
                TotalVat = q.TotalVat,
                TotalTot = q.TotalTot,
                Remark = q.Remark,
                Product=q.Product
            }).ToList();

            tblSaleOrder.TblSaleOrderAttachment = (from q in db.TblSaleOrderAttachment where q.SaleOrderId == id select new TblSaleOrderAttachment {

                Id = q.Id,
                SaleOrderId = q.SaleOrderId,
                AttachmentPath = q.AttachmentPath,
                AttachmentOrder = q.AttachmentOrder,
                AttachmentRemark = q.AttachmentRemark
               
            }).ToList();
            return tblSaleOrder;
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

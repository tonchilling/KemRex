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
    public class ProductOfWareHouseModule : IModule<TblProductOfWareHouse, int>
    {
        private readonly mainContext db;
        public ProductOfWareHouseModule(mainContext context)
        {
            db = context;
        }

       
   
        public void Delete(TblProductOfWareHouse ob)
        {
            if (IsExist(ob.ProductId))
            { db.TblProductOfWareHouse.Remove(ob); }
        }

        public TblProductOfWareHouse Get(int id)
        {
            return db.TblProductOfWareHouse
                .Where(x => x.ProductId == id )
                .FirstOrDefault() ?? new TblProductOfWareHouse()
                    {
                       
                    };
        }

        public TblProductOfWareHouse Get(int id,int whid)
        {
            return db.TblProductOfWareHouse
                .Where(x => x.ProductId == id && x.Whid == whid)
                .FirstOrDefault() ?? new TblProductOfWareHouse()
                {

                };
        }




        public List<TblProductOfWareHouse> GetByCondition(string productCode, string productName)
        {




            var data = db.TblProductOfWareHouse.Select(o => new TblProductOfWareHouse
            {
                ProductId = o.ProductId,
                Whid = o.Whid,
                WareHouseName= (from q in db.TblWareHouse.Where(xx => xx.Whid == o.Whid) select q.Whname).FirstOrDefault(),
                ProductName = (from q in db.TblProduct.Where(xx => xx.ProductId == o.ProductId) select q.ProductName).FirstOrDefault() ,
                PriceNet = o.PriceNet,
                QtyStock = o.QtyStock,
               


            }).ToList();


            return data;
        }

        public List<TblProductOfWareHouse> Gets(int productid)
        {
            return db.TblProductOfWareHouse.Where(xx => xx.ProductId == productid).Select(o => new TblProductOfWareHouse
            {
                ProductId = o.ProductId,
                Whid = o.Whid,
                WareHouseName = (from q in db.TblWareHouse.Where(xx => xx.Whid == o.Whid) select q.Whname).FirstOrDefault(),
                ProductName = (from q in db.TblProduct.Where(xx => xx.ProductId == o.ProductId) select q.ProductName).FirstOrDefault(),
                PriceNet = o.PriceNet,
                QtyStock = o.QtyStock,



            }).ToList();
        }

        public List<TblProductOfWareHouse> Gets(int page = 1, int size = 0, string src = "")
        {
            var data = db.TblProductOfWareHouse
                .AsQueryable();
            // data=data.OrderBy(x => x.ProductId);
            // data = Filter(data, groupId, src);

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public List<TblProductOfWareHouse> GetList()
        {
            var data = (from o in db.TblProductOfWareHouse
                        select new TblProductOfWareHouse
                        {
                            ProductId = o.ProductId,
                            Whid = o.Whid,
                            WareHouseName = (from q in db.TblWareHouse.Where(xx => xx.Whid == o.Whid) select q.Whname).FirstOrDefault(),
                            ProductName = (from q in db.TblProduct.Where(xx => xx.ProductId == o.ProductId) select q.ProductName).FirstOrDefault(),
                            PriceNet = o.PriceNet,
                            QtyStock = o.QtyStock,

                        });
            return data.ToList();
        }
        public bool IsExist(int id)
        {
             return db.TblProduct.Where(x => x.ProductId == id).Count() > 0 ? true : false; 
        }

        public void Set(TblProductOfWareHouse ob)
        {
            if (!ob.IsUpdate)
            { db.TblProductOfWareHouse.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

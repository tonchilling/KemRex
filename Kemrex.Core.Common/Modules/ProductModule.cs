﻿using Kemrex.Core.Common.Interfaces;
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
    public class ProductModule : IModule<TblProduct, int>
    {
        private readonly mainContext db;
        public ProductModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblProduct.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        private IQueryable<TblProduct> Filter(IQueryable<TblProduct> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.ProductName.Contains(src)); }
            return data;
        }
        public void Delete(TblProduct ob)
        {
            if (IsExist(ob.ProductId))
            { db.TblProduct.Remove(ob); }
        }

        public TblProduct Get(int id)
        {
            return db.TblProduct
                .Include(x => x.Category)
                .Include(x => x.Model)
                .Where(x => x.ProductId == id)
                .FirstOrDefault() ?? new TblProduct()
                    {
                        Category = new TblProductCategory(),
                        Model = new TblProductModel()
                    };
        }

        public List<TblProduct> Gets(string categoryType)
        {
            return db.TblProduct
                .Include(x => x.Category)
                .Include(x => x.Model)
                .Where(x => x.Category.Accessory == categoryType)
               .ToList();
        }

        public List<TblProduct> Gets(int page = 1, int size = 0, string src = "")
        {
            var data = db.TblProduct
                .Include(x => x.Category)
                .Include(x => x.Model)
                .AsQueryable();
            // data=data.OrderBy(x => x.ProductId);
            // data = Filter(data, groupId, src);

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public List<TblProduct> GetList()
        {
            var data = (from i in db.TblProduct
                        select new TblProduct
                        {
                            CategoryId = i.CategoryId,
                            Category = i.Category,
                            ProductId = i.ProductId,
                            ProductCode = i.ProductCode,
                            ProductName = i.ProductName,
                            PriceNet = i.PriceNet,
                            PriceTot = i.PriceTot,
                            PriceVat = i.PriceVat,
                            QtyStock = i.QtyStock

                        });
            return data.ToList();
        }
        public bool IsExist(int id)
        {
             return db.TblProduct.Where(x => x.ProductId == id).Count() > 0 ? true : false; 
        }

        public void Set(TblProduct ob)
        {
            if (ob.ProductId == 0)
            { db.TblProduct.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

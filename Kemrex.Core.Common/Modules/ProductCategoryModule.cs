using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class ProductCategoryModule : IModule<TblProductCategory, int>
    {
        private readonly mainContext db;
        public ProductCategoryModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblProductCategory.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblProductCategory ob)
        {
            if (IsExist(ob.CategoryId))
            { db.TblProductCategory.Remove(ob); }
        }

        private IQueryable<TblProductCategory> Filter(IQueryable<TblProductCategory> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.CategoryName.Contains(src)); }
            return data;
        }

        public TblProductCategory Get(int id)
        {
            return db.TblProductCategory
                .Where(x => x.CategoryId == id).FirstOrDefault() ?? new TblProductCategory();
        }

        public List<TblProductCategory> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblProductCategory
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.CategoryOrder);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblProductCategory.Where(x => x.CategoryId == id).Count() > 0 ? true : false; }

        public void Set(TblProductCategory ob)
        {
            if (ob.CategoryId == 0)
            { db.TblProductCategory.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

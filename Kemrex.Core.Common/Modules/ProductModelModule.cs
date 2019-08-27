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
    public class ProductModelModule : IModule<TblProductModel, int>
    {
        private readonly mainContext db;
        public ProductModelModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblProductModel.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblProductModel ob)
        {
            if (IsExist(ob.ModelId))
            { db.TblProductModel.Remove(ob); }
        }

        private IQueryable<TblProductModel> Filter(IQueryable<TblProductModel> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.ModelName.Contains(src)); }
            return data;
        }


   

        public TblProductModel Get(int id)
        {
            return db.TblProductModel
                .Include(x => x.Category)
                .Where(x => x.ModelId == id).FirstOrDefault() ?? new TblProductModel() { Category = new TblProductCategory() };
        }

        public List<TblProductModel> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblProductModel
                .Include(x => x.Category)
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.ModelOrder);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblProductModel.Where(x => x.ModelId == id).Count() > 0 ? true : false; }

        public void Set(TblProductModel ob)
        {
            if (ob.ModelId == 0)
            { db.TblProductModel.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

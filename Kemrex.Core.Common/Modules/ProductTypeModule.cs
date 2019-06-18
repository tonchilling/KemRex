using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class ProductTypeModule : IModule<TblProductType, int>
    {
        private readonly mainContext db;
        public ProductTypeModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblProductType.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblProductType ob)
        {
            if (IsExist(ob.TypeId))
            { db.TblProductType.Remove(ob); }
        }

        private IQueryable<TblProductType> Filter(IQueryable<TblProductType> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.TypeName.Contains(src)); }
            return data;
        }

        public TblProductType Get(int id)
        {
            return db.TblProductType
                .Where(x => x.TypeId == id).FirstOrDefault() ?? new TblProductType();
        }

        public List<TblProductType> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblProductType
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.TypeOrder);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblProductType.Where(x => x.TypeId == id).Count() > 0 ? true : false; }

        public void Set(TblProductType ob)
        {
            if (ob.TypeId == 0)
            { db.TblProductType.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class SysCategoryModule : IModule<SysCategory, int>
    {
        private readonly mainContext db;
        public SysCategoryModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.SysCategory.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(SysCategory ob)
        {
            if (IsExist(ob.CategoryId))
            { db.SysCategory.Remove(ob); }
        }

        private IQueryable<SysCategory> Filter(IQueryable<SysCategory> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.CategoryName.Contains(src)); }
            return data;
        }

        public SysCategory Get(int id)
        {
            return db.SysCategory
                .Where(x => x.CategoryId == id).FirstOrDefault() ?? new SysCategory();
        }

        public List<SysCategory> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.SysCategory
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.CategoryName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.SysCategory.Where(x => x.CategoryId == id).Count() > 0 ? true : false; }

        public void Set(SysCategory ob)
        {
            if (ob.CategoryId == 0)
            { db.SysCategory.Add(ob); }
            else { db.SysCategory.Update(ob); }
        }
    }
}

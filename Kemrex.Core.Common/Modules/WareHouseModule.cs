using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class WareHouseModule : IModule<TblWareHouse, int>
    {
        private readonly mainContext db;
        public WareHouseModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblWareHouse.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblWareHouse ob)
        {
            if (IsExist(ob.Whid))
            { db.TblWareHouse.Remove(ob); }
        }

        private IQueryable<TblWareHouse> Filter(IQueryable<TblWareHouse> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.Whname.Contains(src)); }
            return data;
        }

        public TblWareHouse Get(int id)
        {
            return db.TblWareHouse
                .Where(x => x.Whid == id).FirstOrDefault() ?? new TblWareHouse();
        }

        public List<TblWareHouse> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblWareHouse
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.Whcode);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblWareHouse.Where(x => x.Whid == id).Count() > 0 ? true : false; }

        public void Set(TblWareHouse ob)
        {
            if (ob.Whid == 0)
            { db.TblWareHouse.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

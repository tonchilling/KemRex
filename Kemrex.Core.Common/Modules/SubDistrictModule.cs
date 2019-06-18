using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class SubDistrictModule : IModule<SysSubDistrict, int>
    {
        private readonly mainContext db;
        public SubDistrictModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.SysSubDistrict.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(SysSubDistrict ob)
        {
            if (IsExist(ob.SubDistrictId))
            { db.SysSubDistrict.Remove(ob); }
        }

        private IQueryable<SysSubDistrict> Filter(IQueryable<SysSubDistrict> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.SubDistrictNameTh.Contains(src) || x.SubDistrictNameEn.Contains(src)); }
            return data;
        }

        public SysSubDistrict Get(int id)
        {
            return db.SysSubDistrict
                .Include(x => x.District)
                    .ThenInclude(x => x.State)
                .Where(x => x.SubDistrictId == id).FirstOrDefault() ?? new SysSubDistrict()
                    {
                        District = new SysDistrict() { State = new SysState() }
                    };
        }

        public List<SysSubDistrict> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.SysSubDistrict
                .Include(x => x.District)
                    .ThenInclude(x => x.State)
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.SubDistrictNameTh);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.SysSubDistrict.Where(x => x.SubDistrictId == id).Count() > 0 ? true : false; }

        public void Set(SysSubDistrict ob)
        {
            if (ob.SubDistrictId == 0)
            { db.SysSubDistrict.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

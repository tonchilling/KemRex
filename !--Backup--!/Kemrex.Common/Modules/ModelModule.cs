using Kemrex.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kemrex.Common.Modules
{
    public class ModelModule
    {
        private dbContext db;
        public ModelModule(dbContext _db)
        {
            db = _db;
        }

        public TblModelInfo Get(int id)
        { return db.TblModelInfo.Find(id); }

        public List<string> GetModelGroups()
        {
            var data = db.TblModelInfo.Select(x => x.ModelGroup).Distinct();
            return data.ToList();
        }

        public List<TblModelInfo> GetModel(string group = "")
        {
            var data = db.TblModelInfo.AsQueryable();
            if (group != "")
            { data = data.Where(x => x.ModelGroup == group); }
            return data.ToList();
        }
    }
}

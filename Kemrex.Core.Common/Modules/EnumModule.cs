using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Modules
{
    public class EnumModule
    {
        private readonly mainContext db;
        public EnumModule(mainContext context)
        {
            db = context;
        }

        public EnmPrefix PrefixGet(int id)
        {
            return db.EnmPrefix
                .Where(x => x.PrefixId == id)
                .FirstOrDefault();
        }
        public List<EnmPrefix> PrefixGets()
        {
            return db.EnmPrefix
                .ToList();
        }
    }
}

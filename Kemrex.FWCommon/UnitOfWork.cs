using Kemrex.Database;
using Kemrex.FWCommon.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.FWCommon
{
    public class UnitOfWork
    {
        public dbContext db;
        public UnitOfWork()
        {
            db = new dbContext();
        }

        private AccountModule _Account;
        private ModelInfoModule _ModelInfo;
        private RoleModule _Role;
        private SystemModule _System;
        public AccountModule Account => _Account ?? (_Account = new AccountModule(db));
        public ModelInfoModule ModelInfo => _ModelInfo ?? (_ModelInfo = new ModelInfoModule(db));
        public RoleModule Role => _Role ?? (_Role = new RoleModule(db));
        public SystemModule System => _System ?? (_System = new SystemModule(db));

        public void SaveChanges()
        { db.SaveChanges(); }
    }
}

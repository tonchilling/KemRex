using Kemrex.Common.Modules;
using Kemrex.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kemrex.Common
{
    public class UnitOfWork
    {
        private dbContext db;
        public UnitOfWork()
        {
            db = new dbContext();
        }

        private ModelModule _Model;
        public ModelModule Model => _Model ?? (_Model = new ModelModule(db));
    }
}

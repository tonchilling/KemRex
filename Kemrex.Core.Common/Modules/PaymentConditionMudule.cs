using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class PaymentConditionMudule : IModule<EnmPaymentCondition, int>
    {
        private readonly mainContext db;
        public PaymentConditionMudule(mainContext context)
        {
            db = context;
        }
        public void Delete(EnmPaymentCondition ob)
        {
            throw new System.NotImplementedException();
        }

        public List<EnmPaymentCondition> Gets()
        {
            return db.EnmPaymentCondition
                        .OrderBy(p => p.ConditionId)
                        .ToList();
        }
        public EnmPaymentCondition Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExist(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Set(EnmPaymentCondition ob)
        {
            throw new System.NotImplementedException();
        }
    }
}

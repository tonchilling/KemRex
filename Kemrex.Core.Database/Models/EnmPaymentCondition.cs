using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class EnmPaymentCondition
    {
        public EnmPaymentCondition()
        {
            EnmPaymentConditionTerm = new HashSet<EnmPaymentConditionTerm>();
        }

        public int ConditionId { get; set; }
        public string ConditionName { get; set; }
        public int ConditionTerm { get; set; }
        public bool? FlagActive { get; set; }

        public virtual ICollection<EnmPaymentConditionTerm> EnmPaymentConditionTerm { get; set; }
    }
}

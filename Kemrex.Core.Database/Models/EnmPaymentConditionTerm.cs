using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class EnmPaymentConditionTerm
    {
        public int ConditionId { get; set; }
        public int TermNo { get; set; }
        public int TermPercent { get; set; }
        public int TermAmount { get; set; }
        public bool FlagLast { get; set; }

        public virtual EnmPaymentCondition Condition { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    [Serializable]
    public partial class EnmStatusQuotation
    {
        public EnmStatusQuotation()
        {
            TblQuotation = new HashSet<TblQuotation>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int StatusOrder { get; set; }

        public virtual ICollection<TblQuotation> TblQuotation { get; set; }
    }
}

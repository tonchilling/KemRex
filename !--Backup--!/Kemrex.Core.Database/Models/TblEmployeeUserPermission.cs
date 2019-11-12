using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblEmployeeUserPermission
    {
        public int AccountId { get; set; }
        public int FunId { get; set; }
        public int ViewAccountId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

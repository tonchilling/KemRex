using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TeamOperation
    {
        public TeamOperation()
        {
            TblJobOrder = new HashSet<TblJobOrder>();
            TblSaleOrder = new HashSet<TblSaleOrder>();
            TeamOperationDetail = new HashSet<TeamOperationDetail>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public long ManagerId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual SysAccount Manager { get; set; }
        public virtual ICollection<TblJobOrder> TblJobOrder { get; set; }
        public virtual ICollection<TblSaleOrder> TblSaleOrder { get; set; }
        public virtual ICollection<TeamOperationDetail> TeamOperationDetail { get; set; }
    }
}

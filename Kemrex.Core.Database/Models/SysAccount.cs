using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysAccount
    {
        public SysAccount()
        {
            CalcAccountStaffAccount = new HashSet<CalcAccountStaff>();
            CalcAccountStaffStaff = new HashSet<CalcAccountStaff>();
            SysAccountRole = new HashSet<SysAccountRole>();
            TblEmployee = new HashSet<TblEmployee>();
            TeamOperation = new HashSet<TeamOperation>();
            TeamOperationDetail = new HashSet<TeamOperationDetail>();
            TeamSale = new HashSet<TeamSale>();
            TeamSaleDetail = new HashSet<TeamSaleDetail>();
        }

        public long AccountId { get; set; }
        public string AccountAvatar { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public string AccountFirstName { get; set; }
        public string AccountLastName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountRemark { get; set; }
        public int FlagStatus { get; set; }
        public bool FlagSystem { get; set; }
        public bool FlagAdminCalc { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<CalcAccountStaff> CalcAccountStaffAccount { get; set; }
        public virtual ICollection<CalcAccountStaff> CalcAccountStaffStaff { get; set; }
        public virtual ICollection<SysAccountRole> SysAccountRole { get; set; }
        public virtual ICollection<TblEmployee> TblEmployee { get; set; }
        public virtual ICollection<TeamOperation> TeamOperation { get; set; }
        public virtual ICollection<TeamOperationDetail> TeamOperationDetail { get; set; }
        public virtual ICollection<TeamSale> TeamSale { get; set; }
        public virtual ICollection<TeamSaleDetail> TeamSaleDetail { get; set; }
    }
}

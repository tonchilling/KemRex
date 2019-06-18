using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblEmployee
    {
        public TblEmployee()
        {
            InverseLead = new HashSet<TblEmployee>();
            TblQuotation = new HashSet<TblQuotation>();
            TblSaleOrder = new HashSet<TblSaleOrder>();
        }

        public int EmpId { get; set; }
        public string EmpCode { get; set; }
        public long? AccountId { get; set; }
        public int? PrefixId { get; set; }
        public int? EmpTypeId { get; set; }
        public string EmpNameTh { get; set; }
        public string EmpNameEn { get; set; }
        public string EmpPid { get; set; }
        public string EmpMobile { get; set; }
        public string EmpEmail { get; set; }
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public int? LeadId { get; set; }
        public DateTime? EmpApplyDate { get; set; }
        public DateTime? EmpPromoteDate { get; set; }
        public string EmpAddress { get; set; }
        public string EmpPostcode { get; set; }
        public string EmpSignature { get; set; }
        public string EmpRemark { get; set; }
        public int StatusId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual SysAccount Account { get; set; }
        public virtual TblDepartment Department { get; set; }
        public virtual TblEmployee Lead { get; set; }
        public virtual TblPosition Position { get; set; }
        public virtual EnmPrefix Prefix { get; set; }
        public virtual EnmStatusEmployee Status { get; set; }
        public virtual ICollection<TblEmployee> InverseLead { get; set; }
        public virtual ICollection<TblQuotation> TblQuotation { get; set; }
        public virtual ICollection<TblSaleOrder> TblSaleOrder { get; set; }
    }
}

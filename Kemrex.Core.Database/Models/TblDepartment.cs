using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblDepartment
    {
        public TblDepartment()
        {
            TblDepartmentPosition = new HashSet<TblDepartmentPosition>();
            TblEmployee = new HashSet<TblEmployee>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TblDepartmentPosition> TblDepartmentPosition { get; set; }
        public virtual ICollection<TblEmployee> TblEmployee { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblDepartmentPosition
    {
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public string PositionName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TblDepartment Department { get; set; }
    }
}

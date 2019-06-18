using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblCalLoad
    {
        public int CalId { get; set; }
        public string ProjectName { get; set; }
        public string CalRemark { get; set; }
        public decimal InputC { get; set; }
        public decimal InputDegree { get; set; }
        public decimal InputSafeLoad { get; set; }
        public int ModelId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

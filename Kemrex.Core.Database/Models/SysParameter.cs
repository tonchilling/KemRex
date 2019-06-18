using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysParameter
    {
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
        public string ParamType { get; set; }
        public int? ParamLength { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    [Serializable]
    public partial class TblWareHouse
    {
        public int Whid { get; set; }
        public string Whcode { get; set; }
        public string Whname { get; set; }
        public int? FlagActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? Updatedby { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

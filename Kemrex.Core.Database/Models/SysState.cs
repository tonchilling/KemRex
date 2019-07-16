using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class SysState
    {
        public SysState()
        {
            SysDistrict = new HashSet<SysDistrict>();
        }

        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateNameTh { get; set; }
        public string StateNameEn { get; set; }
        public int? GeoId { get; set; }

        public virtual SysGeography Geo { get; set; }
        public virtual ICollection<SysDistrict> SysDistrict { get; set; }
    }


    public class TransactionStatus
    {
        public string QuoState1 { get; set; }
        public string QuoState2 { get; set; }
        public string QuoState3 { get; set; }
        public string QuoState4 { get; set; }
        public string SOState1 { get; set; }
        public string SOState2 { get; set; }
        public string SOState3 { get; set; }
        public string InvState1 { get; set; }
        public string InvState2 { get; set; }
        public string InvState3 { get; set; }
        public string InvState4 { get; set; }

      
    }
}

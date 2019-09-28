using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kemrex.Core.Database.Models
{
    public partial class TblBankAccount
    {
        public int AcctId { get; set; }
        public string AcctNo { get; set; }
        public string AcctName { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public long UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Database.Models
{
    public partial class SysAccount
    {
        [NotMapped]
        public string AccountName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(AccountFirstName);
                if (!string.IsNullOrWhiteSpace(AccountLastName))
                { sb.Append(" " + AccountLastName); }
                return sb.ToString();
            }
        }
    }
}

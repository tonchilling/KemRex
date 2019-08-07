using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TransferHeaderReference
    {
        public int TransferId { get; set; }
        public int RefTransferId { get; set; }

        public virtual TransferHeader Transfer { get; set; }
        public virtual List<TransferHeader> RefTransfer { get; set; }
    }
}

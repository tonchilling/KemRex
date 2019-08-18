﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{
    [Serializable]
    public partial class TransferDetail
    {

    

        public int TransferId { get; set; }
        public int RefTransferId { get; set; }
        [NotMapped]
        public string RefTransferNo { get; set; }

        [NotMapped]
        public string TransferNo { get; set; }
        public int? Seq { get; set; }
        public int? ProductId { get; set; }
        public string CurrentQty { get; set; }
        public int? RequestQty { get; set; }
        public string RequestUnit { get; set; }
        public decimal? RequestUnitFactor { get; set; }
        public DateTime? LastModified { get; set; }

        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public string ProductCode { get; set; }

        public virtual TblProduct Product { get; set; }
      /*  public virtual TransferHeader TransferHeader { get; set; }*/
    }


    [Serializable]
    public partial class TransferRefHeader
    {
        public int TransferId { get; set; }
        public int RefTransferId { get; set; }
        public string RefTransferNo { get; set; }
        public string TransferNo { get; set; }
    }
    }

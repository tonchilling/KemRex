using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblSaleOrderAttachment
    {
        public int Id { get; set; }
        public int SaleOrderId { get; set; }
        public string AttachmentPath { get; set; }
        public int AttachmentOrder { get; set; }
        public string AttachmentRemark { get; set; }
       
    public virtual TblSaleOrder SaleOrder { get; set; }
    }
}

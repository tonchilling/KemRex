namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblSaleOrderAttachment")]
    public partial class TblSaleOrderAttachment
    {
        public int Id { get; set; }

        public int SaleOrderId { get; set; }

        [Required]
        [StringLength(500)]
        public string AttachmentPath { get; set; }

        public int AttachmentOrder { get; set; }

        public string AttachmentRemark { get; set; }

        public virtual TblSaleOrder TblSaleOrder { get; set; }
    }
}

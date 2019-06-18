namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblInvoice")]
    public partial class TblInvoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNo { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int SaleOrderId { get; set; }

        public string InvoiceRemark { get; set; }

        public int InvoiceTerm { get; set; }

        public decimal? InvoiceAmount { get; set; }

        public int? StatusId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual TblSaleOrder TblSaleOrder { get; set; }
    }
}

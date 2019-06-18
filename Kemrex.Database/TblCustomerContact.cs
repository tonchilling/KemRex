namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblCustomerContact")]
    public partial class TblCustomerContact
    {
        [Key]
        public int ContactId { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(200)]
        public string ContactName { get; set; }

        [StringLength(200)]
        public string ContactPosition { get; set; }

        [StringLength(500)]
        public string ContactEmail { get; set; }

        [StringLength(50)]
        public string ContactPhone { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual TblCustomer TblCustomer { get; set; }
    }
}

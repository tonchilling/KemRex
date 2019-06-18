namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblCustomer")]
    public partial class TblCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblCustomer()
        {
            TblCustomerAddress = new HashSet<TblCustomerAddress>();
            TblCustomerContact = new HashSet<TblCustomerContact>();
            TblQuotation = new HashSet<TblQuotation>();
            TblSaleOrder = new HashSet<TblSaleOrder>();
        }

        [Key]
        public int CustomerId { get; set; }

        public int? PrefixId { get; set; }

        [Required]
        [StringLength(500)]
        public string CustomerName { get; set; }

        [StringLength(500)]
        public string CustomerAvatar { get; set; }

        [StringLength(20)]
        public string CustomerTaxId { get; set; }

        [StringLength(50)]
        public string CustomerPhone { get; set; }

        [StringLength(50)]
        public string CustomerFax { get; set; }

        [StringLength(200)]
        public string CustomerEmail { get; set; }

        public int CustomerType { get; set; }

        public int? GroupId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual EnmPrefix EnmPrefix { get; set; }

        public virtual TblCustomerGroup TblCustomerGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCustomerAddress> TblCustomerAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCustomerContact> TblCustomerContact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblQuotation> TblQuotation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSaleOrder> TblSaleOrder { get; set; }
    }
}

namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblCustomerAddress")]
    public partial class TblCustomerAddress
    {
        [Key]
        public int AddressId { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressName { get; set; }

        [Required]
        [StringLength(500)]
        public string AddressValue { get; set; }

        public int? SubDistrictId { get; set; }

        [StringLength(10)]
        public string AddressPostcode { get; set; }

        [StringLength(500)]
        public string AddressContact { get; set; }

        [StringLength(500)]
        public string AddressContactEmail { get; set; }

        [StringLength(500)]
        public string AddressContactPhone { get; set; }

        public int AddressOrder { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual SysSubDistrict SysSubDistrict { get; set; }

        public virtual TblCustomer TblCustomer { get; set; }
    }
}

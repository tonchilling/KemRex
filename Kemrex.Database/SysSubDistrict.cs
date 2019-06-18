namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysSubDistrict")]
    public partial class SysSubDistrict
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysSubDistrict()
        {
            TblCustomerAddress = new HashSet<TblCustomerAddress>();
            TblKpt = new HashSet<TblKpt>();
        }

        [Key]
        public int SubDistrictId { get; set; }

        [StringLength(6)]
        public string SubDistrictCode { get; set; }

        [StringLength(256)]
        public string SubDistrictNameTH { get; set; }

        [StringLength(256)]
        public string SubDistrictNameEN { get; set; }

        public int? DistrictId { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        public virtual SysDistrict SysDistrict { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCustomerAddress> TblCustomerAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblKpt> TblKpt { get; set; }
    }
}

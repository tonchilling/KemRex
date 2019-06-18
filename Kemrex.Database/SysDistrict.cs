namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysDistrict")]
    public partial class SysDistrict
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysDistrict()
        {
            SysPostcode = new HashSet<SysPostcode>();
            SysSubDistrict = new HashSet<SysSubDistrict>();
        }

        [Key]
        public int DistrictId { get; set; }

        [StringLength(4)]
        public string DistrictCode { get; set; }

        [StringLength(256)]
        public string DistrictNameTH { get; set; }

        [StringLength(256)]
        public string DistrictNameEN { get; set; }

        public int? StateId { get; set; }

        public virtual SysState SysState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysPostcode> SysPostcode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysSubDistrict> SysSubDistrict { get; set; }
    }
}

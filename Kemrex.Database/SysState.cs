namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysState")]
    public partial class SysState
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysState()
        {
            SysDistrict = new HashSet<SysDistrict>();
        }

        [Key]
        public int StateId { get; set; }

        [StringLength(2)]
        public string StateCode { get; set; }

        [StringLength(256)]
        public string StateNameTH { get; set; }

        [StringLength(256)]
        public string StateNameEN { get; set; }

        public int? GeoId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysDistrict> SysDistrict { get; set; }

        public virtual SysGeography SysGeography { get; set; }
    }
}

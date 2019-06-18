namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysGeography")]
    public partial class SysGeography
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysGeography()
        {
            SysState = new HashSet<SysState>();
        }

        [Key]
        public int GeoId { get; set; }

        [StringLength(256)]
        public string GeoNameTH { get; set; }

        [StringLength(256)]
        public string GeoNameEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysState> SysState { get; set; }
    }
}

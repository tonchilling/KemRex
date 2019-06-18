namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TblModelSeries
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblModelSeries()
        {
            TblModelInfo = new HashSet<TblModelInfo>();
        }

        [Key]
        public int SeriesId { get; set; }

        [Required]
        [StringLength(100)]
        public string SeriesName { get; set; }

        [StringLength(500)]
        public string SeriesImage { get; set; }

        public int SeriesOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblModelInfo> TblModelInfo { get; set; }
    }
}

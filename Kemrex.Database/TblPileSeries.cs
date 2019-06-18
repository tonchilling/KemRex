namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TblPileSeries
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblPileSeries()
        {
            TblPile = new HashSet<TblPile>();
        }

        [Key]
        public int SeriesId { get; set; }

        [Required]
        [StringLength(100)]
        public string SeriesName { get; set; }

        [Required]
        [StringLength(500)]
        public string SeriesImage { get; set; }

        public int SeriesOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblPile> TblPile { get; set; }
    }
}

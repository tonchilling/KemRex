namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblKptStation")]
    public partial class TblKptStation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblKptStation()
        {
            TblKptStationAttachment = new HashSet<TblKptStationAttachment>();
            TblKptStationDetail = new HashSet<TblKptStationDetail>();
        }

        [Key]
        public int StationId { get; set; }

        public int KptId { get; set; }

        [Required]
        [StringLength(100)]
        public string StationName { get; set; }

        [Required]
        [StringLength(200)]
        public string StationTestBy { get; set; }

        public string StationRemark { get; set; }

        public int StationOrder { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual TblKpt TblKpt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblKptStationAttachment> TblKptStationAttachment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblKptStationDetail> TblKptStationDetail { get; set; }
    }
}

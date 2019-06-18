namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblKpt")]
    public partial class TblKpt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblKpt()
        {
            TblKptAttachment = new HashSet<TblKptAttachment>();
            TblKptDetail = new HashSet<TblKptDetail>();
            TblKptStation = new HashSet<TblKptStation>();
        }

        [Key]
        public int KptId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; }

        [StringLength(200)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string KptLatitude { get; set; }

        [StringLength(50)]
        public string KptLongtitude { get; set; }

        [StringLength(200)]
        public string KptLocation { get; set; }

        public int? SubDistrictId { get; set; }

        [StringLength(100)]
        public string KptStation { get; set; }

        public DateTime KptDate { get; set; }

        [StringLength(500)]
        public string TestBy { get; set; }

        public string KptRemark { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual SysSubDistrict SysSubDistrict { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblKptAttachment> TblKptAttachment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblKptDetail> TblKptDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblKptStation> TblKptStation { get; set; }
    }
}

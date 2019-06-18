namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblProductModel")]
    public partial class TblProductModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblProductModel()
        {
            TblProduct = new HashSet<TblProduct>();
        }

        [Key]
        public int ModelId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string ModelName { get; set; }

        public int ModelOrder { get; set; }

        public bool FlagActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblProduct> TblProduct { get; set; }

        public virtual TblProductCategory TblProductCategory { get; set; }
    }
}

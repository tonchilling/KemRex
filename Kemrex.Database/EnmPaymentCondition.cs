namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnmPaymentCondition")]
    public partial class EnmPaymentCondition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnmPaymentCondition()
        {
            EnmPaymentConditionTerm = new HashSet<EnmPaymentConditionTerm>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConditionId { get; set; }

        [Required]
        [StringLength(100)]
        public string ConditionName { get; set; }

        public int ConditionTerm { get; set; }

        public bool FlagActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnmPaymentConditionTerm> EnmPaymentConditionTerm { get; set; }
    }
}

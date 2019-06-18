namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnmPaymentConditionTerm")]
    public partial class EnmPaymentConditionTerm
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConditionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TermNo { get; set; }

        public int TermPercent { get; set; }

        public int TermAmount { get; set; }

        public bool FlagLast { get; set; }

        public virtual EnmPaymentCondition EnmPaymentCondition { get; set; }
    }
}

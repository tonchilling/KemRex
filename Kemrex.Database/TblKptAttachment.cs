namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblKptAttachment")]
    public partial class TblKptAttachment
    {
        [Key]
        public int AttachId { get; set; }

        public int KptId { get; set; }

        [Required]
        [StringLength(200)]
        public string AttachName { get; set; }

        [Required]
        [StringLength(500)]
        public string AttachPath { get; set; }

        public virtual TblKpt TblKpt { get; set; }
    }
}

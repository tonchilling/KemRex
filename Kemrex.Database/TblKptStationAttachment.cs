namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblKptStationAttachment")]
    public partial class TblKptStationAttachment
    {
        [Key]
        public int AttachId { get; set; }

        public int StationId { get; set; }

        [Required]
        [StringLength(200)]
        public string AttachName { get; set; }

        [Required]
        [StringLength(500)]
        public string AttachPath { get; set; }

        public virtual TblKptStation TblKptStation { get; set; }
    }
}

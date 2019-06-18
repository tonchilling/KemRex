namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblKptDetail")]
    public partial class TblKptDetail
    {
        public long Id { get; set; }

        public int KptId { get; set; }

        public int KptDepth { get; set; }

        public int BlowCount { get; set; }

        public virtual TblKpt TblKpt { get; set; }
    }
}

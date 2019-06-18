namespace Kemrex.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TblKptStationDetail")]
    public partial class TblKptStationDetail
    {
        public long Id { get; set; }

        public int StationId { get; set; }

        public int StationDepth { get; set; }

        public int StationBlowCount { get; set; }

        public virtual TblKptStation TblKptStation { get; set; }
    }
}

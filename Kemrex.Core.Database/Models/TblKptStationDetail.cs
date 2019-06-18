using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblKptStationDetail
    {
        public long Id { get; set; }
        public int StationId { get; set; }
        public int StationDepth { get; set; }
        public int StationBlowCount { get; set; }

        public virtual TblKptStation Station { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblKptStationAttachment
    {
        public int AttachId { get; set; }
        public int StationId { get; set; }
        public string AttachName { get; set; }
        public string AttachPath { get; set; }

        public virtual TblKptStation Station { get; set; }
    }
}

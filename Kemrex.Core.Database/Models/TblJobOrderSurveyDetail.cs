using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderSurveyDetail
    {
        public int JobOrderId { get; set; }
        public string SurveyDetailId { get; set; }
        public int? IsCheck { get; set; }
        public string Desc { get; set; }
        public int? StatusId { get; set; }

        public virtual TblJobOrder JobOrder { get; set; }
    }
}

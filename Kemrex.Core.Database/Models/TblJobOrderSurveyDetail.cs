using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kemrex.Core.Database.Models
{
    public partial class TblJobOrderSurveyDetail
    {
        public int JobOrderId { get; set; }
        public string SurveyDetailId { get; set; }
        public int? IsCheck { get; set; }
        public string Desc { get; set; }
        public int? StatusId { get; set; }
        [NotMapped]
        public int SubSurveyID { get; set; }
        [NotMapped]
        public int No { get; set; }

        public virtual TblJobOrder JobOrder { get; set; }
    }
}

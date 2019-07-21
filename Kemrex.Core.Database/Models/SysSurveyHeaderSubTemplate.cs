using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kemrex.Core.Database.Models
{
    public partial class SysSurveyHeaderSubTemplate
    {
        public int SurveyId { get; set; }
        public int SubSurveyId { get; set; }
        public string SurveyDetailID { get; set; }
        public int? Order { get; set; }
        public string Desc { get; set; }
        public int? IsReason { get; set; }
        public string Reason { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [NotMapped]
        public List<SysSurveyDetailTemplate> SurveyDetailList { get; set; }
    }
}

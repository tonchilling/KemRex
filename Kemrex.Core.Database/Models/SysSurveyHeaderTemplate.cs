using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kemrex.Core.Database.Models
{
    public partial class SysSurveyHeaderTemplate
    {
        public int SurveyId { get; set; }
        public int? Approve1 { get; set; }
        public int? Approve2 { get; set; }
        public int? Approve3 { get; set; }
        public int? Approve4 { get; set; }
        public int? StatusId { get; set; }
        public string Desc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [NotMapped]
        public List<SysSurveyHeaderSubTemplate> SurveySubList  { get; set; }
}
}

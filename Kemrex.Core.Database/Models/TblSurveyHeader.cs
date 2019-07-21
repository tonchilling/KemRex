using System;
using System.Collections.Generic;

namespace Kemrex.Core.Database.Models
{
    public partial class TblSurveyHeader
    {
        public int SurveyId { get; set; }
        public DateTime? SurveyDate { get; set; }
        public int? Approve1 { get; set; }
        public int? Approve2 { get; set; }
        public int? Approve3 { get; set; }
        public int? Approve4 { get; set; }
        public int? StatusId { get; set; }
        public int? RefSurveyId { get; set; }
        public string Desc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

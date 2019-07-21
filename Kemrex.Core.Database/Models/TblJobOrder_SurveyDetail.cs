using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Database.Models
{
    public class TblJobOrder_SurveyDetail
    {
        public int JobOrderID { get; set; }
        public int SurveyDetailID { get; set; }
        public int IsCheck { get; set; }
        public string Desc { get; set; }
        public int StatusId { get; set; } 
    }
}

using Kemrex.Core.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kemrex.Web.Main.Models
{
    public class CalendarDayModel
    {
        public DateTime Date { get; set; }
        public List<TblSaleOrder> Jobs { get; set; }
        public List<TblJobOrder> JobsOrder { get; set; }
        public TeamType TeamType { get; set; }

    }

}
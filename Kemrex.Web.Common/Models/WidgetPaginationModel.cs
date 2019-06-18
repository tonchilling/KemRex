using Kemrex.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Web.Common.Models
{
    public class WidgetPaginationModel
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public int Total { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Last { get { return ((Total / Size) + ((Total % Size) > 0 ? 1 : 0)); } }

        public string CurrentDisplayText { get; set; }

        public string SortExp { get; set; }
        public Dictionary<string, dynamic> SearchCri { get; set; }
        public WidgetPaginationModel(PathModel pth)
        {
            Total = 0;
            Page = 1;
            Size = 10;
            Area = pth.Area;
            Controller = pth.Controller;
            Action = pth.Action;
            CurrentDisplayText = "กำลังแสดงรายการที่ {0} - {1} , จากทั้งหมด {2} รายการ";
        }
        public WidgetPaginationModel(string mvcAction, string mvcController, string mvcArea)
        {
            Total = 0;
            Page = 1;
            Size = 10;
            Area = mvcArea;
            Controller = mvcController;
            Action = mvcAction;
            CurrentDisplayText = "กำลังแสดงรายการที่ {0} - {1} , จากทั้งหมด {2} รายการ";
        }
        public string CurrentDisplay
        {
            get
            {
                int curStart = ((Page - 1) * Size) + 1;
                int curEnd = (Page * Size) > Total ? Total : (Page * Size);
                return string.Format(CurrentDisplayText, curStart, curEnd, Total);
            }
        }
    }
}

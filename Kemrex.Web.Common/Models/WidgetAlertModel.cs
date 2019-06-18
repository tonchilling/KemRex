using DMN.Standard.Common.Extensions;
using Kemrex.Web.Common.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Web.Common.Models
{
    public class WidgetAlertModel
    {
        public WidgetAlertModel()
        {
            Type = AlertMsgType.Info;
            Icon = AlertMsgType.Info.Description();
        }
        private AlertMsgType _Type { get; set; }
        public AlertMsgType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                Icon = _Type.Description();
                switch (_Type)
                {
                    case AlertMsgType.Success:
                        Title = "สำเร็จ";
                        break;
                    case AlertMsgType.Warning:
                        Title = "คำเตือน";
                        break;
                    case AlertMsgType.Danger:
                        Title = "เกิดข้อผิดพลาด";
                        break;
                    case AlertMsgType.Info:
                    default:
                        Title = "ข้อมูล";
                        break;
                }
            }
        }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }
        public string TypeStr { get { return _Type.ToString().ToLower(); } }
    }
}

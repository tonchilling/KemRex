using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Web.Common.Constraints
{
    public enum AlertMsgType
    {
        [Description("ban")]
        Danger,
        [Description("info")]
        Info,
        [Description("warning")]
        Warning,
        [Description("check")]
        Success
    }
}

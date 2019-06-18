using Kemrex.Core.Common.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Models
{
    public class PathModel
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public PathModel(string _action, string _controller, string _area = KemrexPath.AREA_BASE)
        {
            Area = _area;
            Controller = _controller;
            Action = _action;
        }
    }
}
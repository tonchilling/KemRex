using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Web.Common.Models
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public int MenuOrder { get; set; }
        public string MvcArea { get; set; }
        public string MvcController { get; set; }
        public string MvcAction { get; set; }
        public List<MenuModel> SubMenus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Web.Common.Models.Layouts
{
    public class MenuLayoutModel
    {
        public List<int> ActiveMenuId { get; set; }
        public List<MenuModel> Menus { get; set; }
    }
}

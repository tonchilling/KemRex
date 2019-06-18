using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common;
using Kemrex.Core.Common.Constraints;
using Kemrex.Core.Database.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Common.ActionFilters
{
    public class AuthorizedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.Request.Cookies["sid"] != null)
            {
                HttpCookie ckSid = ctx.Request.Cookies["sid"];
                ckSid.Expires = DateTime.Now.AddMonths(1);
                if (ctx.Session["sid"] == null)
                { ctx.Session["sid"] = ckSid.Value; }
                ctx.Response.SetCookie(ckSid);
            }
            // check if session is supported
            if (HttpContext.Current.Session["sid"] == null)
            {
                filterContext.Result = new RedirectResult("~/Authen");
                return;
            }
            else
            {
                string constr = ConfigurationManager.AppSettings[ConfigKey.CONNECTION_DB];
                int sid = HttpContext.Current.Session["sid"].ToString().ParseInt();
                UnitOfWork uow = new UnitOfWork(constr);
                SysAccount ob = uow.Modules.Account.Get(sid);
                if (ob == null)
                {
                    filterContext.Result = new RedirectResult("~/Authen");
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
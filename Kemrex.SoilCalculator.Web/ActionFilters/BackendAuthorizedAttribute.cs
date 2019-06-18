using DMN.Standard.Common.Extensions;
using Kemrex.Database;
using Kemrex.FWCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.SoilCalculator.Web.ActionFilters
{
    public class BackendAuthorizedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.Request.Cookies["sid"] != null) {
                HttpCookie ckSid = ctx.Request.Cookies["sid"];
                ckSid.Expires = DateTime.Now.AddMonths(1);
                if (ctx.Session["sid"] == null)
                { ctx.Session["sid"] = ckSid.Value; }
                ctx.Response.SetCookie(ckSid);
            }
            // check if session is supported
            if (ctx.Session["sid"] == null)
            {
                filterContext.Result = new RedirectResult("~/Authen");
                return;
            }
            else
            {
                int sid = ctx.Session["sid"].ToString().ParseInt();
                UnitOfWork uow = new UnitOfWork();
                SysAccount ob = uow.db.SysAccount.Find(sid);
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
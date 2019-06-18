using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common;
using Kemrex.Core.Common.Constraints;
using Kemrex.Core.Common.Helper;
using Kemrex.Core.Common.Models;
using Kemrex.Core.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kemrex.Web.Common.Controllers
{
    public class BaseController : Controller
    {
        public string MVCArea => Request.RequestContext.RouteData.DataTokens["area"]?.Convert2String();
        public string MVCController => Request.RequestContext.RouteData.Values["controller"].Convert2String("Home");
        public string MVCAction => Request.RequestContext.RouteData.Values["action"].Convert2String("Index");

        public string GetCurrentTheme()
        {
            string nameDefault = SystemHelper.GetConfigurationKey(ConfigKey.DEFAULT_THEME, "Default");
            return Session["sysTheme" + MVCArea].Convert2String(nameDefault);
        }

        public string PathView(string FileName = "")
        {
            return string.Format("~/Views/{0}/{1}{2}/{3}",
                GetCurrentTheme(),
                (string.IsNullOrWhiteSpace(MVCArea) ? "Base/" : MVCArea + "/"),
                MVCController,
                (string.IsNullOrWhiteSpace(FileName) ? MVCAction : FileName) + ".cshtml");
        }

        public string PathViewShared(string FileName)
        {
            return string.Format("~/Views/{0}/{1}Shared/{2}",
                GetCurrentTheme(),
                (string.IsNullOrWhiteSpace(MVCArea) ? "Base/" : MVCArea + "/"),
                string.IsNullOrWhiteSpace(FileName) ? "" : FileName + ".cshtml");
        }

        #region Url

        private RouteValueDictionary GetRouteObject(PathModel pth, IDictionary<string, dynamic> routeObject = null)
        {
            if (routeObject == null) { routeObject = new Dictionary<string, dynamic>(); }
            if (routeObject.Keys.Contains("area")) { routeObject["area"] = pth.Area; }
            else { routeObject.Add("area", pth.Area); }
            if (routeObject.Keys.Contains("controller")) { routeObject["controller"] = pth.Controller; }
            else { routeObject.Add("controller", pth.Controller); }
            return new RouteValueDictionary(routeObject);
        }
        public string UrlAction<T>(PathModel pth,T id) where T : struct
        {
            Dictionary<string, dynamic> routeObject = new Dictionary<string, dynamic>() { { "id", id } };
            return Url.Action(pth.Action, GetRouteObject(pth, routeObject));
        }
        public string UrlAction(PathModel pth, IDictionary<string, dynamic> routeObject = null) =>
            Url.Action(pth.Action, GetRouteObject(pth, routeObject));
        public ActionResult UrlRedirect(PathModel pth, IDictionary<string, dynamic> routeObject = null) =>
            RedirectToAction(pth.Action, GetRouteObject(pth, routeObject));

        #endregion Url

        #region ViewEngine

        protected internal new ViewResult View()
        {
            return base.View(PathView());
        }

        protected internal new ViewResult View(object model)
        {
            return base.View(PathView(), model);
        }

        protected internal new ViewResult View(string FileName, object model)
        {
            return base.View(PathView(FileName), model);
        }

        protected internal new ViewResult View(string viewName, string masterName)
        {
            return base.View(viewName, masterName);
        }

        protected internal new PartialViewResult PartialView(string FileName, object model)
        {
            return base.PartialView(PathViewShared(FileName), model);
        }

        #endregion ViewEngine
    }
}
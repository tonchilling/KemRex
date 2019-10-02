using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common.Constraints;
using Kemrex.Core.Common.Helper;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class AuthenController : KemrexController
    {
        public ActionResult Index(string msg)
        {
            if (!string.IsNullOrWhiteSpace(msg))
            {
                WidgetAlertModel alert = new WidgetAlertModel() {
                    Message = msg,
                    Type = AlertMsgType.Danger
                };
                ViewBag.Alert = alert;
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName(KemrexPath.ACTION_SIGNIN)]
        public ActionResult SignIn()
        {
            string username = Request.Form["username"].ValidString();
            string password = Request.Form["password"].ValidString(false);
            bool flagRemember = Request.Form["remember"].ParseBoolean(false);
            SysAccount ob = uow.Modules.Account.GetByUsernameOrEmail(username);
            Dictionary<string, dynamic> routeObject = new Dictionary<string, dynamic>();
            if (ob == null)
            {
                logger.LogInfo("[Authen] Cannot found user " + username);
                Session.Remove("sid");
                routeObject.Add("msg", "ข้อมูลเข้าสู่ระบบไม่ถูกต้อง");
                return UrlRedirect(PathHelper.Authen, routeObject);
            }
            else
            {
                if (!Crypto.VerifyHashedPassword(ob.AccountPassword, password))
                {
                    logger.LogInfo("[Authen] Password not matched for " + username);
                    Session.Remove("sid");
                    routeObject.Add("msg", "ข้อมูลเข้าสู่ระบบไม่ถูกต้อง");
                    return UrlRedirect(PathHelper.Authen, routeObject);
                }
                else
                {
                    Session["sid"] = ob.AccountId;
                    if (flagRemember)
                    {
                        HttpCookie ckSid;
                        if (Request.Cookies["sid"] == null)
                        {
                            ckSid = new HttpCookie("sid", ob.AccountId.ToString())
                            {
                                Expires = DateTime.Now.AddHours(1)
                            };
                        }
                        else
                        {
                            ckSid = Request.Cookies["sid"];
                            ckSid.Value = ob.AccountId.ToString();
                            ckSid.Expires = DateTime.Now.AddHours(1);
                        }
                        Response.SetCookie(ckSid);
                    }
                    return UrlRedirect(PathHelper.Home);
                }
            }
        }

        [Authorized]
        public ActionResult SignOut()
        {
            if (Session["sid"] != null) { Session.Remove("sid"); }
            if (Request.Cookies["sid"] != null)
            {
                Response.Cookies["sid"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Remove("sid");
            }
            return UrlRedirect(PathHelper.Authen);
        }
    }
}
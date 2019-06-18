using DMN.Standard.Common.Extensions;
using Kemrex.Database;
using Kemrex.FWCommon;
using Kemrex.SoilCalculator.Web.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class AuthenController : BaseController
    {
        public UnitOfWork uow;
        public int CurrentUID => (Session["sid"].Convert2String().ParseIntNullable() ?? 0);
        public SysAccount CurrentUser => uow.Account.Get(CurrentUID);
        public DateTime CurrentDateTime = DateTime.Now;
        public AuthenController()
        {
            uow = new UnitOfWork();
        }
        // GET: Authen
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn()
        {
            string username = Request.Form["username"].ValidString();
            string password = Request.Form["password"].ValidString(false);
            bool flagRemember = Request.Form["remember"].ParseBoolean(false);
            var data = (from d in uow.db.SysAccount where d.AccountUsername == username || d.AccountEmail == username select d);
            if (data.Count() <= 0)
            {
                Session.Remove("sid");
                return RedirectToAction("Index", new { area = "", controller = "Authen", msg = "ข้อมูลเข้าสู่ระบบไม่ถูกต้อง" });
            }
            else
            {
                var ob = data.First();
                if (!Crypto.VerifyHashedPassword(ob.AccountPassword, password))
                {
                    Session.Remove("sid");
                    return RedirectToAction("Index", new { area = "", controller = "Authen", msg = "ข้อมูลเข้าสู่ระบบไม่ถูกต้อง" });
                }
                else
                {
                    Session["sid"] = ob.AccountId;
                    if(flagRemember)
                    {
                        HttpCookie ckSid = null;
                        if (Request.Cookies["sid"] == null) {
                            ckSid = new HttpCookie("sid", ob.AccountId.ToString())
                            {
                                Expires = DateTime.Now.AddMonths(1)
                            };
                        }
                        else
                        {
                            ckSid = Request.Cookies["sid"];
                            ckSid.Value = ob.AccountId.ToString();
                            ckSid.Expires = DateTime.Now.AddMonths(1);
                        }
                        Response.SetCookie(ckSid);
                    }
                    return RedirectToAction("Index", new { area = "", controller = "Home" });
                }
            }
        }

        [HttpPost]
        [BackendAuthorized]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            if (Session["sid"] != null) { Session.Remove("sid"); }
            if (Request.Cookies["sid"] != null)
            {
                Response.Cookies["sid"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Remove("sid");
            }
            return RedirectToAction("Index", new { area = "", controller = "Authen" });
        }
    }
}
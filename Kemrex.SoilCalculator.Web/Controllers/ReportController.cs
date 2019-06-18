using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Database;
using Kemrex.FWCommon;
using Kemrex.SoilCalculator.Web.ActionFilters;
using Kemrex.SoilCalculator.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class ReportController : BaseController
    {
        public UnitOfWork uow;
        public int CurrentUID => (Session["sid"].Convert2String().ParseIntNullable() ?? 0);
        public SysAccount CurrentUser => uow.Account.Get(CurrentUID);
        public DateTime CurrentDateTime = DateTime.Now;
        public ReportController()
        {
            uow = new UnitOfWork();
        }
        // GET: Report
        [BackendAuthorized]
        public ActionResult Index(int? page, int? size, string dateStart, string dateEnd, string msg, AlertMsgType? msgType)
        {
            DateTime? dtStart = dateStart.ParseDateNullable(DateFormat.ddMMyyyy);
            DateTime? dtEnd = dateEnd.ParseDateNullable(DateFormat.ddMMyyyy);
            List<TblCalLoad> lst = new List<TblCalLoad>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                var data = db.TblCalLoad.AsQueryable();
                if (dtStart.HasValue) { data = data.Where(x => DbFunctions.TruncateTime(x.CreatedDate) >= dtStart.Value); }
                if (dtEnd.HasValue) { data = data.Where(x => DbFunctions.TruncateTime(x.CreatedDate) <= dtEnd.Value); }
                int total = data.Count();
                ViewData["dateStart"] = dtStart.ParseString(DateFormat.ddMMyyyy);
                ViewData["dateEnd"] = dtEnd.ParseString(DateFormat.ddMMyyyy);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Report", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>(),
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = (from d in data
                       join m in db.TblPile on d.ModelId equals m.PileId
                       orderby
                        d.CreatedDate descending
                        , m.PileName ascending
                        , d.InputC ascending
                       select d)
                    .Skip((Pagination.Page - 1) * Pagination.Size)
                    .Take(Pagination.Size)
                    .ToList();
            }
            catch (Exception ex)
            {
                WidgetAlertModel Alert = new WidgetAlertModel()
                {
                    Type = AlertMsgType.Danger,
                    Message = ex.GetMessage()
                };
                ViewBag.Alert = Alert;
            }
            return View(lst);
        }

        [BackendAuthorized]
        public ActionResult Maps(int? page, int? size, string dateStart, string dateEnd, string msg, AlertMsgType? msgType)
        {
            DateTime? dtStart = dateStart.ParseDateNullable(DateFormat.ddMMyyyy);
            DateTime? dtEnd = dateEnd.ParseDateNullable(DateFormat.ddMMyyyy);
            ViewData["GoogleMapsAPIKey"] = ConfigurationManager.AppSettings["GoogleMapsAPI"];
            SysRole role = CurrentUser.Role(3).Role();
            ViewBag.canWrite = role.SysRolePermission.Where(x => x.MenuId == 3710 && x.PermissionId == 2 && x.PermissionFlag).Count() > 0;
            ViewBag.canDelete = role.SysRolePermission.Where(x => x.MenuId == 3710 && x.PermissionId == 3 && x.PermissionFlag).Count() > 0;
            ViewBag.canExport = role.SysRolePermission.Where(x => x.MenuId == 3710 && x.PermissionId == 4 && x.PermissionFlag).Count() > 0;
            List<TblKpt> lst = new List<TblKpt>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                var data = uow.db.TblKpt.AsQueryable();
                if (dtStart.HasValue) { data = data.Where(x => DbFunctions.TruncateTime(x.CreatedDate) >= dtStart.Value); }
                if (dtEnd.HasValue) { data = data.Where(x => DbFunctions.TruncateTime(x.CreatedDate) <= dtEnd.Value); }
                ViewData["dateStart"] = dtStart.ParseString(DateFormat.ddMMyyyy);
                ViewData["dateEnd"] = dtEnd.ParseString(DateFormat.ddMMyyyy);
                int total = data.Count();
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Maps", "Report", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>(),
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = data
                    .OrderByDescending(x => x.KptDate)
                    .ThenBy(x => x.ProjectName)
                    .Include(x => x.TblKptDetail)
                    .Skip((Pagination.Page - 1) * Pagination.Size)
                    .Take(Pagination.Size)
                    .ToList();
            }
            catch (Exception ex)
            {
                WidgetAlertModel Alert = new WidgetAlertModel()
                {
                    Type = AlertMsgType.Danger,
                    Message = ex.GetMessage()
                };
                ViewBag.Alert = Alert;
            }
            return View(lst);
        }
    }
}
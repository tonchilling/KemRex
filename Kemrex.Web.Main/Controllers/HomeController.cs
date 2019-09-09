using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common.Extensions.Database;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using Kemrex.Web.Main.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class HomeController : KemrexController
    {
        [Authorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            int year, int month, string src = "")
        {
            List<CalendarDayModel> data = new List<CalendarDayModel>();
            List<TblSaleOrder> lst = new List<TblSaleOrder>();
            List<TblJobOrder> joblst = new List<TblJobOrder>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                AccountPermission permission = GetPermission(CurrentUID);
                ViewData["optPermission"] = permission;
                DateTime firstDt = new DateTime(year, month, 1);
                ViewBag.FocusDate = firstDt;
                if (permission.TeamType == TeamType.Admin || permission.TeamType == TeamType.Sale)   //administrator of sale
                {
                    int total = uow.Modules.SaleOrder.Count(month, src);
                    lst = uow.Modules.SaleOrder.GetAll(1, -1, month, src, (permission.IsAdminTeam || permission.IsManager) ? 0 : CurrentUID);
                    #region Calculate first week
                    for (int i = firstDt.DayOfWeek.ToInt(); i > 0; i--)
                    {
                        CalendarDayModel day = new CalendarDayModel()
                        {
                            Date = firstDt.AddDays(-i),
                            Jobs = new List<TblSaleOrder>(),
                            TeamType = permission.TeamType
                        };
                        data.Add(day);
                    }
                    #endregion
                    int offset = data.Count;
                    for (int i = offset; i <= 42; i++)
                    {
                        CalendarDayModel day = new CalendarDayModel()
                        {
                            Date = firstDt.AddDays(i - offset),
                            Jobs = new List<TblSaleOrder>(),
                            TeamType = permission.TeamType
                        };
                        if (day.Date.Month == month)
                        {
                            var dayJobs = lst.Where(x =>
                                x.SaleOrderDate.HasValue
                                && x.SaleOrderDate.Value.Date == day.Date.Date);
                            day.Jobs = dayJobs.Count() > 0 ? dayJobs.ToList() : new List<TblSaleOrder>();
                        }
                        data.Add(day);
                    }
                }
                else if (permission.TeamType == TeamType.Operation)  ///operation
                {
                    joblst = uow.Modules.JobOrder.GetAll(1, -1, month, src, (permission.IsAdminTeam) ? 0 : permission.TeamId);
                    #region Calculate first week
                    for (int i = firstDt.DayOfWeek.ToInt(); i > 0; i--)
                    {
                        CalendarDayModel day = new CalendarDayModel()
                        {
                            Date = firstDt.AddDays(-i),
                            JobsOrder = new List<TblJobOrder>(),
                            TeamType = permission.TeamType
                        };
                        data.Add(day);
                    }
                    #endregion
                    int offset = data.Count;
                    for (int i = offset; i <= 42; i++)
                    {
                        CalendarDayModel day = new CalendarDayModel()
                        {
                            Date = firstDt.AddDays(i - offset),
                            JobsOrder = new List<TblJobOrder>(),
                            TeamType = permission.TeamType
                        };
                        if (day.Date.Month == month)
                        {
                            var dayJobs = joblst.Where(x =>
                                x.StartDate.HasValue
                                && x.StartDate.Value.Date == day.Date.Date
                                );
                            day.JobsOrder = dayJobs.Count() > 0 ? dayJobs.ToList() : new List<TblJobOrder>();
                        }
                        data.Add(day);
                    }
                }
                else
                {
                    #region Calculate first week
                    for (int i = firstDt.DayOfWeek.ToInt(); i > 0; i--)
                    {
                        CalendarDayModel day = new CalendarDayModel()
                        {
                            Date = firstDt.AddDays(-i),
                            JobsOrder = new List<TblJobOrder>(),
                            TeamType = permission.TeamType
                        };
                        data.Add(day);
                    }
                    #endregion
                    int offset = data.Count;
                    for (int i = offset; i <= 42; i++)
                    {
                        CalendarDayModel day = new CalendarDayModel()
                        {
                            Date = firstDt.AddDays(i - offset)
                        };
                        data.Add(day);
                    }
                }

                
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
            return View(data);
        }
    }
}
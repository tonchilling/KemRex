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
    public class CalendarController : KemrexController
    {
        [Authorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            int year, int month, string src = "")
        {
            List<CalendarDayModel> data = new List<CalendarDayModel>();
            List<TblSaleOrder> lstAll = new List<TblSaleOrder>();
            List<TblSaleOrder> lst = new List<TblSaleOrder>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                DateTime firstDt = new DateTime(year, month, 1);
                ViewBag.FocusDate = firstDt;
                int total = uow.Modules.SaleOrder.Count(month, src);

                //lst = uow.Modules.SaleOrder.Gets(1, -1, month, src);
                lstAll = uow.Modules.SaleOrder.Gets(1, -1, month, src);
                if (CurrentUID == 1) //admin
                {
                    lst = lstAll;
                }
                else
                {
                    Team userTeam = uow.Modules.TeamSale.CheckTeamSale(CurrentUID);
                    Team jobTeam = new Team();
                    if (userTeam != null)
                    {
                        foreach (var x in lstAll)
                        {
                            jobTeam = uow.Modules.TeamSale.CheckTeamSale(x.CreatedBy.HasValue?x.CreatedBy.Value:0);
                            if (jobTeam == null) continue;
                            if (userTeam.TeamId == jobTeam.TeamId)
                            {
                                if(CurrentUID == jobTeam.AccountId || CurrentUID == jobTeam.ManagerId)
                                lst.Add(x);

                            }
                        }
                    }
                    else
                    {
                        lst = null;
                    }

                    /* Team checkteam = uow.Modules.TeamOperation.CheckTeamOperation(CurrentUID);
                    if (checkteam != null)
                    {
                        if (checkteam.TeamId == 1)
                        {
                            lst = lstAll;
                        }
                        else if (checkteam.TeamId > 1)
                        {
                            lst = (from a in lstAll
                                   where a.TeamId == checkteam.TeamId
                                   select a).ToList();
                        }
                    }
                    else
                    {
                        lst = null;
                    }*/
                }
                #region Calculate first week
                for (int i = firstDt.DayOfWeek.ToInt(); i > 0; i--)
                {
                    CalendarDayModel day = new CalendarDayModel()
                    {
                        Date = firstDt.AddDays(-i),
                        Jobs = new List<TblSaleOrder>()
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
                        Jobs = new List<TblSaleOrder>()
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
 
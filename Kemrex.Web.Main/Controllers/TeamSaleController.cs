using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common.Constraints;
using Kemrex.Core.Common.Extensions.Database;
using Kemrex.Core.Common.Helper;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class TeamSaleController : KemrexController
    {
        [Authorized]
        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TeamSale ob = uow.Modules.TeamSale.Get(id ?? 0);
            return ViewDetail(ob, msg, msgType);
        }
        [Authorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType)
        {
            List<TeamSale> lst = new List<TeamSale>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.TeamSale.Count();
                WidgetPaginationModel Pagination = new WidgetPaginationModel(PathHelper.SaleTeam)
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = uow.Modules.TeamSale.Gets(Pagination.Page, Pagination.Size);
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

        [Authorized]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName(KemrexPath.ACTION_DETAIL)]
        public ActionResult SetDetail()
        {
            Dictionary<string, dynamic> rs = new Dictionary<string, dynamic>()
            {
                { "msg", "การทำงานไม่ถูกต้อง" },
                { "msgType", AlertMsgType.Danger }
            };
            int teamId = Request.Form["teamId"].ParseInt();
            TeamSale ob = uow.Modules.TeamSale.Get(teamId);
            bool isInsert = ob.TeamId <= 0;
            if (ob.TeamId <= 0)
            {
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            ob.TeamName = Request.Form["teamName"];
            ob.ManagerId = Request.Form["managerId"].ParseInt();
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                if (!ob.IsValid(out string errMsg))
                { throw new Exception(errMsg); }
                
                uow.Modules.TeamSale.Set(ob);
                uow.SaveChanges();

                rs["msg"] = "บันทึกข้อมูลเรียบร้อยแล้ว";
                rs["msgType"] = AlertMsgType.Success;

                if (isInsert) { rs.Add("id", ob.TeamId); }
                return isInsert ? UrlRedirect(PathHelper.SaleTeamDetail, rs) : UrlRedirect(PathHelper.SaleTeam, rs);
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage();
                return ViewDetail(ob, msg, AlertMsgType.Danger);
            }
        }

        [Authorized]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName(KemrexPath.ACTION_DETAIL + KemrexPath.ACTION_SET)]
        public ActionResult SetDetailData()
        {
            Dictionary<string, dynamic> rs = new Dictionary<string, dynamic>()
            {
                { "msg", "การทำงานไม่ถูกต้อง" },
                { "msgType", AlertMsgType.Danger }
            };
            int id = Request.Form["id"].ParseInt();
            TeamSaleDetail ob = uow.Modules.TeamSaleDetail.Get(id);
            if (ob.Id <= 0)
            {
                ob.TeamId = Request.Form["teamId"].ParseInt();
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            ob.TeamRemark = Request.Form["teamRemark"];
            ob.AccountId = Request.Form["accountId"].ParseInt();
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                if (!ob.IsValid(out string errMsg))
                { throw new Exception(errMsg); }

                uow.Modules.TeamSaleDetail.Set(ob);
                uow.SaveChanges();

                rs["msg"] = "บันทึกข้อมูลเรียบร้อยแล้ว";
                rs["msgType"] = AlertMsgType.Success;
            }
            catch (Exception ex)
            {
                rs["msg"] = ex.GetMessage();
                rs["msgType"] = AlertMsgType.Danger;
            }
            finally
            {
                rs.Add("id", ob.TeamId);
            }
            return UrlRedirect(PathHelper.SaleTeamDetail, rs);
        }

        [HttpPost]
        [Authorized]
        public ActionResult Delete()
        {
            Dictionary<string, dynamic> rs = new Dictionary<string, dynamic>()
            {
                { "msg", "การทำงานไม่ถูกต้อง" },
                { "msgType", AlertMsgType.Danger }
            };
            try
            {
                int id = Request.Form["id"].ParseInt();
                TeamSale ob = uow.Modules.TeamSale.Get(id);
                if (ob == null)
                {
                    rs["msg"] = "ไม่พบข้อมูลที่ต้องการ";
                    rs["msgType"] = AlertMsgType.Warning;
                    return UrlRedirect(PathHelper.SaleTeam, rs);
                }

                uow.Modules.TeamSale.Delete(ob);
                rs["msg"] = "ลบข้อมูลเรียบร้อยแล้ว";
                rs["msgType"] = AlertMsgType.Success;
                return UrlRedirect(PathHelper.SaleTeam, rs);
            }
            catch (Exception ex)
            {
                rs["msg"] = ex.GetMessage();
                rs["msgType"] = AlertMsgType.Danger;
                return UrlRedirect(PathHelper.SaleTeam, rs);
            }
        }
        [HttpPost]
        [Authorized]
        public ActionResult DeleteDetail()
        {
            Dictionary<string, dynamic> rs = new Dictionary<string, dynamic>()
            {
                { "msg", "การทำงานไม่ถูกต้อง" },
                { "msgType", AlertMsgType.Danger }
            };
            try
            {
                int id = Request.Form["id"].ParseInt();
                int TeamId = Request.Form["TeamId"].ParseInt();
                TeamSaleDetail ob = uow.Modules.TeamSaleDetail.Get(id);
                if (ob == null)
                { return RedirectToAction("Detail", "TeamSale", new { id = TeamId, msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.TeamSaleDetail.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", "TeamSale", new { id = TeamId, msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            {
                int TeamId = Request.Form["TeamId"].ParseInt();
                return RedirectToAction("Detail", "TeamSale", new { id = TeamId, msg = ex.GetMessage(), msgType = AlertMsgType.Danger });
            }
        }
        #region Private Action
        private ActionResult ViewDetail(TeamSale ob, string msg, AlertMsgType? msgType)
        {
            try
            {
                if (ob == null)
                { throw new Exception("ไม่พบข้อมูลที่ต้องการ, กรุณาลองใหม่อีกครั้ง"); }

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                ViewData["optSale"] = uow.Modules.TeamSale.GetNotMembers();
                return View("Detail", ob);
            }
            catch (Exception ex)
            {
                Dictionary<string, dynamic> rs = new Dictionary<string, dynamic>()
                {
                    { "msg", ex.GetMessage() },
                    { "msgType", AlertMsgType.Danger }
                };
                return UrlRedirect(PathHelper.SaleTeam, rs);
            }
        }
        #endregion
    }
}
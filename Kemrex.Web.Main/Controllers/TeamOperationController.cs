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
    public class TeamOperationController : KemrexController
    {
        [Authorized]
        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TeamOperation ob = uow.Modules.TeamOperation.Get(id ?? 0);
            return ViewDetail(ob, msg, msgType);
        }
        [Authorized]
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType)
        {
            List<TeamOperation> lst = new List<TeamOperation>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.TeamOperation.Count();
                WidgetPaginationModel Pagination = new WidgetPaginationModel(PathHelper.OperationTeam)
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = uow.Modules.TeamOperation.Gets(Pagination.Page, Pagination.Size);
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
            TeamOperation ob = uow.Modules.TeamOperation.Get(teamId);
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

                uow.Modules.TeamOperation.Set(ob);
                uow.SaveChanges();

                rs["msg"] = "บันทึกข้อมูลเรียบร้อยแล้ว";
                rs["msgType"] = AlertMsgType.Success;

                if (isInsert) { rs.Add("id", ob.TeamId); }
                return isInsert ? UrlRedirect(PathHelper.OperationTeamDetail, rs) : UrlRedirect(PathHelper.OperationTeam, rs);
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
            TeamOperationDetail ob = uow.Modules.TeamOperationDetail.Get(id);
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

                uow.Modules.TeamOperationDetail.Set(ob);
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
            return UrlRedirect(PathHelper.OperationTeamDetail, rs);
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
                int id = Request.Form["TeamId"].ParseInt();
                TeamOperation ob = uow.Modules.TeamOperation.Get(id);
                if (ob == null)
                {
                    rs["msg"] = "ไม่พบข้อมูลที่ต้องการ";
                    rs["msgType"] = AlertMsgType.Warning;
                    return UrlRedirect(PathHelper.OperationTeam, rs);
                }

                uow.Modules.TeamOperation.Delete(ob);
                uow.SaveChanges();
                rs["msg"] = "ลบข้อมูลเรียบร้อยแล้ว";
                rs["msgType"] = AlertMsgType.Success;
                return UrlRedirect(PathHelper.OperationTeam, rs);
            }
            catch (Exception ex)
            {
                rs["msg"] = ex.GetMessage();
                rs["msgType"] = AlertMsgType.Danger;
                return UrlRedirect(PathHelper.OperationTeam, rs);
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
                TeamOperationDetail ob = uow.Modules.TeamOperationDetail.Get(id);
                if (ob == null)
                { return RedirectToAction("Detail", "TeamOperation", new { id = TeamId, msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.TeamOperationDetail.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", "TeamOperation", new { id= TeamId, msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            {
                int TeamId = Request.Form["TeamId"].ParseInt();
                return RedirectToAction("Detail", "TeamOperation", new { id = TeamId, msg = ex.GetMessage(), msgType = AlertMsgType.Danger });
            }
        }
        #region Private Action

        [HttpGet]
      
        public JsonResult GetAllTeamNotIn(string StartDate,string EndDate)
        {
            DateTime stDate, enDate;

            stDate = Converting.StringToDate(StartDate,"dd/MM/yyyy");
            enDate = Converting.StringToDate(EndDate, "dd/MM/yyyy");
            List<int> teamId= uow.Modules.JobOrder.GetTeamByPeriod(stDate, enDate);
            List<TeamOperation> teamOperations = uow.Modules.TeamOperation.GetTeamNotIn(teamId);

         
            // ob.TblSaleOrderDetail = uow.Modules.SaleOrderDetail.Gets(id ?? 0);
            // TblCustomer objCustomer = uow.Modules.Customer.Get(Convert.ToInt32(ob.CustomerId));



            return Json(teamOperations, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public JsonResult GetAllTeam()
        {
            DateTime stDate, enDate;



            List<TeamOperation> teamOperations = uow.Modules.TeamOperation.GetAll();


            // ob.TblSaleOrderDetail = uow.Modules.SaleOrderDetail.Gets(id ?? 0);
            // TblCustomer objCustomer = uow.Modules.Customer.Get(Convert.ToInt32(ob.CustomerId));



            return Json(teamOperations, JsonRequestBehavior.AllowGet);
        }




        private ActionResult ViewDetail(TeamOperation ob, string msg, AlertMsgType? msgType)
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
                ViewData["optOperation"] = uow.Modules.TeamOperation.GetNotMembers();
                return View("Detail", ob);
            }
            catch (Exception ex)
            {
                Dictionary<string, dynamic> rs = new Dictionary<string, dynamic>()
                {
                    { "msg", ex.GetMessage() },
                    { "msgType", AlertMsgType.Danger }
                };
                return UrlRedirect(PathHelper.OperationTeam, rs);
            }
        }
        #endregion
    }
}
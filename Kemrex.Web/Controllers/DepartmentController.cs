﻿using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Models;
using Kemrex.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Controllers
{
    public class DepartmentController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            string src = "")
        {
            List<TblDepartment> lst = new List<TblDepartment>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Department.Counts(src);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Account", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>()
                    {
                        { "src", src }
                    },
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = uow.Modules.Department.Gets(Pagination.Page, Pagination.Size, src);
            }
            catch (Exception ex)
            {
                WidgetAlertModel Alert = new WidgetAlertModel()
                {
                    Type = AlertMsgType.Danger,
                    Message = ex.GetMessage(true)
                };
                ViewBag.Alert = Alert;
            }
            return View(lst);
        }

        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TblDepartment ob = uow.Modules.Department.Get(id ?? 0);
            return ViewDetail(ob, msg, msgType);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int id = Request.Form["DepartmentId"].ParseInt();
            TblDepartment ob = uow.Modules.Department.Get(id);
            if (ob.DepartmentId <= 0)
            {
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            ob.DepartmentName = Request.Form["DepartmentName"];
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                //Validate model b4 save

                uow.Modules.Department.Set(ob);
                uow.SaveChanges();

                return RedirectToAction("Index", new
                {
                    area = "",
                    controller = MVCController,
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว",
                    msgType = AlertMsgType.Success
                });
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(ob, msg, AlertMsgType.Danger);
            }
        }

        [HttpPost]
        [Authorized]
        public ActionResult Delete(int id)
        {
            try
            {
                TblDepartment ob = uow.Modules.Department.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Department.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        #region Private Action
        private ActionResult ViewDetail(TblDepartment ob, string msg, AlertMsgType? msgType)
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
                //ViewData["optPrefix"] = uow.Role.Gets(1, 0, 2);
                //ViewData["optRoleCal"] = uow.Role.Gets(1, 0, 2);
                return View(ob);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", MVCController, new
                {
                    area = MVCArea,
                    msg = ex.GetMessage(),
                    msgType = AlertMsgType.Danger
                });
            }
        }
        #endregion
    }
}
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
    public class EmployeeController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            string src = "", string phone = "", string email = "")
        {
            List<TblEmployee> lst = new List<TblEmployee>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Employee.Counts(src, phone, email);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Account", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>()
                    {
                        { "src", src },
                        { "phone", phone },
                        { "email", email }
                    },
                    SortExp = "",
                    Total = total
                };
                ViewBag.Pagination = Pagination;
                lst = uow.Modules.Employee.Gets(Pagination.Page, Pagination.Size, src, phone, email);
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

        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TblEmployee ob = uow.Modules.Employee.Get(id ?? 0);
            return ViewDetail(ob, msg, msgType);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int id = Request.Form["EmpId"].ParseInt();
            TblEmployee ob = uow.Modules.Employee.Get(id);
            if (ob.EmpId <= 0)
            {
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            ob.EmpCode = Request.Form["EmpCode"];
            
            if (Request.Form["AccountId"].ParseInt() <= 0) {
                ob.AccountId = null;
                ob.Account = null;
            }
            else {
                ob.AccountId = Request.Form["AccountId"].ParseInt();
                ob.Account = uow.Modules.Account.Get(ob.AccountId.Value);
            }
            if (Request.Form["PrefixId"].ParseInt() <= 0) {
                ob.PrefixId = null;
                ob.Prefix = null;
            }
            else {
                ob.PrefixId = Request.Form["PrefixId"].ParseInt();
                ob.Prefix = uow.Modules.Enum.PrefixGet(ob.PrefixId.Value);
            }
            ob.EmpNameTh = Request.Form["EmpNameTh"];
            ob.EmpNameEn = Request.Form["EmpNameEn"];
            if (Request.Form["DepartmentId"].ParseInt() <= 0) {
                ob.DepartmentId = null;
                ob.Department = null;
                ob.PositionId = null;
                ob.Position = null;
            }
            else {
                ob.DepartmentId = Request.Form["DepartmentId"].ParseInt();
                ob.Department = uow.Modules.Department.Get(ob.DepartmentId.Value);
                if (Request.Form["PositionId"].ParseInt() <= 0)
                {
                    ob.PositionId = null;
                    ob.Position = null;
                }
                else {
                    ob.PositionId = Request.Form["PositionId"].ParseInt();
                    ob.Position = uow.Modules.Position.Get(ob.PositionId.Value);
                }
            }
            ob.StatusId = Request.Form["StatusId"].ParseInt();
            if (Request.Form["EmpTypeId"].ParseInt() <= 0) { ob.EmpTypeId = null; }
            else { ob.EmpTypeId = Request.Form["EmpTypeId"].ParseInt(); }
            if (Request.Form["LeadId"].ParseInt() <= 0) { ob.LeadId = null; }
            else { ob.LeadId = Request.Form["LeadId"].ParseInt(); }
            ob.EmpPid = Request.Form["EmpPid"];
            ob.EmpAddress = Request.Form["EmpAddress"];
            ob.EmpPostcode = Request.Form["EmpPostcode"];
            ob.EmpMobile = Request.Form["EmpMobile"];
            ob.EmpEmail = Request.Form["EmpEmail"];
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                //Validate model b4 save

                uow.Modules.Employee.Set(ob);
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
                TblEmployee ob = uow.Modules.Employee.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Employee.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        #region Private Action
        private ActionResult ViewDetail(TblEmployee ob, string msg, AlertMsgType? msgType)
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
                ViewData["optPrefix"] = uow.Modules.Enum.PrefixGets();
                ViewData["optDepartment"] = uow.Modules.Department.Gets();
                ViewData["optPosition"] = uow.Modules.Position.Gets(1, 0);
                ViewData["optLead"] = uow.Modules.Employee.Gets();
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
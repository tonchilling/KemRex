using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common.Extensions.Database;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using IOFile = System.IO.File;

namespace Kemrex.Web.Main.Controllers
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
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Employee", "")
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

        [HttpGet]
        public JsonResult GetEmployee(string fieldSearch)
        {

            List<TblEmployee> EmployeeList = uow.Modules.Employee.GetByEmployeeCode(fieldSearch);
            // ob.TblSaleOrderDetail = uow.Modules.SaleOrderDetail.Gets(id ?? 0);
            // TblCustomer objCustomer = uow.Modules.Customer.Get(Convert.ToInt32(ob.CustomerId));



            return Json(EmployeeList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetEmployeeByCode(string GetByEmpCode)
        {
            TblEmployee Employee = uow.Modules.Employee.GetEmployeeByEmpCode(GetByEmpCode);

            return Json(Employee, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetEmployeeList()
        {

            List<TblEmployee> EmployeeList = uow.Modules.Employee.GetList();
            // ob.TblSaleOrderDetail = uow.Modules.SaleOrderDetail.Gets(id ?? 0);
            // TblCustomer objCustomer = uow.Modules.Customer.Get(Convert.ToInt32(ob.CustomerId));



            return Json(EmployeeList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost, ActionName("GetAccountList")]
        public JsonResult GetAccountList()
        {
           // List <SysAccount>
            List<SysAccount> AccountList = uow.Modules.Account.GetList();
            // ob.TblSaleOrderDetail = uow.Modules.SaleOrderDetail.Gets(id ?? 0);
            // TblCustomer objCustomer = uow.Modules.Customer.Get(Convert.ToInt32(ob.CustomerId));



            return Json(null, JsonRequestBehavior.AllowGet);
        }



        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            List<TblEmployeeUserPermission> perList = new List<TblEmployeeUserPermission>();
            int id = Request.Form["EmpId"].ParseInt();
            TblEmployee ob = uow.Modules.Employee.Get(id);
            if (ob.EmpId <= 0)
            {
                ob.EmpCode = GetNewId();
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }

            if (Request.Form["ddlPerQuotation"]!=null && Request.Form["ddlPerQuotation"]!="" && ob.AccountId>0)
            {

                foreach (string viewEmpId in Request.Form["ddlPerQuotation"].ToString().Split(','))
                {
                    perList.Add(new TblEmployeeUserPermission() {
                        AccountId = (int)ob.AccountId.Value,
                        FunId = 1,
                        ViewAccountId = viewEmpId.ParseInt(),
                        CreatedBy = CurrentUID,
                    CreatedDate = CurrentDate
                });
                }
            }

            if (Request.Form["ddlPerSaleOrder"] !=null && Request.Form["ddlPerSaleOrder"] != "" && ob.AccountId > 0)
            {

                foreach (string viewEmpId in Request.Form["ddlPerSaleOrder"].ToString().Split(','))
                {
                    perList.Add(new TblEmployeeUserPermission()
                    {
                        AccountId = (int)ob.AccountId.Value,
                        FunId = 2,
                        ViewAccountId = viewEmpId.ParseInt(),
                        CreatedBy = CurrentUID,
                        CreatedDate = CurrentDate
                    });
                }
            }

            if (Request.Form["ddlPerInvoice"]!=null && Request.Form["ddlPerInvoice"] != "" && ob.AccountId > 0)
            {

                foreach (string viewEmpId in Request.Form["ddlPerInvoice"].ToString().Split(','))
                {
                    perList.Add(new TblEmployeeUserPermission()
                    {
                        AccountId = (int)ob.AccountId.Value,
                        FunId = 3,
                        ViewAccountId = viewEmpId.ParseInt(),
                        CreatedBy = CurrentUID,
                        CreatedDate = CurrentDate
                    });
                }
            }

            if (Request.Form["ddlPerJobOrder"]!=null && Request.Form["ddlPerJobOrder"] != "" && ob.AccountId > 0)
            {

                foreach (string viewEmpId in Request.Form["ddlPerJobOrder"].ToString().Split(','))
                {
                    perList.Add(new TblEmployeeUserPermission()
                    {
                        AccountId = (int)ob.AccountId.Value,
                        FunId = 4,
                        ViewAccountId = viewEmpId.ParseInt(),
                        CreatedBy = CurrentUID,
                        CreatedDate = CurrentDate
                    });
                }
            }



            if (Request.Form["AccountId"].ParseInt() <= 0)
            {
                ob.AccountId = null;
                ob.Account = null;
            }
            else
            {
                ob.AccountId = Request.Form["AccountId"].ParseInt();
                ob.Account = uow.Modules.Account.Get(ob.AccountId.Value);
            }
            if (Request.Form["PrefixId"].ParseInt() <= 0)
            {
                ob.PrefixId = null;
                ob.Prefix = null;
            }
            else
            {
                ob.PrefixId = Request.Form["PrefixId"].ParseInt();
                ob.Prefix = uow.Modules.Enum.PrefixGet(ob.PrefixId.Value);
            }
            ob.EmpNameTh = Request.Form["EmpNameTh"];
            ob.EmpNameEn = Request.Form["EmpNameEn"];
            if (Request.Form["DepartmentId"].ParseInt() <= 0)
            {
                ob.DepartmentId = null;
                ob.Department = null;
                ob.PositionId = null;
                ob.Position = null;
            }
            else
            {
                ob.DepartmentId = Request.Form["DepartmentId"].ParseInt();
                ob.Department = uow.Modules.Department.Get(ob.DepartmentId.Value);
                if (Request.Form["PositionId"].ParseInt() <= 0)
                {
                    ob.PositionId = null;
                    ob.Position = null;
                }
                else
                {
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
            ob.IsQuotationApprove = Request.Form["hddIsQuotationApprove"] == "on" ? 1 : 0;
            ob.IsJobOrderApprove = Request.Form["hddIsJobOrderApprove"] == "on" ? 1 : 0;
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                //Validate model b4 save
                bool clearOld = false;
                string oldSignature = ob.EmpSignature;
                if (Request.Files.Count > 0 && Request.Files["EmpSignature"] != null && Request.Files["EmpSignature"].ContentLength > 0)
                {
                    HttpPostedFileBase uploadedFile = Request.Files["EmpSignature"];
                    string FilePath = string.Format("files/signature/{0}{1}", CurrentDate.ParseString(DateFormat._yyyyMMddHHmmssfff), Path.GetExtension(uploadedFile.FileName));
                    if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                    if (!Directory.Exists(Server.MapPath("~/files/signature"))) { Directory.CreateDirectory(Server.MapPath("~/files/signature")); }
                    uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));

                    ob.EmpSignature = FilePath;
                    clearOld = true;
                }
                if (clearOld && !string.IsNullOrWhiteSpace(oldSignature) && IOFile.Exists(Server.MapPath("~/" + oldSignature)))
                { IOFile.Delete(Server.MapPath("~/" + oldSignature)); }

                uow.Modules.Employee.Set(ob);
                uow.SaveChanges();

                
                    uow.Modules.EmployeeUserPermission.Set(perList.FindAll(o=>o.FunId==1));
                uow.Modules.EmployeeUserPermission.Set(perList.FindAll(o => o.FunId == 2));
                uow.Modules.EmployeeUserPermission.Set(perList.FindAll(o => o.FunId == 3));
                uow.Modules.EmployeeUserPermission.Set(perList.FindAll(o => o.FunId == 4));

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

        private string GetNewId()
        {
            string qid = "E";
            string Id = uow.Modules.Employee.GetLastEmpCode();

            if (Id == null)
            {
                Id = string.Format("{0}-000000", qid);
            }

          
            int runid = int.Parse(Id.Replace(qid, ""));

            runid++;

            qid = qid + runid.ToString("D6");
            return qid;
        }




        [HttpPost]
       
        public ActionResult Delete(int EmpId)
        {
            try
            {
                TblEmployee ob = uow.Modules.Employee.Get(EmpId);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Employee.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        [HttpPost]

        public ActionResult DeleteNew()
        {
            try
            {
                int EmpId = Request.Form["EmpId"].ParseInt();
                TblEmployee ob = uow.Modules.Employee.Get(EmpId);
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
                ViewData["optAccount"] = uow.Modules.Account.Gets();

                ViewData["optEmpPermission"] = uow.Modules.EmployeeUserPermission.Gets(src:ob.AccountId.ToString());
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
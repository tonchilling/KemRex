using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using DMN.Standard.Common.Constraints;
using Kemrex.Core.Common.Helper;
using IOFile = System.IO.File;
using System.Web;
using System.IO;
using System.Globalization;

namespace Kemrex.Web.Main.Controllers
{
    public class PaymentController : KemrexController
    {
        public string dateText = DateTime.Now.ParseString(DateFormat.ddMMyyyy);
        public DateTime txt2Date = "2019-02-01 00:15:00".ParseDate(DateFormat.yyyyMMddHHmmss);
        System.Globalization.CultureInfo _cultureEngInfo = new System.Globalization.CultureInfo("en-US");
        // GET: Payment
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
                    string src = "")
        {
            List<TblPayment> lst = new List<TblPayment>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Payment.Count(src);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Payment", "")
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
                lst = uow.Modules.Payment.Gets(Pagination.Page, Pagination.Size, src);
                foreach (var ls in lst.ToList())
                {
                    ls.AcctReceive = uow.Modules.BankAccount.Get(ls.AcctReceiveId.HasValue ? ls.AcctReceiveId.Value : 0);
                }
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


        private string getId(string type)
        {
            string qid = type;
            
            var dt = DateTime.Now.ToString("yyMMdd", _cultureEngInfo);
            string Id = uow.Modules.Payment.GetLastId(qid + dt);

            if (Id == null)
            {
                Id = string.Format("{0}{1}-0000", qid, dt);
            }

            string preid = Id.Split('-')[0];
            int runid = int.Parse(Id.Split('-')[1]);

            if (preid.Contains(dt))
            {
                runid++;
            }
            else
            {
                runid = 1;
            }
            qid += dt + "-" + runid.ToString("D3");
            return qid;
        }


        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail(TblPayment obj)
        {


            TblPayment old = uow.Modules.Payment.Get(obj.PaymentId);

            //string PaymentId = this.Request.Form["PaymentId"];
            if (obj.PaymentId < 1)
            {
                obj.PaymentNo = getId("P");
                obj.CreatedDate = CurrentDate;
                obj.UpdatedDate = CurrentDate;
                obj.CreatedBy = CurrentUID;
                obj.UpdatedBy = CurrentUID;
                obj.StatusId = 1;
                

            }
            else
            {
                obj.UpdatedDate = CurrentDate;
                obj.UpdatedBy = CurrentUID;
                string StatusId = this.Request.Form["StatusId"];
                string hdApprove = this.Request.Form["hdApprove"];
                if(hdApprove == "3") obj.StatusId = int.Parse(hdApprove);
                else obj.StatusId = int.Parse(StatusId);
            }
            if (this.Request.Form["InvoiceId"].ToString() != "")
            {
                string invoiceid = this.Request.Form["InvoiceId"];
                obj.InvoiceId = int.Parse(invoiceid);
                obj.InvoiceNo = this.Request.Form["InvoiceNo"];
            }
            
            string CustomerName = this.Request.Form["CustomerName"];
            obj.CustomerName = CustomerName;
            if (Request.Form["PaymentDate"].ToString().Count() > 0)
            {
                var dd = Request.Form["PaymentDate"].Split(' ')[0];
                DateTime dateEng = Convert.ToDateTime(dd, _cultureEngInfo);
                obj.PaymentDate = dateEng;
                obj.StrPaymentDate = dd;
            }
            var AcctReceiveId = Request.Form["radio_bankaccount"];
            if (AcctReceiveId != null)
            {
                obj.AcctReceiveId = int.Parse(AcctReceiveId);
            }
            
            obj.BankPayFrom = this.Request.Form["BankPayFrom"];
            obj.BankPayFromBranch = this.Request.Form["BankPayFromBranch"];
            bool clearOld = false;
            string oldPaySlipPath = old.PaySlipPath;
            obj.StrPaySlipPath = oldPaySlipPath;
            obj.PaySlipPath = oldPaySlipPath;
            if (Request.Files.Count > 0 && Request.Files["PaySlipPath"] != null && Request.Files["PaySlipPath"].ContentLength > 0)
            {
                HttpPostedFileBase uploadedFile = Request.Files["PaySlipPath"];
                string FilePath = string.Format("files/payment/{0}{1}", "P" + CurrentDate.ParseString(DateFormat._yyyyMMddHHmmssfff), Path.GetExtension(uploadedFile.FileName));
                if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                if (!Directory.Exists(Server.MapPath("~/files/payment"))) { Directory.CreateDirectory(Server.MapPath("~/files/payment")); }
                uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));

                obj.PaySlipPath = FilePath;
                clearOld = true;
            }

            if (clearOld && !string.IsNullOrWhiteSpace(oldPaySlipPath) && IOFile.Exists(Server.MapPath("~/" + oldPaySlipPath)))
            { IOFile.Delete(Server.MapPath("~/" + oldPaySlipPath)); }


            try
            {
                int invoicestatus = uow.Modules.Invoice.GetStatus(obj.InvoiceId.HasValue?obj.InvoiceId.Value:0);
                if (obj.StatusId == 3)
                {
                    if (invoicestatus == 3)
                    {
                        return RedirectToAction("Detail", MVCController, new { id = obj.PaymentId, msg = "มีการชำระเงินไปแล้ว กรุณาเลือกใบแจ้งหนี้อื่น", msgType = AlertMsgType.Danger });
                    }
                }
                uow.Modules.Payment.SetPayment(obj);
                //uow.Modules.Payment.Set(obj);
                //uow.SaveChanges();
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(obj, msg, AlertMsgType.Danger);
            }

            return RedirectToAction("Detail", MVCController, new { id = obj.PaymentId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });


            //  return RedirectToAction("Detail", MVCController, new { id = 1, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TblPayment ob = uow.Modules.Payment.Get(id ?? 0);
            ob.StrCreatedDate = ob.CreatedDate.Day.ToString("00") + "/" + ob.CreatedDate.Month.ToString("00") + "/" + ob.CreatedDate.Year;
            ob.StrUpdatedDate = ob.UpdatedDate.Day.ToString("00") + "/" + ob.UpdatedDate.Month.ToString("00") + "/" + ob.UpdatedDate.Year;
            ob.StrPaymentDate = ob.PaymentDate.HasValue ? ob.PaymentDate.Value.Day.ToString("00") + "/" + ob.PaymentDate.Value.Month.ToString("00") + "/" + ob.PaymentDate.Value.Year : "";
            ob.StrPaySlipPath = ob.PaySlipPath;
            ob.Invoice = uow.Modules.Invoice.Get(ob.InvoiceId.HasValue?ob.InvoiceId.Value:0);
            return ViewDetail(ob, msg, msgType);
        }

        private ActionResult ViewDetail(TblPayment ob, string msg, AlertMsgType? msgType)
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
                AccountPermission permission = new AccountPermission();
                permission = GetPermissionSale(CurrentUser.AccountId, ob.CreatedBy);

                ViewData["optBankAccount"] = uow.Modules.BankAccount.GetList(1);
                CurrentUser.TblEmployee = uow.Modules.Employee.GetEmployeeByAccount(CurrentUID);
                ViewData["userAccount"] = CurrentUser;
                ViewData["optPermission"] = permission;
                ViewData["optEmployee"] = uow.Modules.Employee.Gets();
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

        public ActionResult AddProduct()
        {
            int qid = Request.Form["TransferStockId"].ParseInt();
            var id = Request.Form["selProduct"].Split(':');  //  ProductId:PriceNet
            int qty = Request.Form["RequestQty"].ParseInt();
            int WHId = Request.Form["selWareHose"].ParseInt();
            if (id.Count() > 0)
            {
                int pid = int.Parse(id[0]);
                int seq = uow.Modules.TransferStock.GetDetails(qid) + 1;
                TransferStockDetail ob = uow.Modules.TransferStock.GetDetail(0);

                ob.TransferStockId = qid;
                ob.ProductId = pid;
                ob.RequestQty = qty;
                ob.Seq = seq;
                ob.WHId = WHId;

                bool result = uow.Modules.TransferStock.Add(ob);

                //uow.Modules.TransferStock.SetDetail(ob);
                //uow.SaveChanges();
            }
            return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "บันทึกข้อมูลสินค้าเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }
        [HttpPost]
        public ActionResult ConfirmProduct()
        {
            int qid = Request.Form["TransferStockId"].ParseInt();
            bool result = uow.Modules.TransferStock.TransferStockInApprove(qid);

            return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "บันทึกข้อมูลสินค้าเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        [HttpPost]
        //[Authorized]
        public ActionResult DetailDelete()
        {
            int qid = Request.Form["TransferStockId"].ParseInt();
            try
            {
                int Seq = Request.Form["Seq"].ParseInt();
                TransferStockDetail ob = uow.Modules.TransferStock.GetDetail(qid, Seq);
                if (ob == null)
                { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                ob.TransferStockId = qid;
                ob.Seq = Seq;

                bool result = uow.Modules.TransferStock.Del(ob);
                //uow.Modules.TransferStock.DeleteDetail(ob);
                //uow.SaveChanges();
                return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }
        [HttpPost]
        public JsonResult GetPaymentList()
        {
            List<TblPayment> lst = new List<TblPayment>();
            try
            {
                lst = uow.Modules.Payment.GetList();
                foreach (var ls in lst.ToList())
                {
                    ls.AcctReceive = uow.Modules.BankAccount.Get(ls.AcctReceiveId.HasValue?ls.AcctReceiveId.Value:0);
                    ls.StrPaymentDate = (ls.PaymentDate.HasValue? ls.PaymentDate.Value.Day.ToString("00"):"") +"/"+ (ls.PaymentDate.HasValue ? ls.PaymentDate.Value.Month.ToString("00"):"") + "/" + (ls.PaymentDate.HasValue ? ls.PaymentDate.Value.Year.ToString():"");
                    ls.StrUpdatedDate = ls.UpdatedDate.Day.ToString("00") + "/" + ls.UpdatedDate.Month.ToString("00") + "/" + ls.UpdatedDate.Year + " " + ls.UpdatedDate.ToString("HH:mm");
                }
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
            return Json(lst);
        }
    }
}
using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kemrex.Core.Common.Helper;
namespace Kemrex.Web.Main.Controllers
{
    public class QuotationController : KemrexController
    {
        public string dateText = DateTime.Now.ParseString(DateFormat.ddMMyyyy);
        public DateTime txt2Date = "2019-02-01 00:15:00".ParseDate(DateFormat.yyyyMMddHHmmss);
        // GET: Quotation
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            string src = "")
        {
            List<TblQuotation> lst = new List<TblQuotation>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
             /*   int total = uow.Modules.Quotation.Count(0, src);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "Qoutation", "")
                {
                    Page = (page ?? 1),
                    Size = (size ?? 10),
                    SearchCri = new Dictionary<string, dynamic>()
                    {
                        { "src", src }
                    },
                    SortExp = "",
                    Total = total
                };*/
               // ViewBag.Pagination = Pagination;
             //   lst = uow.Modules.Quotation.Gets(Pagination.Page, Pagination.Size, src);
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


        [HttpGet]
        public ActionResult GetList(int id)
        {
            List<TblQuotationDisplay> obList = uow.Modules.Quotation.GetList(id);

            return Json(obList,JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetList()
        {
            List<TblQuotation> obList = uow.Modules.Quotation.GetList();

            return Json(obList);
        }


        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TblQuotation ob = uow.Modules.Quotation.Get(id ?? 0);
           
            return ViewDetail(ob, msg, msgType);
        }


        [HttpGet]
        public JsonResult GetCustomer(string CustomerNo, string CustomerName, string ContactName)
        {

            List<TblCustomer> customerResult = uow.Modules.Customer.GetByCondition(CustomerNo, CustomerName, ContactName);





            return Json(customerResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProduct(string ProductCode, string ProductName)
        {
            List<TblProduct> customerResult = null;
            try
            {
                 customerResult = uow.Modules.Product.GetByCondition(ProductCode, ProductName).Where(o => o.PriceNet > 0).ToList();
            }
            catch (Exception ex)

            { }



            return Json(customerResult, JsonRequestBehavior.AllowGet);
        }


        private string genQuotationId()
        {
            System.Globalization.CultureInfo _cultureTHInfo = new System.Globalization.CultureInfo("en-US");
            string Id = uow.Modules.Quotation.GetLastId();
            string qid = "QU";
            var dt = DateTime.Now.ToString("yyMMdd", _cultureTHInfo);
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

        [HttpPost, ActionName("Clone")]
        public ActionResult CloneDetail()
        {
            int id = Request.Form["QuotationId"].ParseInt();
          
            TblQuotation objOrg = uow.Modules.Quotation.GetDetail(id);
            TblQuotation ob = Kemrex.Core.Common.Helper.Converting.ObjectClone<TblQuotation>(objOrg);
            ob.QuotationId = 0;
            ob.RefQuotationId = id;
            ob.OrgQuotationId = ob.OrgQuotationId.HasValue ? ob.OrgQuotationId : id;
            /*   if (ob.QuotationId <= 0)
                {
                    ob.QuotationNo = genQuotationId();
                    ob.CreatedBy = CurrentUID;
                    ob.UpdatedBy = CurrentUID;
                    ob.CreatedDate = CurrentDate;
                    ob.UpdatedDate = CurrentDate;
                    ob.QuotationDate = CurrentDate;
                }
                else
                {
                    ob.UpdatedBy = CurrentUID;
                    ob.UpdatedDate = CurrentDate;
                }
  */



            ob.QuotationNo = genQuotationId();
            ob.StatusId = 1;
            ob.CreatedBy = CurrentUID;
            ob.UpdatedBy = CurrentUID;
            ob.CreatedDate = CurrentDate;
            ob.UpdatedDate = CurrentDate;
            ob.QuotationDate = CurrentDate;
            ob.ApprovedBy = Convert.ToInt32(CurrentUID);
            ob.QuotationId = 0;
           
            try
            {

                if (ob.TblQuotationDetail != null && ob.TblQuotationDetail.Count > 0)
                {
                    foreach (TblQuotationDetail detail in ob.TblQuotationDetail)
                    {

                        detail.QuotationId = ob.QuotationId;
                        detail.Id = 0;
                    }
                }


                uow.Modules.Quotation.Set(ob);
                uow.SaveChanges();
/*
                if (ob.TblQuotationDetail != null && ob.TblQuotationDetail.Count > 0)
                {
                    foreach (TblQuotationDetail detail in ob.TblQuotationDetail)
                    {

                        detail.QuotationId = ob.QuotationId;
                        detail.Id = 0;
                    }
                }

                uow.Modules.QuotationDetail.Set(ob.TblQuotationDetail.ToList());
                uow.SaveChanges();
*/


                return RedirectToAction("Detail", MVCController, new { id = ob.QuotationId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(ob, msg, AlertMsgType.Danger);
            }
        }

      
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            bool updateOrgId = false;
            TblQuotation cloneObj = new TblQuotation();
            int id = Request.Form["QuotationId"].ParseInt();
            int approveStatus = Request.Form["hdApprove"] != null ? Request.Form["hdApprove"].ParseInt():0;
            TblQuotation ob = uow.Modules.Quotation.Get(id);
            if (ob.QuotationId <= 0)
            {
                updateOrgId = true;
                ob.QuotationNo = genQuotationId();
                ob.CreatedBy = CurrentUID;
                ob.UpdatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
                ob.UpdatedDate = CurrentDate;
                ob.QuotationDate = CurrentDate;
              //  ob.RefQuotationId = id;
              //  ob.OrgQuotationId = ob.OrgQuotationId.HasValue ? ob.OrgQuotationId : id;
            }
            else
            {
                ob.UpdatedBy = CurrentUID;
                ob.UpdatedDate = CurrentDate;
            }
            ob.StatusId = Request.Form["StatusId"].ParseInt();

            if (approveStatus == 3)
            {
                ob.StatusId = 3;
                ob.ApprovedBy = Convert.ToInt32(CurrentUID);
            }
            ob.QuotationRemark = Request.Form["QuotationRemark"]; ;
            ob.SaleId = Request.Form["SaleId"] != null ? Request.Form["SaleId"].ParseInt() : 0;
            ob.SaleName = Request.Form["SaleName"];
            if (Request.Form["DeliveryDate"].ToString().Count() > 0)
            {
                var dd = Request.Form["DeliveryDate"].Split(' ')[0] + " 00:00:00";

                ob.DeliveryDate = dd.ParseDate(DateFormat.ddMMyyyyHHmmss);
            }

            ob.CustomerId = Request.Form["CustomerId"].ParseInt();
            ob.CustomerName = Request.Form["CustomerName"];

            ob.ContractName = Request.Form["ContractName"];
            ob.ContractEmail = Request.Form["ContractEmail"];
            ob.ContractPhone = Request.Form["ContractPhone"];
            ob.BillingAddress = Request.Form["BillingAddress"];

            ob.ShippingAddress = Request.Form["ShippingAddress"];
          

            if (Request.Form["DiscountCash"] != null)
                ob.DiscountCash = decimal.Parse(Request.Form["DiscountCash"]);

            ob.QuotationValidDay = Request.Form["QuotationValidDay"].ParseInt();
            ob.QuotationCreditDay = Request.Form["QuotationCreditDay"].ParseInt();

            ob.DueDate = ob.CreatedDate.Value.AddDays(ob.QuotationValidDay).ToUniversalTime();

          


            try
            {

                uow.Modules.Quotation.Set(ob);

              
                uow.SaveChanges();
                if (updateOrgId)
                {
                    ob.OrgQuotationId = ob.QuotationId;
                    uow.Modules.Quotation.Set(ob);
                }
                uow.SaveChanges();
                return RedirectToAction("Detail", MVCController, new { id = ob.QuotationId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(ob, msg, AlertMsgType.Danger);
            }
        }

        [HttpPost]
        //[Authorized]
        public ActionResult Delete()
        {
            try
            {
                int id = Request.Form["QuotationId"].ParseInt();
                TblQuotation ob = uow.Modules.Quotation.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Quotation.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }
        #region Private Action
        private ActionResult ViewDetail(TblQuotation ob, string msg, AlertMsgType? msgType)
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
             


                ViewData["optQtDetail"] = uow.Modules.QuotationDetail.Gets(ob.QuotationId);
                ViewData["optCustomer"] = uow.Modules.Customer.GetAllAddress();
                ViewData["optCustomerAddress"] = uow.Modules.CustomerAddress.Gets();
                ViewData["optProduct"] = uow.Modules.Product.Gets();
                ViewData["optContact"] = uow.Modules.CustomerContact.Gets();
                ViewData["optEmployee"] = uow.Modules.Employee.Gets();
                ViewData["userAccount"] = CurrentUser;
                ViewData["optWareHouse"] = uow.Modules.WareHouse.Gets();
                ViewData["optPermission"] = GetPermission(ob.CreatedBy);
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
        [HttpPost]
        public ActionResult CustomerAddressfilter(int id)
        {
            ViewData["optCustomerAddress"] = uow.Modules.CustomerAddress.Gets(cusId: id);
            return View();
        }


        [HttpPost]
        //[Authorized]
        public ActionResult DetailDelete()
        {
            int qid = Request.Form["QuotationId"].ParseInt();
            try
            {
                int id = Request.Form["SelQuotationProductId"].ParseInt();
                TblQuotationDetail ob = uow.Modules.QuotationDetail.Get(id);
                if (ob == null)
                { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.QuotationDetail.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        public ActionResult AddProduct()
        {
            int qid = Request.Form["QuotationId"].ParseInt();
            var productId = Request.Form["selProduct"].Split(':');  //  ProductId:PriceNet
            var SelQuotationProductId = Request.Form["SelQuotationProductId"].ParseInt();  //  ProductId:PriceNet
            int qty = Request.Form["ProductQty"].ParseInt();
            Decimal price = Request.Form["ProductPrice"].ParseDecimal();
            Decimal discount = Request.Form["ProductDiscount"].ParseDecimal();
            int WHId = Request.Form["selWareHose"].ParseInt();
            decimal realDiscount = 0;
            var remark = Request.Form["Remark"];
            if (productId.Count() > 0)
            {
                int pid = int.Parse(productId[0]);

                TblQuotationDetail ob = SelQuotationProductId >0 ? uow.Modules.QuotationDetail.Get(SelQuotationProductId) : uow.Modules.QuotationDetail.Get(qid, Converting.ToInt(productId[0]), WHId);
                realDiscount = discount;
                ob.QuotationId = qid;
                ob.WHId = WHId;
                ob.ProductId = pid;
                ob.Quantity = qty;
                ob.CalType= Request.Form["selCalType"].ParseInt(); 
                ob.PriceNet = price * qty;
                ob.PriceVat = uow.Modules.System.GetVatFromNet(price * qty);
                // ob.PriceVat = (price * qty * vat) - (price * qty);
                ob.PriceTot = price * qty + ob.PriceVat;
                if (ob.CalType == 1)
                    realDiscount = ((ob.PriceNet * discount) / 100);

                ob.Discount = discount;
                ob.DiscountNet = realDiscount;
                ob.DiscountVat = uow.Modules.System.GetVatFromNet(realDiscount);
                ob.DiscountTot = realDiscount + ob.DiscountVat;

                ob.TotalNet = ob.PriceNet - realDiscount;

                ob.TotalVat= ob.PriceVat - ob.DiscountVat;
                ob.TotalTot = ob.PriceTot - ob.DiscountTot;

                ob.Remark = remark;
                uow.Modules.QuotationDetail.Set(ob);
                uow.SaveChanges();

                TblQuotation objQuoation = uow.Modules.Quotation.GetDetail(qid);

                objQuoation.SubTotalNet = objQuoation.TblQuotationDetail.Sum(o=>o.PriceNet);
                objQuoation.SubTotalVat = objQuoation.TblQuotationDetail.Sum(o => o.PriceVat);
                objQuoation.SubTotalTot= objQuoation.TblQuotationDetail.Sum(o => o.PriceTot);
                objQuoation.DiscountNet = objQuoation.TblQuotationDetail.Sum(o => o.DiscountNet);
                objQuoation.DiscountVat = objQuoation.TblQuotationDetail.Sum(o => o.DiscountVat);
                objQuoation.DiscountTot = objQuoation.TblQuotationDetail.Sum(o => o.DiscountTot);
                objQuoation.SummaryNet= objQuoation.TblQuotationDetail.Sum(o => o.TotalNet);
                objQuoation.SummaryVat = objQuoation.TblQuotationDetail.Sum(o => o.TotalVat);
                objQuoation.SummaryTot = objQuoation.TblQuotationDetail.Sum(o => o.TotalTot);

                uow.Modules.Quotation.Set(objQuoation);
                uow.SaveChanges();
            }
            return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "", msgType = AlertMsgType.Success });
        }
        public ActionResult saveDiscount()
        {
            int qid = Request.Form["QuotationId"].ParseInt();

            TblQuotation ob = uow.Modules.Quotation.Get(qid);
            ob.DiscountCash = Request.Form["DiscountCash"].ParseDecimal();
            uow.Modules.Quotation.Set(ob);
            uow.SaveChanges();
            return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "", msgType = AlertMsgType.Success });
        }
        public string getStatus(int id)
        {
            return uow.Modules.Quotation.GetStatus(id);
        }
        #endregion
    }
}
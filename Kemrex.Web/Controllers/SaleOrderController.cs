using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using IOFile = System.IO.File;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Models;
using Kemrex.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace Kemrex.Web.Controllers
{
    public class SaleOrderController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
                   string src = "")
        {
            List<TblSaleOrder> lst = new List<TblSaleOrder>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.SaleOrder.Count(0, src);
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "SaleOrder", "")
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
                lst = uow.Modules.SaleOrder.Gets(Pagination.Page, Pagination.Size, src);
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
            TblSaleOrder ob = uow.Modules.SaleOrder.Get(id ?? 0);
            return ViewDetail(ob, msg, msgType);
        }

        private string genSaleOrderId(string pre)
        {

            var dt = CurrentDate.ToString("ddMMyy");


            pre += "-" + CurrentDate.ToString("yy") + "/" + CurrentDate.ToString("MM");

            string Id = uow.Modules.SaleOrder.GetLastId(pre);
            int runid = 1;
            if (Id != null)
            {
                runid = int.Parse(Id.Substring(Id.Length - 3)) + 1;
            }


            string soNo = pre + runid.ToString("D3");
            return soNo;
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {

            int id = Request.Form["SaleOrderId"].ParseInt();
            TblSaleOrder ob = uow.Modules.SaleOrder.Get(id);
            if (ob.SaleOrderId <= 0)
            {
                string pre = Request.Form["SaleOrderPreNo"];
                ob.SaleOrderNo = genSaleOrderId(pre);
                ob.CreatedBy = CurrentUID;
                ob.UpdatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
                ob.UpdateDate = CurrentDate;
                ob.SaleOrderDate = CurrentDate;
            }
            else
            {
                ob.UpdatedBy = CurrentUID;
                ob.UpdateDate = CurrentDate;
            }

            ob.QuotationNo = Request.Form["selQuotationNo"];
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
            ob.BillingAddress = Request.Form["BillingAddress"];

            ob.ShippingAddress = Request.Form["ShippingAddress"];
            ob.SaleOrderRemark = Request.Form["SaleOrderRemark"];

            if (Request.Form["DiscountCash"] != null)
                ob.DiscountCash = decimal.Parse(Request.Form["DiscountCash"]);

            ob.SaleOrderCreditDay = Request.Form["SaleOrderCreditDay"].ParseInt();

            ob.StatusId = Request.Form["StatusId"].ParseInt();
            ob.ConditionId = Request.Form["ConditionId"].ParseInt();
            try
            {

                uow.Modules.SaleOrder.Set(ob);
                uow.SaveChanges();

                return RedirectToAction("Detail", MVCController, new { id = ob.SaleOrderId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
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
                int id = Request.Form["SaleOrderId"].ParseInt();
                TblSaleOrder ob = uow.Modules.SaleOrder.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.SaleOrder.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }
        #region Private Action
        private ActionResult ViewDetail(TblSaleOrder ob, string msg, AlertMsgType? msgType)
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

                ViewData["optSoDetail"] = uow.Modules.SaleOrderDetail.Gets(ob.SaleOrderId);
                ViewData["optQuotation"] = uow.Modules.Quotation.Gets()
                                            .Where(q => q.StatusId == 3)
                                            .ToList();
                ViewData["optCustomer"] = uow.Modules.Customer.GetAllAddress();
                ViewData["optCustomerAddress"] = uow.Modules.CustomerAddress.Gets();
                ViewData["optProduct"] = uow.Modules.Product.Gets();
                ViewData["optContact"] = uow.Modules.CustomerContact.Gets();
                ViewData["optEmployee"] = uow.Modules.Employee.Gets();
                ViewData["optPayment"] = uow.Modules.PaymentCondition.Gets();
                ViewData["optAttachment"] = uow.Modules.SaleOrderAttachment.Gets(ob.SaleOrderId);
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
            int sid = Request.Form["SaleOrderId"].ParseInt();
            try
            {
                int id = Request.Form["Id"].ParseInt();
                TblSaleOrderDetail ob = uow.Modules.SaleOrderDetail.Get(id);
                if (ob == null)
                { return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Product", msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.SaleOrderDetail.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Product", msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Product", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        public ActionResult AddProduct()
        {
            int sid = Request.Form["SaleOrderId"].ParseInt();
            var id = Request.Form["selProduct"].Split(':');  //  ProductId:PriceNet
            int qty = Request.Form["ProductQty"].ParseInt();
            Decimal price = Request.Form["ProductPrice"].ParseDecimal();
            Decimal discount = Request.Form["ProductDiscount"].ParseDecimal();

            if (id.Count() > 0)
            {
                int pid = int.Parse(id[0]);

                TblSaleOrderDetail ob = uow.Modules.SaleOrderDetail.Get(0);

                ob.SaleOrderId = sid;
                ob.ProductId = pid;
                ob.Quantity = qty;
                ob.PriceNet = price * qty;
                ob.PriceVat = uow.Modules.System.GetVatFromNet(price * qty);
                // ob.PriceVat = (price * qty * vat) - (price * qty);
                ob.PriceTot = price * qty + ob.PriceVat;
                ob.DiscountNet = discount;
                ob.DiscountVat = uow.Modules.System.GetVatFromNet(discount);
                ob.DiscountTot = discount + ob.DiscountVat;

                uow.Modules.SaleOrderDetail.Set(ob);
                uow.SaveChanges();


            }
            return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Product", msg = "", msgType = AlertMsgType.Success });
        }
        public ActionResult saveDiscount()
        {
            int qid = Request.Form["SaleOrderId"].ParseInt();

            TblSaleOrder ob = uow.Modules.SaleOrder.Get(qid);
            ob.DiscountCash = Request.Form["DiscountCash"].ParseDecimal();
            uow.Modules.SaleOrder.Set(ob);
            uow.SaveChanges();
            return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "", msgType = AlertMsgType.Success });
        }

        [HttpPost]
        public ActionResult QT2SO()
        {
            int sid = 0;
            if (string.IsNullOrEmpty(Request.Form["lbSaleOrderId"]))
            {
                sid = Request.Form["lbSaleOrderId"].ParseInt();
            }
            else
            {
                TblSaleOrder ob = uow.Modules.SaleOrder.Get(sid);
                string pre = Request.Form["lbPreSONo"];
                ob.SaleOrderNo = genSaleOrderId(pre);
                ob.CreatedBy = CurrentUID;
                ob.UpdatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
                ob.UpdateDate = CurrentDate;
                ob.SaleOrderDate = CurrentDate;
                ob.CustomerId = 1;
                ob.SaleId = 1;
                ob.StatusId = 1;
                ob.ConditionId = 1;
                uow.Modules.SaleOrder.Set(ob);
                uow.SaveChanges();

                sid = ob.SaleOrderId;
            }
            if (sid > 0)
            {

                string qno = Request.Form["lbQuotationId"];

                TblSaleOrder so = uow.Modules.SaleOrder.Get(sid);
                var qid = uow.Modules.Quotation.GetId(qno);
                TblQuotation qt = uow.Modules.Quotation.Get(qid);
                List<TblQuotationDetail> qtd = uow.Modules.QuotationDetail.Gets(qid)
                                                    .OrderBy(c => c.Id).ToList();


                // clear saleorderdetail
                uow.Modules.SaleOrderDetail.DeleteId(sid);
                //  uow.SaveChanges();

                // set QT to SO
                so.QuotationNo = qno;
                so.CustomerId = qt.CustomerId;
                so.CustomerName = qt.CustomerName;
                so.ContractName = qt.ContractName;
                so.SaleId = qt.SaleId;
                so.SaleName = qt.SaleName;
                so.BillingAddress = qt.BillingAddress;
                so.ShippingAddress = qt.ShippingAddress;
                so.SaleOrderRemark = qt.QuotationRemark;
                so.SubTotalNet = qt.SubTotalNet;
                so.SubTotalTot = qt.SubTotalTot;
                so.SubTotalVat = qt.SubTotalVat;
                so.DiscountCash = qt.DiscountCash;
                so.DiscountNet = qt.DiscountNet;
                so.DiscountTot = qt.DiscountTot;
                so.DiscountVat = qt.DiscountVat;
                so.SummaryNet = qt.SummaryNet;
                so.SummaryTot = qt.SummaryTot;
                so.SummaryVat = qt.SummaryVat;

                uow.Modules.SaleOrder.Set(so);
                // uow.SaveChanges();


                //Add QT detail to SO detail
                if (qtd.Count() > 0)
                {
                    qtd.ForEach(dt =>
                    {
                        TblSaleOrderDetail sod = uow.Modules.SaleOrderDetail.Get(0);
                        sod.SaleOrderId = sid;
                        sod.ProductId = dt.ProductId;
                        sod.Quantity = dt.Quantity;
                        sod.PriceNet = dt.PriceNet;
                        sod.PriceVat = dt.PriceVat;
                        sod.PriceTot = dt.PriceTot;
                        sod.DiscountNet = dt.DiscountNet;
                        sod.DiscountTot = dt.DiscountTot;
                        sod.DiscountVat = dt.DiscountVat;
                        sod.TotalNet = dt.TotalNet;
                        sod.TotalTot = dt.TotalTot;
                        sod.TotalVat = dt.TotalVat;
                        uow.Modules.SaleOrderDetail.Set(sod);
                    });

                }

                uow.SaveChanges();
            }
            return RedirectToAction("Detail", MVCController, new { id = sid, msg = "", msgType = AlertMsgType.Success });
        }

        // This action handles the form POST and the upload
        [HttpPost]
        public ActionResult UploadFile()
        {
            // Verify that the user selected a file
            string sid = Request.Form["soId"];
            string FilePath = "";
            try
            {
                if (Request.Files.Count > 0 && Request.Files["FileAttachment"] != null && Request.Files["FileAttachment"].ContentLength > 0)
                {

                    TblSaleOrderAttachment sa = uow.Modules.SaleOrderAttachment.Get(0);

                    HttpPostedFileBase uploadedFile = Request.Files["FileAttachment"];
                    FilePath = string.Format("files/so/{0}", sid);
                    if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                    if (!Directory.Exists(Server.MapPath("~/files/so"))) { Directory.CreateDirectory(Server.MapPath("~/files/so")); }
                    if (!Directory.Exists(Server.MapPath("~/" + FilePath))) { Directory.CreateDirectory(Server.MapPath(FilePath)); }
                    FilePath += "/" + Path.GetFileName(uploadedFile.FileName);
                    sa.AttachmentPath = FilePath;
                    sa.SaleOrderId = int.Parse(sid);
                    sa.AttachmentRemark = Path.GetExtension(uploadedFile.FileName);
                    sa.AttachmentOrder = uow.Modules.SaleOrderAttachment.GetLastOrder(int.Parse(sid)) + 1;
                    uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));

                    uow.Modules.SaleOrderAttachment.Set(sa);
                    uow.SaveChanges();
                }  else
                {
                    string msg = "ไม่พบไฟล์แนบ";
                        return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Attachment", fi = FilePath, msg, msgType = AlertMsgType.Danger });
                }
            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Attachment", fi = FilePath, msg, msgType = AlertMsgType.Danger });
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Attachment", msg = "", msgType = AlertMsgType.Success });
        }
        [HttpPost]
        public ActionResult DeleteFile()
        {
            string sid = Request.Form["soId"];
            string atid = Request.Form["atId"];
            string atname = Request.Form["atName"];

            // Delete phisical file
            if (!string.IsNullOrWhiteSpace(atname) && IOFile.Exists(Server.MapPath("~/" + atname)))
            { IOFile.Delete(Server.MapPath("~/" + atname)); }
            // Delete database
            uow.Modules.SaleOrderAttachment.Delete(int.Parse(atid));
            uow.SaveChanges();

            return RedirectToAction("Detail", MVCController, new { id = sid, tab = "Attachment", msg = "", msgType = AlertMsgType.Success });
        }
        #endregion
    }



}

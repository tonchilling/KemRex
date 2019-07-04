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

namespace Kemrex.Web.Main.Controllers
{
    public class TransferStockInController : KemrexController
    {
        // GET: TransferStockIn
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
                    string src = "")
        {
            List<TransferStockHeader> lst = new List<TransferStockHeader>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.TransferStock.Count(0, "I");
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "TransferStockIn", "")
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
                lst = uow.Modules.TransferStock.Gets(Pagination.Page, Pagination.Size, src, "I");
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
            string qid = "T" + type;
            System.Globalization.CultureInfo _cultureTHInfo = new System.Globalization.CultureInfo("en-US");
            var dt = DateTime.Now.ToString("yyMMdd", _cultureTHInfo);
            string Id = uow.Modules.TransferStock.GetLastId(qid + dt);

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


        [HttpPost, ActionName("SetDetail")]
        public ActionResult SetDetail(TransferStockHeader obj)
        {
            System.Globalization.CultureInfo _cultureTHInfo = new System.Globalization.CultureInfo("en-US");


            string TransferStockId = this.Request.Form["TransferStockId"];

            /*  string StartHH = this.Request.Form["StartHH"];
              string StartMM = this.Request.Form["StartMM"];
              string EndHH = this.Request.Form["EndHH"];
              string EndMM = this.Request.Form["EndMM"];
              string SaleName = this.Request.Form["SaleName"];
              string SaleMobile = this.Request.Form["SaleMobile"];
              string hddProject = this.Request.Form["hddProject"];
              string hddEquipmentType = this.Request.Form["hddEquipmentType"];
              string hddLandType = this.Request.Form["hddLandType"];
              string hddUndergroundType = this.Request.Form["hddUndergroundType"];
              string hddObstructionType = this.Request.Form["hddObstructionType"];
              string hddAttachmentType = this.Request.Form["hddAttachmentType"];
          */
            if (TransferStockId == "" || TransferStockId == "0")
            {
                obj.TransferNo = getId("TI");
                obj.CreateDate = DateTime.Now;
            }
            else
            {
                obj.TransferStockId = Convert.ToInt32(TransferStockId);
                obj.UpdateDate = DateTime.Now;
            }

            obj.TransferType = "I";
            if (Request.Form["TransferDate"].ToString() != "")
            {
                var dd = Request.Form["TransferDate"];

                obj.TransferDate = dd.ParseDate(DateFormat.ddMMyyyy, culInfo: _cultureTHInfo);
            }

            obj.EmpId = "1";



            /*# hddProject
            # hddEquipmentType
            # hddLandType
            # hddUndergroundType
            # hddObstructionType
            # hddAttachmentType
                        */


            try
            {

                uow.Modules.TransferStock.Set(obj);
                uow.SaveChanges();




                //  uow.Modules.Transfer.Set(obj);

                //  uow.SaveChanges();


            }
            catch (Exception ex)
            {
                string msg = ex.GetMessage(true);
                return ViewDetail(obj, msg, AlertMsgType.Danger);
            }

            return RedirectToAction("Detail", MVCController, new { id = obj.TransferStockId, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });


            //  return RedirectToAction("Detail", MVCController, new { id = 1, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        public ActionResult Detail(int? id, string msg, AlertMsgType? msgType)
        {
            TransferStockHeader ob = uow.Modules.TransferStock.Get(id ?? 0);

            return ViewDetail(ob, msg, msgType);
        }

        private ActionResult ViewDetail(TransferStockHeader ob, string msg, AlertMsgType? msgType)
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


                ViewData["optCustomer"] = uow.Modules.Customer.GetAllAddress();
                ViewData["optCustomerAddress"] = uow.Modules.CustomerAddress.Gets();
                ViewData["optProduct"] = uow.Modules.Product.Gets();
                ViewData["optContact"] = uow.Modules.CustomerContact.Gets();
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



            if (id.Count() > 0)
            {
                int pid = int.Parse(id[0]);
                int seq = uow.Modules.TransferStock.GetDetails(qid) + 1;
                TransferStockDetail ob = uow.Modules.TransferStock.GetDetail(0);

                ob.TransferStockId = qid;
                ob.ProductId = pid;
                ob.RequestQty = qty;
                ob.Seq = seq;



                uow.Modules.TransferStock.SetDetail(ob);
                uow.SaveChanges();


            }
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

                uow.Modules.TransferStock.DeleteDetail(ob);
                uow.SaveChanges();
                return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Detail", MVCController, new { id = qid, tab = "Product", msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }
    }
}
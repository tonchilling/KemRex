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
    public class JobOrderController : KemrexController
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

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            return RedirectToAction("Detail", MVCController, new { id = 1, msg = "บันทึกข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
        }

        [HttpPost]
        //[Authorized]
        public ActionResult Delete()
        {
            return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning });
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
                }
                else
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

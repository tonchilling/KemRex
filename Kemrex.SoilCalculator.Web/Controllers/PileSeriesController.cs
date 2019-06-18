using DMN.Standard.Common.Extensions;
using IOFile = System.IO.File;
using Kemrex.Database;
using Kemrex.FWCommon;
using Kemrex.SoilCalculator.Web.ActionFilters;
using Kemrex.SoilCalculator.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMN.Standard.Common.Constraints;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class PileSeriesController : BaseController
    {
        public const int SITE_ID = 3;
        public UnitOfWork uow;
        public int CurrentUID => (Session["sid"].Convert2String().ParseIntNullable() ?? 0);
        public SysAccount CurrentUser => uow.Account.Get(CurrentUID);
        public DateTime CurrentDateTime = DateTime.Now;
        public PileSeriesController()
        {
            uow = new UnitOfWork();
        }


        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            string src = "")
        {
            List<TblPileSeries> lst = new List<TblPileSeries>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = db.TblPileSeries.Count();
                WidgetPaginationModel Pagination = new WidgetPaginationModel("Index", "PileSires", "")
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
                lst = db.TblPileSeries.OrderBy(x => x.SeriesOrder).Skip((Pagination.Page - 1) * Pagination.Size).Take(Pagination.Size).ToList();
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
            TblPileSeries ob = db.TblPileSeries.Find(id ?? 0) ?? new TblPileSeries();
            return ViewDetail(ob, msg, msgType);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int id = Request.Form["SeriesId"].ParseInt();
            TblPileSeries ob = db.TblPileSeries.Find(id) ?? new TblPileSeries();
            ob.SeriesName = Request.Form["SeriesName"];
            ob.SeriesOrder = Request.Form["SeriesOrder"].ParseInt();
            try
            {
                if (Request.Files.Count > 0 && Request.Files["SeriesImage"] != null && Request.Files["SeriesImage"].ContentLength > 0)
                {
                    string oldPath = ob.SeriesImage;
                    HttpPostedFileBase uploadedFile = Request.Files["SeriesImage"];
                    string FilePath = string.Format("files/products/{0}{1}", CurrentDate.ParseString(DateFormat._yyyyMMddHHmmssfff), Path.GetExtension(uploadedFile.FileName));
                    if (!Directory.Exists(Server.MapPath("~/files"))) { Directory.CreateDirectory(Server.MapPath("~/files")); }
                    if (!Directory.Exists(Server.MapPath("~/files/products"))) { Directory.CreateDirectory(Server.MapPath("~/files/products")); }
                    uploadedFile.SaveAs(Server.MapPath("~/" + FilePath));
                    ob.SeriesImage = FilePath;
                    if (!string.IsNullOrWhiteSpace(oldPath) && IOFile.Exists(Server.MapPath("~/" + oldPath)))
                    { IOFile.Delete(Server.MapPath("~/" + oldPath)); }
                }
                else if (ob.SeriesId <= 0) { throw new Exception("กรุณาระบุรูปซีรี่ย์ของสินค้า"); }
                //Validate model b4 save

                if (ob.SeriesId <= 0) { db.TblPileSeries.Add(ob); }
                else { db.Entry(ob).State = System.Data.Entity.EntityState.Modified; }
                db.SaveChanges();

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
        [BackendAuthorized]
        public ActionResult Delete()
        {
            try
            {
                int id = Request.Form["SeriesId"].ParseInt();
                TblPileSeries ob = db.TblPileSeries.Find(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                db.TblPileSeries.Remove(ob);
                db.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        #region Private Action
        private ActionResult ViewDetail(TblPileSeries ob, string msg, AlertMsgType? msgType)
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
        #endregion
    }
}
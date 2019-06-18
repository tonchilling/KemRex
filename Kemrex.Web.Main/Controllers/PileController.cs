using DMN.Standard.Common.Extensions;
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
    public class PileController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
            string src = "")
        {
            List<TblPile> lst = new List<TblPile>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Pile.Count(src);
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
                lst = uow.Modules.Pile.Gets(Pagination.Page, Pagination.Size, src);
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
            TblPile ob = uow.Modules.Pile.Get(id ?? 0);
            return ViewDetail(ob, msg, msgType);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int id = Request.Form["PileId"].ParseInt();
            TblPile ob = uow.Modules.Pile.Get(id);
            ob.PileName = Request.Form["PileName"];
            ob.SeriesId = Request.Form["SeriesId"].ParseInt();
            ob.Series = uow.Modules.PileSeries.Get(ob.SeriesId.Value);
            ob.PileLength = Request.Form["PileLength"].ParseInt();
            ob.PileDia = Request.Form["PileDia"].ParseDecimal();
            ob.PileBlade = Request.Form["PileBlade"].ParseDecimal();
            ob.PileSpiralDepth = Request.Form["PileSpiralDepth"].ParseDecimal();
            ob.PileSpiralLength = Request.Form["PileSpiralLength"].ParseDecimal();
            ob.PileFlangeLength = Request.Form["PileFlangeLength"].ParseDecimal();
            ob.PileFlangeWidth = Request.Form["PileFlangeWidth"].ParseDecimal();
            ob.PileSeriesOrder = Request.Form["PileSeriesOrder"].ParseInt();
            try
            {
                //Validate model b4 save

                uow.Modules.Pile.Set(ob);
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
        public ActionResult Delete()
        {
            try
            {
                int id = Request.Form["PileId"].ParseInt();
                TblPile ob = uow.Modules.Pile.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Pile.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        #region Private Action
        private ActionResult ViewDetail(TblPile ob, string msg, AlertMsgType? msgType)
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
                ViewData["optSeries"] = uow.Modules.PileSeries.Gets();
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
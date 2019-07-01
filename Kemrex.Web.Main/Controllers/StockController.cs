﻿using DMN.Standard.Common.Extensions;
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
    public class StockController : KemrexController
    {
        public ActionResult Index(int? page, int? size, string msg, AlertMsgType? msgType,
             string src = "")
        {
            List<TblProduct> lst = new List<TblProduct>();
            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    WidgetAlertModel alert = new WidgetAlertModel() { Message = msg };
                    if (msgType.HasValue) { alert.Type = msgType.Value; }
                    ViewBag.Alert = alert;
                }
                int total = uow.Modules.Product.Count(src);
                WidgetPaginationModel Pagination = new WidgetPaginationModel(MVCAction, MVCController, MVCArea)
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
                lst = uow.Modules.Product.Gets(Pagination.Page, Pagination.Size, src);
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
            TblProduct ob = uow.Modules.Product.Get(id ?? 0);
            return ViewDetail(ob, msg, msgType);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Detail")]
        public ActionResult SetDetail()
        {
            int id = Request.Form["ModelId"].ParseInt();
            TblProduct ob = uow.Modules.Product.Get(id);
            if (ob.ProductId <= 0)
            {
                ob.CategoryId = Request.Form["CategoryId"].ParseInt();
                ob.Category = uow.Modules.ProductCategory.Get(ob.CategoryId);
                ob.CreatedBy = CurrentUID;
                ob.CreatedDate = CurrentDate;
            }
            int modelId = Request.Form["ModelId"].ParseInt();
            if (modelId <= 0)
            {
                ob.ModelId = null;
                ob.Model = null;
            }
            else
            {
                ob.ModelId = modelId;
                ob.Model = uow.Modules.ProductModel.Get(modelId);
            }
            ob.ProductCode = Request.Form["ProductCode"];
            ob.ProductName = Request.Form["ProductName"];
            ob.ProductNameBilling = Request.Form["ProductNameBilling"];
            ob.ProductNameTrade = Request.Form["ProductNameTrade"];
            ob.UnitId = Request.Form["UnitId"].ParseInt();
            ob.PriceNet = Request.Form["PriceNet"].ParseDecimal();
            ob.PriceVat = Request.Form["PriceVat"].ParseDecimal();
            ob.PriceTot = ob.PriceNet + ob.PriceVat;
            ob.CostNet = 0;
            ob.CostVat = 0;
            ob.CostTot = 0;
            ob.QtyStock = Request.Form["QtyStock"].ParseInt();
            ob.FlagVat = true;
            ob.FlagActive = Request.Form["FlagDeactive"].ParseBoolean() ? false : true;
            ob.UpdatedBy = CurrentUID;
            ob.UpdatedDate = CurrentDate;
            try
            {
                //Validate model b4 save

                uow.Modules.Product.Set(ob);
                uow.SaveChanges();

                return RedirectToAction("Index", new
                {
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
                int id = Request.Form["Id"].ParseInt();
                TblProduct ob = uow.Modules.Product.Get(id);
                if (ob == null)
                { return RedirectToAction("Index", MVCController, new { msg = "ไม่พบข้อมูลที่ต้องการ", msgType = AlertMsgType.Warning }); }

                uow.Modules.Product.Delete(ob);
                uow.SaveChanges();
                return RedirectToAction("Index", MVCController, new { msg = "ลบข้อมูลเรียบร้อยแล้ว", msgType = AlertMsgType.Success });
            }
            catch (Exception ex)
            { return RedirectToAction("Index", MVCController, new { msg = ex.GetMessage(), msgType = AlertMsgType.Danger }); }
        }

        #region Private Action
        private ActionResult ViewDetail(TblProduct ob, string msg, AlertMsgType? msgType)
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
                ViewData["optCategory"] = uow.Modules.ProductCategory.Gets();
                ViewData["optModel"] = uow.Modules.ProductModel.Gets();
                ViewData["optUnit"] = uow.Modules.Unit.Gets();
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
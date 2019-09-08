using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Kemrex.Core.Common.Helper;

namespace Kemrex.Web.Main.Controllers
{
    public class ReportController : KemrexController
    {
        // GET: Report
        public ActionResult Index()
        {
            AccountPermission permission = GetPermission(CurrentUID);
            List<SaleOrderHeader> ob = uow.Modules.SaleOrder.GetHeader((permission.IsAdminTeam || permission.IsManager) ? 0 : CurrentUID);

            List<int> yearAll =ob.GroupBy(o => o.DSaleOrderDate.Value.Year).Select(grp => grp.Key).ToList();
            ViewData["optYearAll"] = yearAll;

            return View();
        }



        [HttpGet]
        public JsonResult GetReport(string searchYear)
        {
            ChartModel  Result = null;
            List<string> xAix = new List<string>();
            List<string> yAix = new List<string>();
            List<string> yAix2 = new List<string>();
            AccountPermission permission = GetPermission(CurrentUID);
          
            try
            {
               
                List<SaleOrderHeader> obSaleOrder = uow.Modules.SaleOrder.GetHeader((permission.IsAdminTeam || permission.IsManager) ? 0 : CurrentUID);
                List<TblQuotation> objQuation = uow.Modules.Quotation.GetList((permission.IsAdminTeam || permission.IsManager) ? 0 : CurrentUID);
              
                xAix.AddRange(Converting.GetShortMonth());

                int intMonth = 1;

                obSaleOrder = obSaleOrder.Where(o => o.DSaleOrderDate.Value.Year.ToString() == searchYear).ToList();
                objQuation = objQuation.Where(o => o.QuotationDate.Year.ToString() == searchYear).ToList();
                foreach (string month in Converting.GetShortMonth())
                {
                    string monthyearFormat = string.Format("{0}{1}", searchYear, intMonth.ToString("##00"));
                    decimal sumSaleOrder = obSaleOrder.Where(o=>(o.DSaleOrderDate.Value.Year.ToString()+ o.DSaleOrderDate.Value.Month.ToString("##00")== monthyearFormat)).Sum(o=> o.SummaryTot.HasValue?o.SummaryTot.Value:0);
                    yAix.Add(sumSaleOrder.ToString());

                    decimal sumQuotation = objQuation.Where(o => (o.QuotationDate.Year.ToString() + o.QuotationDate.Month.ToString("##00") == monthyearFormat)).Sum(o => o.SummaryTot.HasValue ? o.SummaryTot.Value : 0);
                    yAix2.Add(sumQuotation.ToString());
                    intMonth++;
                }

                


              /*  xAix.Add("Jan");
                xAix.Add("Feb");

                yAix.Add("10000");
                yAix.Add("20000");*/
                Result = new ChartModel();
                Result.xAixData = xAix;
                Result.yAixData = yAix;
                Result.yAixData2 = yAix2;
              //  customerResult = uow.Modules.Product.GetByCondition(ProductCode, ProductName).Where(o => o.PriceNet > 0).ToList();
            }
            catch (Exception ex)

            { }



            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}
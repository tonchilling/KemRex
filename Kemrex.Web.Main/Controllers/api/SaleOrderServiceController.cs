using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Kemrex.Core.Common.Helper;
using Kemrex.Core.Common.Constraints;
using System.Web.Script.Serialization;
using Kemrex.Core.Database.Models;
using Kemrex.Core.Common;
namespace Kemrex.Web.Main.Controllers.api
{
    [RoutePrefix("api/SaleOrderService")]
    public class SaleOrderServiceController : ApiController
    {
        public UnitOfWork uow;

        [HttpGet]
        public IHttpActionResult GetDetail(int? id)
        {
            
            uow = new UnitOfWork(SystemHelper.GetConfigurationKey(ConfigKey.CONNECTION_DB));
            TblSaleOrder ob = uow.Modules.SaleOrder.GetFull(id ?? 0);
           // ob.TblSaleOrderDetail = uow.Modules.SaleOrderDetail.Gets(id ?? 0);
            // TblCustomer objCustomer = uow.Modules.Customer.Get(Convert.ToInt32(ob.CustomerId));
            var js = new JavaScriptSerializer();


            return Json(ob);
        }

    }
}

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
    [RoutePrefix("api/CommonService")]
    public class CommonController : ApiController
    {

        public UnitOfWork uow;

        [HttpGet]
        [Route("Master/GetTeamOperations")]
        public IHttpActionResult GetTeamOperation(int? id)
        {

            uow = new UnitOfWork(SystemHelper.GetConfigurationKey(ConfigKey.CONNECTION_DB));
            var ob = uow.Modules.TeamOperation.GetAll();
           


            return Json(ob);
        }

    }
}

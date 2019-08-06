using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Kemrex.Core.Common.Helper;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{


    public class SurveyModule
    {

        private readonly mainContext db;

        public SurveyModule(mainContext context)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); ;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US"); ;
            db = context;
        }

        public void Delete(TblSurveyHeader ob)
        {
            throw new System.NotImplementedException();
        }

        public List<TblJobOrderSurveyDetail> Get(int id)
        {
            var data = (from i in db.TblJobOrderSurveyDetail
                        join t in db.SysSurveyDetailTemplate
                        on i.SurveyDetailId equals t.SurveyDetailID
                        where i.JobOrderId == id
                        select new TblJobOrderSurveyDetail
                        {
                            JobOrderId = i.JobOrderId,
                            SurveyDetailId = i.SurveyDetailId,
                            IsCheck = i.IsCheck,
                            Desc = i.Desc,
                            StatusId = i.StatusId,
                            SubSurveyID = t.SubSurveyId,
                            No = t.No
                        });
            return data.ToList();
        }

        public bool IsExist(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Set(TblSurveyHeader ob)
        {
            throw new System.NotImplementedException();
        }

        public List<SysSurveyHeaderTemplate> GetList()
        {
            var data = (from i in db.SysSurveyHeaderTemplate
                        select new SysSurveyHeaderTemplate
                        {
                          SurveyId= i.SurveyId,
       Approve1 = i.Approve1,
                            Approve2 = i.Approve2,
                            Approve3 = i.Approve3,
                            Approve4 = i.Approve4,
                            StatusId = i.StatusId,
        Desc = i.Desc,
       CreatedDate= i.CreatedDate,
       UpdatedDate = i.UpdatedDate,
        CreatedBy= i.CreatedBy,
      UpdatedBy = i.UpdatedBy,

       SurveySubList = (from x in db.SysSurveyHeaderSubTemplate.Where(xx=>xx.SurveyId==i.SurveyId) select new SysSurveyHeaderSubTemplate {
           SurveyId =x.SurveyId,
           SubSurveyId = x.SurveyId,
           Order = x.Order,
           Desc = x.Desc,
           IsReason = x.IsReason,
           Reason = x.Reason,
           StatusId = x.StatusId,
           SurveyDetailList = (from m in db.SysSurveyDetailTemplate.Where(mm => mm.SurveyId == i.SurveyId && mm.SubSurveyId==x.SubSurveyId) select m).ToList()
    }).ToList()

                        });
            return data.ToList();
        }
    }
}

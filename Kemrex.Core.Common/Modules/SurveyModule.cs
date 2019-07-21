using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Kemrex.Core.Common.Helper;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{


    public class SurveyModule : IModule<TblSurveyHeader, int>
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

        public TblSurveyHeader Get(int id)
        {
            throw new System.NotImplementedException();
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

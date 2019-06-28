using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Modules
{
    public class QuotationDetailModule : IModule<TblQuotationDetail, int>
    {
        private readonly mainContext db;

        public QuotationDetailModule(mainContext context)
        {
            db = context;
        }                           

        public int Count(int QtID = 0, string src = "")
        {
            var data = db.TblQuotationDetail.AsQueryable();
            // data = Filter(data, groupId, src);
            return data.Count();
        }           
        public void Delete(TblQuotationDetail ob)
        {
            if (IsExist(ob.Id))
            { db.TblQuotationDetail.Remove(ob); }
        }
        public bool IsExist(int id)
        { return db.TblQuotationDetail.Where(x => x.Id == id).Count() > 0 ? true : false; }

        public TblQuotationDetail Get(int id=0)
        {
            return db.TblQuotationDetail
                .Where(x => x.Id == id)
                .FirstOrDefault() ?? new TblQuotationDetail();
        }
        public List<TblQuotationDetail> Gets(int QuotationId=0)
        {
            var data = db.TblQuotationDetail
              .AsQueryable();
            // data = Filter(data, groupId, src);
            var query = (from qd in data
                         join p in db.TblProduct
                         on qd.ProductId equals p.ProductId
                         where qd.QuotationId == QuotationId
                         select qd).ToList();
            return query;
        }
        bool IModule<TblQuotationDetail, int>.IsExist(int id) => throw new NotImplementedException();

        public void Set(TblQuotationDetail ob)
        {
            if (ob.Id <= 0)
            { db.TblQuotationDetail.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }

        TblQuotationDetail IModule<TblQuotationDetail, int>.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

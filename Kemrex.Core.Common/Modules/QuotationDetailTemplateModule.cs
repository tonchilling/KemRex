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
    public class QuotationDetailTemplateModule : IModule<TblQuotationDetailTemplate, int>
    {
        private readonly mainContext db;

        public QuotationDetailTemplateModule(mainContext context)
        {
            db = context;
        }                           

        public int Count(int QtID = 0, string src = "")
        {
            var data = db.TblQuotationDetailTemplate.AsQueryable();
            // data = Filter(data, groupId, src);
            return data.Count();
        }           
        public void Delete(TblQuotationDetailTemplate ob)
        {
            if (IsExist(ob.Id))
            { db.TblQuotationDetailTemplate.Remove(ob); }
        }
        public bool IsExist(int id)
        { return db.TblQuotationDetailTemplate.Where(x => x.Id == id).Count() > 0 ? true : false; }

        public TblQuotationDetailTemplate Get(int id=0)
        {
            return db.TblQuotationDetailTemplate
                .Where(x => x.Id == id)
                .FirstOrDefault() ?? new TblQuotationDetailTemplate();
        }

        public TblQuotationDetailTemplate Get(int QuationId = 0,int ProductId = 0, int WhId= 0)
        {
            return db.TblQuotationDetailTemplate
                .Where(x => x.QuotationId == QuationId && x.ProductId== ProductId && x.Whid == WhId)
                .FirstOrDefault() ?? new TblQuotationDetailTemplate();
        }
        public List<TblQuotationDetailTemplate> Gets(int QuotationId=0)
        {
            var data = db.TblQuotationDetailTemplate
              .AsQueryable();
            // data = Filter(data, groupId, src);
            var query = (from qd in data
                         join p in db.TblProduct
                         on qd.ProductId equals p.ProductId
                         join w in db.TblWareHouse
                          on qd.Whid equals w.Whid
                         where qd.QuotationId== QuotationId
                         select new TblQuotationDetailTemplate{
                     Id= qd.Id,
       QuotationId= qd.QuotationId,
                             ProductId= qd.ProductId,
                             Product=p,
                             Whid = qd.Whid,
      Quantity= qd.Quantity,
       PriceNet=qd.PriceNet,
        PriceVat=qd.PriceVat,
       PriceTot=qd.PriceTot,
       Discount=qd.Discount,
      DiscountNet =qd.DiscountNet,
     DiscountVat=qd.DiscountVat,
     DiscountTot=qd.DiscountTot,
    TotalNet=qd.TotalNet,
     TotalVat=qd.TotalVat,
      TotalTot=qd.TotalTot,
        Remark=qd.Remark,
       CalType=qd.CalType,
       WareHouse= w

                         }).ToList();
            return query;
        }
        bool IModule<TblQuotationDetailTemplate, int>.IsExist(int id) => throw new NotImplementedException();

        public void Set(TblQuotationDetailTemplate ob)
        {
            if (ob.Id <= 0)
            { db.TblQuotationDetailTemplate.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }


        public void Set(List<TblQuotationDetailTemplate> obList)
        {
            foreach (TblQuotationDetailTemplate ob in obList)
            {
                if (ob.Id <= 0)
                { db.TblQuotationDetailTemplate.Add(ob); }
                else { db.Entry(ob).State = EntityState.Modified; }
            }
        }

        TblQuotationDetailTemplate IModule<TblQuotationDetailTemplate, int>.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

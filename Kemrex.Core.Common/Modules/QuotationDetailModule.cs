﻿using Kemrex.Core.Common.Interfaces;
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

        public TblQuotationDetail Get(int QuationId = 0,int ProductId = 0, int WhId= 0)
        {
            return db.TblQuotationDetail
                .Where(x => x.QuotationId == QuationId && x.ProductId== ProductId && x.WHId== WhId)
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
                         join w in db.TblWareHouse
                          on qd.WHId equals w.Whid
                         where qd.QuotationId== QuotationId
                         select new TblQuotationDetail{
                     Id= qd.Id,
       QuotationId= qd.QuotationId,
                             ProductId= qd.ProductId,
                             Product=p,
       WHId = qd.WHId,
      Quantity= qd.Quantity,
                             PriceUnit = qd.PriceUnit,
                             PriceNet =qd.PriceNet,
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
        bool IModule<TblQuotationDetail, int>.IsExist(int id) => throw new NotImplementedException();

        public void Set(TblQuotationDetail ob)
        {
            if (ob.Id <= 0)
            { db.TblQuotationDetail.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }


        public void Set(List<TblQuotationDetail> obList)
        {
            foreach (TblQuotationDetail ob in obList)
            {
                if (ob.Id <= 0)
                { db.TblQuotationDetail.Add(ob); }
                else { db.Entry(ob).State = EntityState.Modified; }
            }
        }

        TblQuotationDetail IModule<TblQuotationDetail, int>.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

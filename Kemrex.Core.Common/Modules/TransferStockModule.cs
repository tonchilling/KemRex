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
   public class TransferStockModule 
    {
        private readonly mainContext db;
        public TransferStockModule(mainContext context)
        {
            db = context;
        }
        public void Delete(TransferStockHeader ob)
        {
            if (IsExist(ob.TransferNo))
            { db.TransferStockHeader.Remove(ob); }
        }
        public void DeleteDetail(TransferStockDetail ob)
        {
            if (IsExist(ob.TransferStockId))
            { db.TransferStockDetail.Remove(ob); }
        }
        public int Count(int groupId = 0, string TransferType = "")
        {
            var data = db.TransferStockHeader.Where(o=>o.TransferType== TransferType).AsQueryable();
            return data.Count();
        }
       

        public string GetLastId(string pre)
        {
            return (from q in db.TransferStockHeader
                    .Where(n=>n.TransferNo.Contains(pre))
                    select q.TransferNo).Max();
        }
        public TransferStockHeader Get(int id)
        {
          
                
                
               TransferStockHeader transferStockHeader= db.TransferStockHeader
                   .Where(x => x.TransferStockId == id)
                   .FirstOrDefault() ?? new TransferStockHeader()
                   {
                       
                   };

            transferStockHeader.TransferStockDetail = (from q in db.TransferStockDetail.Include(x => x.Product)
                                 where q.TransferStockId == id
                                 select new TransferStockDetail
                                 {
                                     TransferStockId = q.TransferStockId,
                                     Seq = q.Seq,
                                     ProductId= q.ProductId,
                                     CurrentQty = q.CurrentQty,
                                     RequestQty = q.RequestQty,
                                     RequestUnit = q.RequestUnit,
                                     RequestUnitFactor = q.RequestUnitFactor,
                                     LastModified = q.LastModified,
                                     Product=db.TblProduct.Where(p=>p.ProductId== q.ProductId).FirstOrDefault()
      
    }).ToList();

            return transferStockHeader;


        }


        public TransferStockDetail GetDetail(int id = 0)
        {
           // var detail = (from d in db.TransferDetail.Where(x => x.TransferId == id) select d);

          //  if (detail == null)
              //  detail = new TransferDetail();

            return new TransferStockDetail();
        }


        public TransferStockDetail GetDetail(int transferStockId, int seq)
        {
             var detail = (from d in db.TransferStockDetail.Where(x => x.TransferStockId == transferStockId && x.Seq== seq) select d);

            //  if (detail == null)
            //  detail = new TransferDetail();

            return detail.FirstOrDefault();
        }

        public int GetDetails(int id)
        {

            return (from detail in db.TransferStockDetail.Where(x => x.TransferStockId == id) select detail.ProductId).Count();
            //return db.TransferDetail
              //  .Where(x => x.TransferId == id).ToList();
        }



        public List<TransferStockHeader> Gets(int page = 1, int size = 0, string src = "",string TransferType="")
        {
            var data = db.TransferStockHeader.Where(o=>o.TransferType== TransferType)
                        .OrderByDescending(c => c.TransferNo)
                .AsQueryable();

          //  if (month > 0) { data = data.Where(x => x.TransferDate.Value.Month == month); }

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public bool IsExist(string id)
        {
             return db.TransferStockHeader.Where(x => x.TransferNo == id).Count() > 0 ? true : false; 
        }

        public bool IsExist(int id)
        {
            return db.TransferStockHeader.Where(x => x.TransferStockId == id).Count() > 0 ? true : false;
        }

        public void Set(TransferStockHeader ob)
        {
            if (ob.TransferStockId == 0)
            { db.TransferStockHeader.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }

        public void SetDetail(TransferStockDetail ob)
        {
            db.TransferStockDetail.Add(ob);
            /* if (ob.TransferId == 0)
             { db.TransferDetail.Add(ob); }
             else { db.Entry(ob).State = EntityState.Modified; }*/
        }
    }
}

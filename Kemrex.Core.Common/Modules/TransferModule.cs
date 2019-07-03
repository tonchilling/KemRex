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
   public class TransferModule 
    {
        private readonly mainContext db;
        public TransferModule(mainContext context)
        {
            db = context;
        }
        public void Delete(TransferHeader ob)
        {
            if (IsExist(ob.TransferNo))
            { db.TransferHeader.Remove(ob); }
        }
        public void DeleteDetail(TransferDetail ob)
        {
            if (IsExist(ob.TransferId))
            { db.TransferDetail.Remove(ob); }
        }
        public int Count(int groupId = 0, string TransferType = "")
        {
            var data = db.TransferHeader.Where(o=>o.TransferType== TransferType).AsQueryable();
            return data.Count();
        }
       

        public string GetLastId(string pre)
        {
            return (from q in db.TransferHeader
                    .Where(n=>n.TransferNo.Contains(pre))
                    select q.TransferNo).Max();
        }
        public TransferHeader Get(int id)
        {
          
                
                
               TransferHeader transferHeader= db.TransferHeader
                   .Where(x => x.TransferId == id)
                   .FirstOrDefault() ?? new TransferHeader()
                   {
                       
                   };

            transferHeader.TransferDetail = (from q in db.TransferDetail.Include(x => x.Product)
                                 where q.TransferId == id
                                 select new TransferDetail
                                 {
                                     TransferId=q.TransferId,
                                     Seq = q.Seq,
                                   ProductId= q.ProductId,
                                     CurrentQty = q.CurrentQty,
                                     RequestQty = q.RequestQty,
                                     RequestUnit = q.RequestUnit,
                                     RequestUnitFactor = q.RequestUnitFactor,
                                     LastModified = q.LastModified,
                                     Product=db.TblProduct.Where(p=>p.ProductId== q.ProductId).FirstOrDefault()
      
    }).ToList();

            return transferHeader;


        }


        public TransferDetail GetDetail(int id = 0)
        {
           // var detail = (from d in db.TransferDetail.Where(x => x.TransferId == id) select d);

          //  if (detail == null)
              //  detail = new TransferDetail();

            return new TransferDetail();
        }


        public TransferDetail GetDetail(int transferId,int seq)
        {
             var detail = (from d in db.TransferDetail.Where(x => x.TransferId == transferId && x.Seq== seq) select d);

            //  if (detail == null)
            //  detail = new TransferDetail();

            return detail.FirstOrDefault();
        }

        public int GetDetails(int id)
        {

            return (from detail in db.TransferDetail.Where(x => x.TransferId == id) select detail.ProductId).Count();
            //return db.TransferDetail
              //  .Where(x => x.TransferId == id).ToList();
        }



        public List<TransferHeader> Gets(int page = 1, int size = 0, string src = "",string TransferType="")
        {
            var data = db.TransferHeader.Where(o=>o.TransferType== TransferType)
                        .OrderByDescending(c => c.TransferNo)
                .AsQueryable();

          //  if (month > 0) { data = data.Where(x => x.TransferDate.Value.Month == month); }

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public bool IsExist(string id)
        {
             return db.TransferHeader.Where(x => x.TransferNo == id).Count() > 0 ? true : false; 
        }

        public bool IsExist(int id)
        {
            return db.TransferHeader.Where(x => x.TransferId == id).Count() > 0 ? true : false;
        }

        public void Set(TransferHeader ob)
        {
            if (ob.TransferId == 0)
            { db.TransferHeader.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }

        public void SetDetail(TransferDetail ob)
        {
            db.TransferDetail.Add(ob);
            /* if (ob.TransferId == 0)
             { db.TransferDetail.Add(ob); }
             else { db.Entry(ob).State = EntityState.Modified; }*/
        }
    }
}

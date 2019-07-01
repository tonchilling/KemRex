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
        public int Count(int groupId = 0, string src = "")
        {
            var data = db.TransferHeader.AsQueryable();
            return data.Count();
        }
        public string GetLastId(string pre)
        {
            return (from q in db.TransferHeader
                    .Where(n=>n.TransferNo.Contains(pre))
                    select q.TransferNo).Max();
        }
        public TransferHeader Get(string id)
        {
          
                
                
               TransferHeader transferHeader= db.TransferHeader
                   .Where(x => x.TransferNo == id)
                   .FirstOrDefault() ?? new TransferHeader()
                   {
                       
                   };

            transferHeader.TransferDetail = (from q in db.TransferDetail.Include(x => x.Product)
                                 where q.TransferNo == id
                                 select new TransferDetail
                                 {
                                    TransferNo=q.TransferNo,
                                     Seq = q.Seq,
                                   ProductId= q.ProductId,
                                     CurrentQty = q.CurrentQty,
                                     RequestQty = q.RequestQty,
                                     RequestUnit = q.RequestUnit,
                                     RequestUnitFactor = q.RequestUnitFactor,
                                     LastModified = q.LastModified
      
    }).ToList();

            return transferHeader;


        }

     

        public List<TransferHeader> Gets(int page = 1, int size = 0, int month = 0, string src = "")
        {
            var data = db.TransferHeader
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

        public void Set(TransferHeader ob)
        {
            if (ob.TransferNo !="")
            { db.TransferHeader.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}
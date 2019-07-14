﻿using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kemrex.Core.Common.Helper;

namespace Kemrex.Core.Common.Modules
{
    public class InvoiceModule : IModule<TblInvoice, int>
    {
        private readonly mainContext db;
        public InvoiceModule(mainContext context)
        {
            db = context;
        }

        public int Count(int groupId = 0, string src = "")
        {
            var data = db.TblInvoice.AsQueryable();
            return data.Count();
        }

        public void Delete(TblInvoice ob)
        {
            throw new NotImplementedException();
        }

        public TblInvoice Get(int id)
        {
            return db.TblInvoice.Where(i => i.InvoiceId == id)
                .FirstOrDefault() ?? new TblInvoice()
                {
                    CreatedDate = DateTime.Now,
                    InvoiceDate = DateTime.Now,
                    DueDate = DateTime.Now.AddMonths(1)
                };
        }
        public List<TblInvoice> Gets(int page = 1, int size = 0, string src = "")
        {
            //var data = db.TblInvoice
            //            .OrderByDescending(c => c.InvoiceId)
            //    .AsQueryable();

            var data = db.TblInvoice.Include(c => c.SaleOrder)    
            .OrderByDescending(c => c.InvoiceId)
             .AsQueryable();

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public List<TblInvoice> GetList()
        {
            var data = (from i in db.TblInvoice
                        select new TblInvoice
                        {
                            InvoiceId = i.InvoiceId,
                            InvoiceNo = i.InvoiceNo,
                            InvoiceDate = i.InvoiceDate,
                            StrInvoiceDate = i.InvoiceDate != null ? Converting.ToDDMMYYYY(i.InvoiceDate) : "",
                            SaleOrderId = i.SaleOrderId,
                            InvoiceRemark = i.InvoiceRemark,
                            InvoiceTerm = i.InvoiceTerm,
                            InvoiceAmount = i.InvoiceAmount,
                            StatusId = i.StatusId,
                            CreatedBy = i.CreatedBy,
                            CreatedDate = i.CreatedDate,
                            UpdatedBy = i.UpdatedBy,
                            UpdateDate = i.UpdateDate,
                            DueDate = i.DueDate,
                            DepositAmount = i.DepositAmount,
                            SaleOrder = null
                        });
            return data.ToList();
        }
        public decimal GetRemain(int SaleOrderId)
        {
            var data = (from i in db.TblInvoice
                    .Where(i => i.SaleOrderId == SaleOrderId)
                    select new TblInvoice
                    {
                        InvoiceId = i.InvoiceId,
                        InvoiceNo = i.InvoiceNo,
                        SaleOrderId = i.SaleOrderId, 
                        InvoiceAmount = i.InvoiceAmount,
                        SaleOrder = null
                    });
            decimal remain = 0;
            foreach (var x in data.ToList())
            {
                remain += x.InvoiceAmount.HasValue ? x.InvoiceAmount.Value : 0;
            }


            return remain;
        }

        public string GetLastId(string pre)
        {
            return (from q in db.TblInvoice
                    .Where(n => n.InvoiceNo.Contains(pre))
                    select q.InvoiceNo).Max();
        }

        public bool IsExist(int id)
        {
            return db.TblInvoice.Where(x => x.InvoiceId == id).Count() > 0 ? true : false;
        }

        public void Set(TblInvoice ob)
        {
            if (ob.InvoiceId <= 0)
            { db.TblInvoice.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

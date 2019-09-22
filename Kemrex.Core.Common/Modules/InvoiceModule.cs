using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kemrex.Core.Common.Helper;
using System.Data.SqlClient;
using System.Data;

namespace Kemrex.Core.Common.Modules
{
    public class InvoiceModule : IModule<TblInvoice, int>
    {
        private readonly mainContext db;
        private WebDB webdb;
        public InvoiceModule(mainContext context)
        {
            db = context;
            webdb = new WebDB();
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
        public List<TblInvoice> GetList(long userId)
        {
            var data = (from i in db.TblInvoice.Where(o => o.CreatedBy == userId || userId == 0)
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
                            UpdatedDate = i.UpdatedDate,
                            DueDate = i.DueDate,
                            DepositAmount = i.DepositAmount,
                            IsDeposit = i.IsDeposit,
                            SaleOrder = null,
                            ConditionId = i.ConditionId,
                            CreatedByName = (from emp in db.SysAccount.Where(acc => acc.AccountId ==i.CreatedBy) select emp.AccountName).FirstOrDefault()
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
        public decimal GetRemain(int SaleOrderId, int exceptTerm)
        {
            var data = (from i in db.TblInvoice
                    .Where(i => i.SaleOrderId == SaleOrderId && i.InvoiceTerm != exceptTerm)
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
        public List<TblInvoice> GetHistoryInvoiceAmount(int SaleOrderId)
        {
            var data = (from i in db.TblInvoice.Where(o => o.SaleOrderId == SaleOrderId)
                        select new TblInvoice
                        {
                            InvoiceTerm = i.InvoiceTerm,
                            InvoiceAmount = i.InvoiceAmount,
                            StatusId = i.StatusId
                        }).OrderBy(c => c.InvoiceTerm);
            return data.ToList();
        }
        public string GetLastId(string pre)
        {
            return (from q in db.TblInvoice
                    .Where(n => n.InvoiceNo.Contains(pre))
                    select q.InvoiceNo).Max();
        }
        public int GetInsertId(string pre)
        {
            return (from q in db.TblInvoice
                    .Where(n => n.InvoiceNo.Contains(pre))
                    select q.InvoiceId).Max();
        }

        public bool IsExist(int id)
        {
            return db.TblInvoice.Where(x => x.InvoiceId == id).Count() > 0 ? true : false;
        }
        public int CountTerm(int id = 0)
        {
            var data = db.TblInvoice.Where(x => x.SaleOrderId == id);
            return data.Count();
        }

        public void Set(TblInvoice ob)
        {
            if (ob.InvoiceId <= 0)
            {
                db.TblInvoice.Add(ob);
            }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
        public bool Add(TblInvoice dto)
        {
            bool result = false;
            string sql = "sp_Invoice_Insert";


            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@InvoiceNo", dto.InvoiceNo)); 
            paramList.Add(new SqlParameter("@SaleOrderId", dto.SaleOrderId));
            paramList.Add(new SqlParameter("@InvoiceRemark", dto.InvoiceRemark));
            paramList.Add(new SqlParameter("@InvoiceTerm", dto.InvoiceTerm));
            paramList.Add(new SqlParameter("@InvoiceAmount", dto.InvoiceAmount));
            paramList.Add(new SqlParameter("@ConditionId", dto.ConditionId));
            paramList.Add(new SqlParameter("@User", dto.CreatedBy));
            
            try
            {

                result = webdb.ExcecuteNonQuery(sql, paramList);
                
            }
            catch (Exception ex)
            {
                throw new Exception("sp_Invoice_Insert::" + ex.ToString());
            }
            finally
            { }
            return result;
        }
    }
}

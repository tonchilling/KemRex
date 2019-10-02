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
    public class PaymentModule : IModule<TblPayment, int>
    {
        private readonly mainContext db;
        private WebDB webdb = new WebDB();
        public PaymentModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblPayment.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblPayment ob)
        {
            if (IsExist(ob.PaymentId))
            { db.TblPayment.Remove(ob); }
        }

        private IQueryable<TblPayment> Filter(IQueryable<TblPayment> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.CustomerName.Contains(src)); }
            return data;
        }

        public TblPayment Get(int id)
        {
            return db.TblPayment
                .Where(x => x.PaymentId == id)
                .FirstOrDefault() ?? new TblPayment()
                {
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    PaymentDate = DateTime.Now
                };
        }

        public List<TblPayment> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblPayment
                .AsQueryable();
            data = Filter(data, src)
                .OrderByDescending(x => x.PaymentDate);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblPayment.Where(x => x.PaymentId == id).Count() > 0 ? true : false; }

        public void Set(TblPayment ob)
        {
            if (ob.PaymentId == 0)
            { db.TblPayment.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
        public string GetLastId(string pre)
        {
            return (from q in db.TblPayment
                    .Where(n => n.PaymentNo.Contains(pre))
                    select q.PaymentNo).Max();
        }
        public List<TblPayment> GetList()
        {
            var data = db.TblPayment
                       .OrderByDescending(c => c.PaymentDate)
               .AsQueryable();
            return data.ToList();
        }
        public bool SetPayment(TblPayment dto)
        {
            bool result = false;
            string sql = "sp_Payment";

            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlParameter pvNewId = new SqlParameter("@PaymentId", SqlDbType.NVarChar, 50);
            pvNewId.Value = dto.PaymentId;
            pvNewId.Direction = ParameterDirection.InputOutput;


            paramList.Add(pvNewId);

            //paramList.Add(new SqlParameter("@PaymentId", dto.PaymentId));
            paramList.Add(new SqlParameter("@PaymentNo", dto.PaymentNo));
            paramList.Add(new SqlParameter("@PaymentDate", dto.StrPaymentDate));
            paramList.Add(new SqlParameter("@InvoiceId", dto.InvoiceId));
            paramList.Add(new SqlParameter("@InvoiceNo", dto.InvoiceNo));
            paramList.Add(new SqlParameter("@CustomerId", dto.CustomerId));
            paramList.Add(new SqlParameter("@CustomerName", dto.CustomerName));
            paramList.Add(new SqlParameter("@PaymentAmount", dto.PaymentAmount));
            paramList.Add(new SqlParameter("@StatusId", dto.StatusId));
            paramList.Add(new SqlParameter("@BankPayFrom", dto.BankPayFrom));
            paramList.Add(new SqlParameter("@BankPayFromBranch", dto.BankPayFromBranch));
            paramList.Add(new SqlParameter("@AcctReceiveId", dto.AcctReceiveId));
            paramList.Add(new SqlParameter("@PaySlipPath", dto.PaySlipPath));
            paramList.Add(new SqlParameter("@UpdatedBy", dto.UpdatedBy));
            paramList.Add(new SqlParameter("@CreatedBy", dto.CreatedBy));
            paramList.Add(new SqlParameter("@ApprovedBy", dto.ApprovedBy));

            try
            {
                result = webdb.ExcecuteWitTranNonQuery(sql, paramList);
                dto.PaymentId = Convert.ToInt32(pvNewId.Value.ToString());
                
            }
            catch (Exception ex)
            {
                throw new Exception("sp_Payment::" + ex.ToString());
            }
            finally
            { }
            return result;
        }
    }
}

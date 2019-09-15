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
using Kemrex.Core.Common.Helper;


namespace Kemrex.Core.Common.Modules
{
    public class QuotationTemplateModule : IModule<TblQuotationTemplate, int>
    {
        private WebDB webdb;
        private readonly mainContext db;
        public QuotationTemplateModule(mainContext context)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); ;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US"); ;
            db = context;
            webdb = new WebDB();
        }

        public int Count(int groupId = 0, string src = "")
        {
            var data = db.TblQuotation.AsQueryable();
            return data.Count();
        }
        public int GetId(string no)
        {
            return db.TblQuotation
                .Where(q => q.QuotationNo == no)
                .Select(q => q.QuotationId).First();
        }
        public TblQuotationTemplate Get(int id)
        {
            return db.TblQuotationTemplate
                .Where(x => x.QuotationId == id)
                .FirstOrDefault() ?? new TblQuotationTemplate()
                {
                    CreatedDate = DateTime.Now
                };
        }

        public TblQuotationTemplate GetDetail(int id)
        {
            return db.TblQuotationTemplate.Include(detail => detail.TblQuotationDetailTemplate)
                .Where(x => x.QuotationId == id)
                .FirstOrDefault() ?? new TblQuotationTemplate()
                {
                    CreatedDate = DateTime.Now
                };
        }


        public TblQuotationTemplate Get(string quotationNo)
        {
            return db.TblQuotationTemplate
                .Where(x => x.QuotationNo == quotationNo)
                .FirstOrDefault() ?? new TblQuotationTemplate()
                {
                    CreatedDate = DateTime.Now
                };
        }

        public string GetLastId()
        {
          //  return db.TblQuotation.OrderByDescending(x => x.QuotationId).Select(x => x.QuotationNo).First();

            return (from q in db.TblQuotation select q.QuotationNo).Max();
        }

        public List<TblQuotationTemplate> Gets(int page = 1, int size = 0, string src = "")
        {
            /* var data = db.TblQuotation.Include(c => c.Status)
                         .Include(c=>c.Customer)
                         .OrderByDescending(c=>c.QuotationId)
                 .AsQueryable();*/

            var data = (from q in db.TblQuotation.Include(c => c.Customer)
                         .OrderByDescending(c => c.QuotationId)
                        select new TblQuotationTemplate
                        {
                            QuotationId = q.QuotationId,
                            QuotationNo = q.QuotationNo,
                            QuotationDate = q.QuotationDate,
                            strQuotationDate = string.Format("{0}/{1}/{2}", q.QuotationDate.Day.ToString("##00"), q.QuotationDate.Month.ToString("##00"), q.QuotationDate.Year.ToString()),
                            QuotationValidDay = q.QuotationValidDay,
                            ConditionId = q.ConditionId,
                            OperationStartDate = q.OperationStartDate,
                            OperationEndDate = q.OperationEndDate,
                            DueDate = q.DueDate,
                            DeliveryDate = q.DeliveryDate,
                            QuotationCreditDay = q.QuotationCreditDay,
                            SaleId = q.SaleId,
                            SaleName = q.SaleName,
                            CustomerId = q.CustomerId,
                            CustomerName = q.CustomerName,

                            SubTotalNet = q.SubTotalNet,
                            SubTotalVat = q.SubTotalVat,
                            SubTotalTot = q.SubTotalTot,
                            DiscountNet = q.DiscountNet,
                            DiscountVat = q.DiscountVat,
                            DiscountTot = q.DiscountTot,
                            DiscountCash = q.DiscountCash,
                            SummaryNet = q.SummaryNet,
                            SummaryVat = q.SummaryVat,
                            SummaryTot = q.SummaryTot,
                            Status = q.Status,
                            StatusId = q.StatusId,
                            RefQuotationId = q.RefQuotationId,
                            OrgQuotationId = q.OrgQuotationId,
                          

                        })

                        .OrderByDescending(c => c.QuotationId)
                .AsQueryable();

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public List<TblQuotationTemplate> GetQuatationNotSale()
        {
            var quatations = (from q in db.TblSaleOrder select q.QuotationNo).ToList();
            var data = (from q in db.TblQuotation.Where(o => !quatations.Contains(o.QuotationNo)).Include(c => c.Status)
                        .Include(c => c.Customer) select new TblQuotationTemplate
                        {
                            QuotationId = q.QuotationId,
                            QuotationNo = q.QuotationNo,
                            QuotationDate = q.QuotationDate,
                            strQuotationDate = string.Format("{0}/{1}/{2}", q.QuotationDate.Day.ToString("##00"), q.QuotationDate.Month.ToString("##00"), q.QuotationDate.Year.ToString()),
                            QuotationValidDay = q.QuotationValidDay,
                            ConditionId = q.ConditionId,
                            OperationStartDate = q.OperationStartDate,
                            OperationEndDate = q.OperationEndDate,
                            DueDate = q.DueDate,
                            DeliveryDate = q.DeliveryDate,
                            QuotationCreditDay = q.QuotationCreditDay,
                            SaleId = q.SaleId,
                            SaleName = q.SaleName,
                            CustomerId = q.CustomerId,
                            CustomerName = q.CustomerName,

                            SubTotalNet = q.SubTotalNet,
                            SubTotalVat = q.SubTotalVat,
                            SubTotalTot = q.SubTotalTot,
                            DiscountNet = q.DiscountNet,
                            DiscountVat = q.DiscountVat,
                            DiscountTot = q.DiscountTot,
                            DiscountCash = q.DiscountCash,
                            SummaryNet = q.SummaryNet,
                            SummaryVat = q.SummaryVat,
                            SummaryTot = q.SummaryTot,
                            Status = q.Status,
                            StatusId = q.StatusId


                        })

                        .OrderByDescending(c => c.QuotationId)
                .AsQueryable();


            return data.ToList();
        }


        public List<TblQuotationDisplay> GetList(int id)
        {
            List<TblQuotationDisplay> result = new List<TblQuotationDisplay>();
            var data = (from q in db.TblQuotation.Where(o=>o.OrgQuotationId== id)
                        select q);

            foreach (TblQuotation qu in data)
            {
                result.Add(new TblQuotationDisplay { QuotationNo=qu.QuotationNo,QuotationId=qu.QuotationId,QuotationDate=qu.QuotationDate});
            }
            


            return result.OrderByDescending(o => o.QuotationDate).ToList();
        }

        public List<TblQuotationTemplate> GetList(long userId)
        {
            var data = (from order in db.TblQuotation.Where(o=>(o.CreatedBy== userId || userId==0) && o.StatusId != 9)
                        select new TblQuotationTemplate
                        {
                         QuotationId = order.QuotationId,
                            QuotationNo = order.QuotationNo,
       QuotationDate = order.QuotationDate,
       strQuotationDate=string.Format("{0}/{1}/{2}",order.QuotationDate.Day.ToString("##00"), order.QuotationDate.Month.ToString("##00"), order.QuotationDate.Year.ToString()),
       SubTotalNet = order.SubTotalNet,
      SubTotalVat = order.SubTotalVat,
       SubTotalTot = order.SubTotalTot,
      DiscountNet = order.DiscountNet,
       DiscountVat = order.DiscountVat,
        DiscountTot = order.DiscountTot,
       DiscountCash = order.DiscountCash,
       SummaryNet = order.SummaryNet,
        SummaryVat = order.SummaryVat,
        SummaryTot = order.SummaryTot,
        StatusId=order.StatusId,
                            CreatedByName = (from emp in db.SysAccount.Where(o => o.AccountId == order.CreatedBy) select emp.AccountName).FirstOrDefault(),
        Status = (from emp in db.EnmStatusQuotation.Where(o => o.StatusId == order.StatusId)
                 select new EnmStatusQuotation
                 {
                     StatusId = emp.StatusId,
                      StatusName = emp.StatusName,
                 }).FirstOrDefault(),
                            Customer = (from emp in db.TblCustomer.Where(o => o.CustomerId == order.CustomerId)
                    select new TblCustomer
                    {
                        CustomerId = emp.CustomerId,
                        CustomerName = emp.CustomerName
                    }).FirstOrDefault(),
        Sale = (from emp in db.TblEmployee.Where(o=>o.EmpId==order.SaleId)
                select new TblEmployee {
                    EmpId=emp.EmpId,
                    EmpCode=emp.EmpCode,
                    EmpNameTh=emp.EmpNameTh
                }).FirstOrDefault(),

                            RefQuotationId = order.RefQuotationId,
                            OrgQuotationId = order.OrgQuotationId
                        
                        });
            return data.OrderByDescending(o=>o.QuotationDate).ToList();
        }



        public string GetStatus(int id)
        {
            return db.EnmStatusQuotation
                 .Where(x => x.StatusId == id)
                 .Select(q => q.StatusName).ToString();
        }


        public void Delete(TblQuotationTemplate ob)
        {
            if (IsExist(ob.QuotationId))
            { db.TblQuotationTemplate.Remove(ob); }
        }

        public bool IsExist(int id)
        { return db.TblQuotation.Where(x => x.QuotationId == id).Count() > 0 ? true : false; }

        TblQuotationTemplate IModule<TblQuotationTemplate, int>.Get(int id)
        {
            throw new NotImplementedException();
        }

        bool IModule<TblQuotationTemplate, int>.IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(TblQuotationTemplate ob)
        {
            if (ob.QuotationId <= 0)
            { db.TblQuotationTemplate.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }


        public void SetVersion(TblQuotationTemplate ob)
        {
            string sql = "sp_TblQuotation_Version_Add";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<TblSaleOrder> list = new List<TblSaleOrder>();
            TblSaleOrder dto = null;
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            int result = 0;

            try
            {
                webdb.OpenConnection();
                paramList.Add(new SqlParameter("@QuotationId", ob.QuotationId));
                paramList.Add(new SqlParameter("@TempQuotationId", ob.TempQuotationId));
                //connect.Open();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = webdb.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                result = sqlCommand.ExecuteNonQuery();
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (webdb.Connection.State == ConnectionState.Open)
                {
                    webdb.CloseConnection();
                }
            }

        }
    }
}

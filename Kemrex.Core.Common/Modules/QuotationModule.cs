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
    public class QuotationModule : IModule<TblQuotation, int>
    {
        private readonly mainContext db;
        public QuotationModule(mainContext context)
        {
            db = context;
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
        public TblQuotation Get(int id)
        {
            return db.TblQuotation
                .Where(x => x.QuotationId == id)
                .FirstOrDefault() ?? new TblQuotation()
                {
                    CreatedDate = DateTime.Now
                };
        }

        public TblQuotation GetDetail(int id)
        {
            return db.TblQuotation.Include(detail => detail.TblQuotationDetail)
                .Where(x => x.QuotationId == id)
                .FirstOrDefault() ?? new TblQuotation()
                {
                    CreatedDate = DateTime.Now
                };
        }


        public TblQuotation Get(string quotationNo)
        {
            return db.TblQuotation
                .Where(x => x.QuotationNo == quotationNo)
                .FirstOrDefault() ?? new TblQuotation()
                {
                    CreatedDate = DateTime.Now
                };
        }

        public string GetLastId()
        {
          //  return db.TblQuotation.OrderByDescending(x => x.QuotationId).Select(x => x.QuotationNo).First();

            return (from q in db.TblQuotation select q.QuotationNo).Max();
        }

        public List<TblQuotation> Gets(int page = 1, int size = 0, string src = "")
        {
            /* var data = db.TblQuotation.Include(c => c.Status)
                         .Include(c=>c.Customer)
                         .OrderByDescending(c=>c.QuotationId)
                 .AsQueryable();*/

            var data = (from q in db.TblQuotation.Include(c => c.Customer)
                         .OrderByDescending(c => c.QuotationId)
                        select new TblQuotation
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
                            TblOrgQuotation = (from qq in db.TblQuotation.Where(qq => qq.QuotationId == q.OrgQuotationId) select qq).FirstOrDefault(),
                            TblRefQuotation = (from qq in db.TblQuotation.Where(qq => qq.QuotationId == q.RefQuotationId) select qq).FirstOrDefault(),
                            TblOtherQuotation = (from qq in db.TblQuotation.Where(qq => qq.OrgQuotationId == q.OrgQuotationId) select qq).ToList()

                        })

                        .OrderByDescending(c => c.QuotationId)
                .AsQueryable();

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public List<TblQuotation> GetQuatationNotSale()
        {
            var quatations = (from q in db.TblSaleOrder select q.QuotationNo).ToList();
            var data = (from q in db.TblQuotation.Where(o => !quatations.Contains(o.QuotationNo)).Include(c => c.Status)
                        .Include(c => c.Customer) select new TblQuotation {
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

        public List<TblQuotation> GetList(long userId)
        {
            var data = (from order in db.TblQuotation.Where(o=>(o.CreatedBy== userId || userId==0) && o.StatusId != 9)
                        select new TblQuotation
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

        public List<TblQuotation> GetList2(long userId)
        {
            var permissionList = db.TblEmployeeUserPermission.Where(o=> (o.ViewEmpId== userId && o.FunId==1) ).Count()>0 
                ? db.TblEmployeeUserPermission.Where(o => o.ViewEmpId == userId && o.FunId == 1).Select(o=>o.EmpId).ToList():null;


            if (permissionList != null)
            {
                permissionList.Add((int)userId);
            }
            else {
                permissionList = new List<int>();

                permissionList.Add((int)userId);
            }
            var data = (from order in db.TblQuotation.Where(o => (permissionList.Contains((int)o.CreatedBy) || userId==0) && o.StatusId != 9)
                        select new TblQuotation
                        {
                            QuotationId = order.QuotationId,
                            QuotationNo = order.QuotationNo,
                            QuotationDate = order.QuotationDate,
                            strQuotationDate = string.Format("{0}/{1}/{2}", order.QuotationDate.Day.ToString("##00"), order.QuotationDate.Month.ToString("##00"), order.QuotationDate.Year.ToString()),
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
                            StatusId = order.StatusId,
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
                            Sale = (from emp in db.TblEmployee.Where(o => o.EmpId == order.SaleId)
                                    select new TblEmployee
                                    {
                                        EmpId = emp.EmpId,
                                        EmpCode = emp.EmpCode,
                                        EmpNameTh = emp.EmpNameTh
                                    }).FirstOrDefault(),

                            RefQuotationId = order.RefQuotationId,
                            OrgQuotationId = order.OrgQuotationId

                        });
            return data.OrderByDescending(o => o.QuotationDate).ToList();
        }


        public string GetStatus(int id)
        {
            return db.EnmStatusQuotation
                 .Where(x => x.StatusId == id)
                 .Select(q => q.StatusName).ToString();
        }


        public void Delete(TblQuotation ob)
        {
            if (IsExist(ob.QuotationId))
            { db.TblQuotation.Remove(ob); }
        }

        public bool IsExist(int id)
        { return db.TblQuotation.Where(x => x.QuotationId == id).Count() > 0 ? true : false; }

        TblQuotation IModule<TblQuotation, int>.Get(int id)
        {
            throw new NotImplementedException();
        }

        bool IModule<TblQuotation, int>.IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(TblQuotation ob)
        {
            if (ob.QuotationId <= 0)
            { db.TblQuotation.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }

       
    }
}

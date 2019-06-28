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
   public class SaleOrderModule : IModule<TblSaleOrder, int>
    {
        private readonly mainContext db;
        public SaleOrderModule(mainContext context)
        {
            db = context;
        }
        public void Delete(TblSaleOrder ob)
        {
            if (IsExist(ob.SaleOrderId))
            { db.TblSaleOrder.Remove(ob); }
        }
        public int Count(int groupId = 0, string src = "")
        {
            var data = db.TblSaleOrder.AsQueryable();
            return data.Count();
        }
        public string GetLastId(string pre)
        {
            return (from q in db.TblSaleOrder
                    .Where(n=>n.SaleOrderNo.Contains(pre))
                    select q.SaleOrderNo).Max();
        }
        public TblSaleOrder Get(int id)
        {
            return db.TblSaleOrder
                   .Where(x => x.SaleOrderId == id)
                   .FirstOrDefault() ?? new TblSaleOrder()
                   {
                       CreatedDate = DateTime.Now
                   };
        }

        public TblSaleOrder GetFull(int id)
        {
            TblSaleOrder tblSaleOrder= db.TblSaleOrder
                   .Where(x => x.SaleOrderId == id)
                   .FirstOrDefault() ?? new TblSaleOrder()
                   {
                       CreatedDate = DateTime.Now
                   };
            tblSaleOrder.CreatedDateToString = Converting.ToDDMMYYYY(tblSaleOrder.CreatedDate);
            tblSaleOrder.DeliveryDateToString = Converting.ToDDMMYYYY(tblSaleOrder.DeliveryDate);
            //var customerID = Convert.ToInt32(tblSaleOrder.CustomerId);
            ////tblSaleOrder.Customers = new List<TblCustomer>();
            //var customerDto = db.TblCustomer.Where(cus => cus.CustomerId == customerID).FirstOrDefault() ?? new TblCustomer()
            //{
            //    CreatedDate = DateTime.Now
            //};

            tblSaleOrder.JobOrder = (from jobOrder in db.TblJobOrder
                                        where jobOrder.SaleOrderId == tblSaleOrder.SaleOrderId
                                        select new TblJobOrder
                                        {
                                            JobOrderId = jobOrder.JobOrderId,
                                            JobOrderNo = jobOrder.JobOrderNo,
                                            SaleOrderId=jobOrder.SaleOrderId,
                                            CreateDate=jobOrder.CreateDate,
                                            CreateDateToString = Converting.ToDDMMYYYY(jobOrder.CreateDate),
                                            JobName = jobOrder.JobName,
                                            StartDate= jobOrder.StartDate,
                                            StartDateToString= Converting.ToDDMMYYYY(jobOrder.StartDate),
                                           EndDate = jobOrder.EndDate,
                                           EndDateToString = Converting.ToDDMMYYYY(jobOrder.EndDate),
                                            StartWorkingTime = jobOrder.StartWorkingTime,
                                            EndWorkingTime = jobOrder.EndWorkingTime,
                                            CustomerId = jobOrder.CustomerId,
                                             CustomerName = jobOrder.CustomerName,
                                              CustomerPhone = jobOrder.CustomerPhone,
                                            CustomerEmail = jobOrder.CustomerEmail,
                                              ProjectName = jobOrder.ProjectName,
                                            ProductId = jobOrder.ProductId,
                                              ProductQty = jobOrder.ProductQty,
                                            ProductWeight = jobOrder.ProductWeight,
                                            ProductSaftyFactory = jobOrder.ProductSaftyFactory,
                                            Adapter = jobOrder.Adapter,
                                            Other = jobOrder.Other,
                                            HouseNo = jobOrder.HouseNo,
                                            VillageNo = jobOrder.VillageNo,
                                            SubDistrictId = jobOrder.SubDistrictId,
                                            Reason = jobOrder.Reason,
                                            Solution = jobOrder.Solution,
                                            TeamId = jobOrder.TeamId,
                                             Team = jobOrder.Team

                                        }).FirstOrDefault();

            tblSaleOrder.Customer =  (from customer in db.TblCustomer where customer.CustomerId == tblSaleOrder.CustomerId select customer).FirstOrDefault();
            tblSaleOrder.TblSaleOrderDetail = (from q in db.TblSaleOrderDetail.Include(x => x.Product)
                                               where q.SaleOrderId == id select new TblSaleOrderDetail
            {
                Id = q.Id,
        SaleOrderId = q.SaleOrderId,
                ProductId = q.ProductId,
                Quantity = q.Quantity,
                PriceNet = q.PriceNet,
                PriceVat = q.PriceVat,
                PriceTot = q.PriceTot,
                DiscountNet = q.DiscountNet,
                DiscountVat = q.DiscountVat,
                DiscountTot = q.DiscountTot,
                TotalNet = q.TotalNet,
                TotalVat = q.TotalVat,
                TotalTot = q.TotalTot,
                Remark = q.Remark,
                Product=q.Product
            }).ToList();

            tblSaleOrder.Sale = (from sale in db.TblEmployee where sale.EmpId == tblSaleOrder.SaleId select new TblEmployee {
                EmpId = sale.EmpId ,
                EmpCode = sale.EmpCode,
                AccountId = sale.AccountId,
                PrefixId = sale.PrefixId ,
                EmpTypeId = sale.EmpTypeId ,
                EmpNameTh = sale.EmpNameTh ,
                EmpNameEn = sale.EmpNameEn ,
                EmpPid = sale.EmpPid ,
                EmpMobile = sale.EmpMobile ,
                EmpEmail = sale.EmpEmail ,
                DepartmentId = sale.DepartmentId ,
                PositionId = sale.PositionId ,
                LeadId = sale.LeadId ,
                EmpApplyDate = sale.EmpApplyDate ,
                EmpPromoteDate = sale.EmpPromoteDate ,
                EmpAddress = sale.EmpAddress ,
                EmpPostcode = sale.EmpPostcode ,
                EmpSignature = sale.EmpSignature ,
                EmpRemark = sale.EmpRemark ,
                StatusId = sale.StatusId ,
                CreatedBy = sale.CreatedBy ,
                CreatedDate = sale.CreatedDate ,
                UpdatedBy = sale.UpdatedBy ,
                UpdatedDate = sale.UpdatedDate 
    }).FirstOrDefault(); 

            tblSaleOrder.TblSaleOrderAttachment = (from q in db.TblSaleOrderAttachment where q.SaleOrderId == id select new TblSaleOrderAttachment {

                Id = q.Id,
                SaleOrderId = q.SaleOrderId,
                AttachmentPath = q.AttachmentPath,
                AttachmentOrder = q.AttachmentOrder,
                AttachmentRemark = q.AttachmentRemark
               
            }).ToList();


            return tblSaleOrder;
        }

        public List<TblSaleOrder> Gets(int page = 1, int size = 0, int month = 0, string src = "")
        {
            var data = (from order in db.TblSaleOrder select new TblSaleOrder {
                SaleOrderId = order.SaleOrderId,
                SaleOrderNo = order.SaleOrderNo,
                SaleOrderDate = order.SaleOrderDate,
                OperationStartDate = order.OperationStartDate,
                OperationEndDate = order.OperationEndDate,
                QuotationNo = order.QuotationNo,
                CustomerId = order.CustomerId,
                CustomerName = order.CustomerName,
                ContractName = order.ContractName,
                ConditionId = order.ConditionId,
                PoNo = order.PoNo,
                SaleId = order.SaleId,
                SaleName = order.SaleName,
                TeamId = order.TeamId,
                BillingAddress = order.BillingAddress,
                ShippingAddress = order.ShippingAddress,
                SaleOrderRemark = order.SaleOrderRemark,
                StatusId = order.StatusId,
                CreatedBy = order.CreatedBy,
                CreatedDate = order.CreatedDate,
                
                CreatedDateToString = order.CreatedDateToString,
                UpdatedBy = order.UpdatedBy,
                UpdateDate = order.UpdateDate,
                CancelReason = order.CancelReason,
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
                DeliveryDate = order.DeliveryDate,
                DeliveryDateToString = order.DeliveryDateToString,
                SaleOrderCreditDay = order.SaleOrderCreditDay,
                HasJob = db.TblJobOrder.Where(o=>o.SaleOrderId==order.SaleOrderId).Count()

            })
                        .OrderByDescending(c => c.SaleOrderId).AsQueryable();
                

            if (month > 0) { data = data.Where(x => x.SaleOrderDate.Value.Month == month); }

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public List<TblSaleOrder> GetFindByCondition(string fromDate="", string toDate="")
        {
            var data = db.TblSaleOrder
                        .OrderByDescending(c => c.SaleOrderId)
                .AsQueryable();
            DateTime stDate, enDate;

            var saleOrderId = db.TblJobOrder.Select(x => x.SaleOrderId).ToArray();
            if (fromDate != "")
            {
                stDate = DateTime.ParseExact("dd/MM/yyyy", fromDate, null);
                enDate = DateTime.ParseExact("dd/MM/yyyy", toDate, null);

                data = data.Where(x => !saleOrderId.Contains(x.SaleOrderId) && (x.SaleOrderDate.Value >= stDate && x.SaleOrderDate.Value <= enDate));

            }
            else {
                data = data.Where(x => !saleOrderId.Contains(x.SaleOrderId));
            }

            return data.ToList();
        }

        public bool IsExist(int id)
        {
             return db.TblSaleOrder.Where(x => x.SaleOrderId == id).Count() > 0 ? true : false; 
        }

        public void Set(TblSaleOrder ob)
        {
            if (ob.SaleOrderId <= 0)
            { db.TblSaleOrder.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

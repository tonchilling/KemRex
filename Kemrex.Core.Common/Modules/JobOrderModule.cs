using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Kemrex.Core.Common.Helper;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class JobOrderModule : IModule<TblJobOrder, int>
    {
        private readonly mainContext db;
        public JobOrderModule(mainContext context)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); ;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US"); ;
            db = context;
        }

        public int Count(string src = "", int managerId = 0)
        {
            var data = db.TblJobOrder.AsQueryable();
            data = Filter(data, src, managerId);
            return data.Count();
        }

        public string GetLastId(string containJob)
        {
            string jobOrderId = containJob+"-0000";
            var obj = db.TblJobOrder.Where(o => (o.JobOrderNo != "" ? o.JobOrderNo : containJob).Substring(0, 7) == containJob);

            if (obj.Count() > 0)
            {
                jobOrderId = obj.OrderByDescending(x => x.JobOrderId).Select(x => x.JobOrderNo).First();
            }
            return jobOrderId; //db.TblJobOrder.Where(o=>(o.JobOrderNo!="" ? o.JobOrderNo: containJob).Substring(0,7)== containJob).OrderByDescending(x => x.JobOrderId).Select(x => x.JobOrderNo).First();
        }

        public void Delete(TblJobOrder ob)
        {
            if (IsExist(ob.JobOrderId))
            { db.TblJobOrder.Remove(ob); }
        }

        public void DeleteDetail(TblJobOrderDetail ob)
        {
            if (IsExist(ob.JobOrderId))
            { db.TblJobOrderDetail.Remove(ob); }
        }

        public void DeletePropertie(TblJobOrderProperties ob)
        {
            if (IsExist(ob.JobOrderId))
            { db.TblJobOrderProperties.Remove(ob); }
        }

        private IQueryable<TblJobOrder> Filter(IQueryable<TblJobOrder> data, string src = "", int JobOrderId = 0)
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.JobName.Contains(src)); }
            if(JobOrderId > 0)
            { data = data.Where(x => x.JobOrderId == JobOrderId); }
            return data;
        }

        public List<TblJobOrder> GetByDate(string fromDate,string toDate)
        {
            List<TblJobOrder> jobOrder = (from q in db.TblJobOrder select q).ToList();

            return jobOrder;
        }


        public List<TblSaleOrderDetail> GetOrderDetail(int saleOrderId)
        {

            var saleOrderDetail = (from q in db.TblSaleOrderDetail.Include(x => x.Product)
                                                     where q.SaleOrderId == saleOrderId
                                   select new TblSaleOrderDetail
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
                                                         Product = q.Product
                                                     }).ToList();


            return saleOrderDetail;

        }

        public TblJobOrder Get(int id)
        {
            /*   public int JobOrderId { get; set; }
        public int No { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }

        public virtual TblProduct Product { get; set; }
          */

            TblJobOrder jobOrder = db.TblJobOrder
                   .Where(x => x.JobOrderId == id)
                .Include(x => x.AttachmentType)
                  .Include(x => x.EquipmentType)
                    .Include(x => x.LandType)
                     .Include(x => x.ObstructionType)
                        .Include(x => x.ProjectType)
                        .Include(x => x.UndergroundType)
                        .Include(x => x.SurveyDetail).FirstOrDefault() ?? new TblJobOrder()
                        {

                        };


            if (jobOrder != null && jobOrder.SaleOrderId>0)
            {

                jobOrder.JobOrderDetail = (from q in db.TblJobOrderDetail.Include(x => x.Product)
                                           where q.JobOrderId == id
                                           select new TblJobOrderDetail
                                           {
                                               JobOrderId = q.JobOrderId,
                                               No = q.No,
                                               ProductId = q.ProductId,
                                               Quantity = q.Quantity,
                                               Weight = q.Weight,
                                               Product = q.Product
                                           }).ToList();

                jobOrder.SaleOrder = (from q in db.TblSaleOrder
                                      where q.SaleOrderId == jobOrder.SaleOrderId
                                      select q).FirstOrDefault();


                jobOrder.SaleOrder.TblSaleOrderDetail = (from q in db.TblSaleOrderDetail.Include(x => x.Product)
                                                   where q.SaleOrderId == jobOrder.SaleOrderId
                                                         select new TblSaleOrderDetail
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
                                                       Product = q.Product
                                                   }).ToList();



                jobOrder.TblJobOrderProperties = (from q in db.TblJobOrderProperties.Include(x => x.Product)
                                           where q.JobOrderId == id
                                           select new TblJobOrderProperties
                                           {
                                               JobOrderId = q.JobOrderId,
                                               No = q.No,
                                               ProductId = q.ProductId,
                                               Quantity = q.Quantity,
                                               Weight = q.Weight,
                                               Product = q.Product
                                           }).ToList();
            }
            else {
                jobOrder = new TblJobOrder();
            }

            return jobOrder;



        }

        public List<int> GetTeamByPeriod(System.DateTime tempStartDate,System.DateTime tempEndDate)
        {
            return (from job in db.TblJobOrder.Where(o => (o.StartDate>= tempStartDate  && o.EndDate<= tempEndDate))
                    select job.TeamId.Value).ToList();
                   
                 
        }


        public List<TblJobOrder> GetHeader()
        {
            List<TblJobOrder> result = new List<TblJobOrder>();

            result = (from o in db.TblJobOrder.Include(x=>x.Team).Include(x=>x.SaleOrder) select new TblJobOrder
            {



                JobOrderId = o.JobOrderId,
                JobOrderNo = o.JobOrderNo,
                SaleOrderId = o.SaleOrderId,
                JobName = o.JobName,
                StartDate = o.StartDate,
                StartDateToString = o.StartDate != null ? Converting.ToDDMMYYYY(o.StartDate.Value) : "",
                EndDate = o.EndDate,
                EndDateToString = o.EndDate != null ? Converting.ToDDMMYYYY(o.EndDate.Value) : "",
                StartWorkingTime = o.StartWorkingTime,
                EndWorkingTime = o.EndWorkingTime,
                CustomerId = o.CustomerId,
                CustomerName = o.CustomerName,
                CustomerPhone = o.CustomerPhone,
                CustomerEmail = o.CustomerEmail,
                ProjectName = o.ProjectName,
                StatusId = o.StatusId.HasValue? o.StatusId.Value:0,
                Status = o.StatusId.HasValue ? Converting.JobOrderStatus(o.StatusId.Value): Converting.JobOrderStatus(0),
               SaleOrder =(from oo in db.TblSaleOrder.Where(od=> od.SaleOrderId == o.SaleOrderId) select new TblSaleOrder {
                   SaleOrderId=oo.SaleOrderId,
                   SaleOrderNo=oo.SaleOrderNo


               }).FirstOrDefault(),
                SubDistrict = o.SubDistrict,
                Team = o.Team,
               
                CreatedDate = o.CreatedDate,
                CreateDateToString = o.CreatedDate.HasValue ? Converting.ToDDMMYYYY(o.CreatedDate.Value) : "",


            }).OrderByDescending(o => o.CreatedDate).ToList();



            return result;
        }

        public List<TblJobOrder> GetHeader(string status)
        {
            List<TblJobOrder> result = new List<TblJobOrder>();

            result = (from o in db.TblJobOrder.Include(x => x.Team).Include(x => x.SaleOrder).Where(t=>t.Status== status)
                      select new TblJobOrder
                      {



                          JobOrderId = o.JobOrderId,
                          JobOrderNo = o.JobOrderNo,
                          SaleOrderId = o.SaleOrderId,
                          JobName = o.JobName,
                          StartDate = o.StartDate,
                          StartDateToString = o.StartDate != null ? Converting.ToDDMMYYYY(o.StartDate) : "",
                          EndDate = o.EndDate,
                          EndDateToString = o.EndDate != null ? Converting.ToDDMMYYYY(o.EndDate) : "",
                          StartWorkingTime = o.StartWorkingTime,
                          EndWorkingTime = o.EndWorkingTime,
                          CustomerId = o.CustomerId,
                          CustomerName = o.CustomerName,
                          CustomerPhone = o.CustomerPhone,
                          CustomerEmail = o.CustomerEmail,
                          ProjectName = o.ProjectName,
                          Status = Converting.JobOrderStatus(o.StatusId.Value),
                          SaleOrder = (from oo in db.TblSaleOrder.Where(od => od.SaleOrderId == o.SaleOrderId)
                                       select new TblSaleOrder
                                       {
                                           SaleOrderId = oo.SaleOrderId,
                                           SaleOrderNo = oo.SaleOrderNo


                                       }).FirstOrDefault(),
                          SubDistrict = o.SubDistrict,
                          Team = o.Team,

                          CreatedDate = o.CreatedDate,
                          CreateDateToString = o.CreatedDate != null ? Converting.ToDDMMYYYY(o.CreatedDate) : "",


                      }).OrderByDescending(o => o.CreatedDate).ToList();



            return result;
        }


        public List<TblJobOrder> GetHeaderForEngineer()
        {
            List<TblJobOrder> result = new List<TblJobOrder>();

            result = (from o in db.TblJobOrder.Include(x => x.Team).Include(x => x.SaleOrder).Where(t => t.StatusId==2 || t.StatusId == 3)
                      select new TblJobOrder
                      {



                          JobOrderId = o.JobOrderId,
                          JobOrderNo = o.JobOrderNo,
                          SaleOrderId = o.SaleOrderId,
                          JobName = o.JobName,
                          StartDate = o.StartDate,
                          StartDateToString = o.StartDate != null ? Converting.ToDDMMYYYY(o.StartDate) : "",
                          EndDate = o.EndDate,
                          EndDateToString = o.EndDate != null ? Converting.ToDDMMYYYY(o.EndDate) : "",
                          StartWorkingTime = o.StartWorkingTime,
                          EndWorkingTime = o.EndWorkingTime,
                          CustomerId = o.CustomerId,
                          CustomerName = o.CustomerName,
                          CustomerPhone = o.CustomerPhone,
                          CustomerEmail = o.CustomerEmail,
                          ProjectName = o.ProjectName,
                          Status = Converting.JobOrderStatus(o.StatusId.Value),
                          StatusId= o.StatusId,
                          SaleOrder = (from oo in db.TblSaleOrder.Where(od => od.SaleOrderId == o.SaleOrderId)
                                       select new TblSaleOrder
                                       {
                                           SaleOrderId = oo.SaleOrderId,
                                           SaleOrderNo = oo.SaleOrderNo


                                       }).FirstOrDefault(),
                          SubDistrict = o.SubDistrict,
                          Team = o.Team,

                          CreatedDate = o.CreatedDate,
                          CreateDateToString = o.CreatedDate != null ? Converting.ToDDMMYYYY(o.CreatedDate) : "",


                      }).OrderByDescending(o => o.CreatedDate).ToList();



            return result;
        }


        public List<TblJobOrder> Gets(int page = 1, int size = 0
            , string src = "", int id = 0)
        {
            var data = (from q in db.TblJobOrder select q )



              /*  .Include(x => x.AttachmentType.Where(o => o.JobOrderId == id))
               .Include(x => x.EquipmentType.Where(o => o.JobOrderId == id))
                 .Include(x => x.LandType.Where(o => o.JobOrderId == id))
                  .Include(x => x.ObstructionType.Where(o => o.JobOrderId == id))
                     .Include(x => x.ProjectType.Where(o => o.JobOrderId == id))
                     .Include(x => x.UndergroundType.Where(o => o.JobOrderId == id))*/
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.JobName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }

            foreach (TblJobOrder job in data)
            {
                job.CreateDateToString = Converting.ToDDMMYYYY(job.CreatedDate);
                job.SaleOrder = (from q in db.TblSaleOrder.Where(o=>o.SaleOrderId== job.SaleOrderId).Include(o => o.Sale) select q).FirstOrDefault();
             

            }
            return data.ToList();
        }

      

        public bool IsExist(int id)
        { return db.TblJobOrder.Where(x => x.JobOrderId == id).Count() > 0 ? true : false; }

        public void Set(TblJobOrder ob)
        {
            if (ob.JobOrderId == 0)
            { db.TblJobOrder.Add(ob); }
            else {
                
               
              
              
                
               
               

                if (ob.AttachmentType != null)
                {
                    db.TblJobOrderAttachmentType.RemoveRange(db.TblJobOrderAttachmentType.Where(o => o.JobOrderId == ob.JobOrderId));
                    db.TblJobOrderAttachmentType.AddRange(ob.AttachmentType);
                }
                if (ob.ProjectType != null)
                {
                    db.TblJobOrderProjectType.RemoveRange(db.TblJobOrderProjectType.Where(o => o.JobOrderId == ob.JobOrderId));
                    db.TblJobOrderProjectType.AddRange(ob.ProjectType);
                }
                if (ob.EquipmentType != null)
                {
                    db.TblJobOrderEquipmentType.RemoveRange(db.TblJobOrderEquipmentType.Where(o => o.JobOrderId == ob.JobOrderId));
                    db.TblJobOrderEquipmentType.AddRange(ob.EquipmentType);
                }

                if (ob.LandType != null)
                {
                    db.TblJobOrderLandType.RemoveRange(db.TblJobOrderLandType.Where(o => o.JobOrderId == ob.JobOrderId));
                    db.TblJobOrderLandType.AddRange(ob.LandType);
                }

                if (ob.ObstructionType != null)
                {
                    db.TblJobOrderObstructionType.RemoveRange(db.TblJobOrderObstructionType.Where(o => o.JobOrderId == ob.JobOrderId));
                    db.TblJobOrderObstructionType.AddRange(ob.ObstructionType);
                }

                if (ob.UndergroundType != null)
                {
                    db.TblJobOrderUndergroundType.RemoveRange(db.TblJobOrderUndergroundType.Where(o => o.JobOrderId == ob.JobOrderId));
                    db.TblJobOrderUndergroundType.AddRange(ob.UndergroundType);
                }

                if (ob.SurveyDetail != null)
                {
                    db.TblJobOrderSurveyDetail.RemoveRange(db.TblJobOrderSurveyDetail.Where(o => o.JobOrderId == ob.JobOrderId));
                    db.TblJobOrderSurveyDetail.AddRange(ob.SurveyDetail);
                }



              
                db.SaveChanges();

               
                 db.TblJobOrder.Update(ob);
               // db.Entry(ob).State = EntityState.Modified;


            }
        }

        public TblJobOrderDetail GetDetail(int id = 0)
        {
            // var detail = (from d in db.TransferDetail.Where(x => x.TransferId == id) select d);

            //  if (detail == null)
            //  detail = new TransferDetail();

            return new TblJobOrderDetail();
        }


        public TblJobOrderProperties GetPropertie(int id = 0)
        {
            // var detail = (from d in db.TransferDetail.Where(x => x.TransferId == id) select d);

            //  if (detail == null)
            //  detail = new TransferDetail();

            return new TblJobOrderProperties();
        }

        public TblJobOrderProperties GetPropertie(int transferId, int seq)
        {
            var detail = (from d in db.TblJobOrderProperties.Where(x => x.JobOrderId == transferId && x.No == seq) select d);

            //  if (detail == null)
            //  detail = new TransferDetail();

            return detail.FirstOrDefault();
        }


        public TblJobOrderDetail GetDetail(int transferId, int seq)
        {
            var detail = (from d in db.TblJobOrderDetail.Where(x => x.JobOrderId == transferId && x.No == seq) select d);

            //  if (detail == null)
            //  detail = new TransferDetail();

            return detail.FirstOrDefault();
        }

        public List<TblJobOrderDetail> GetDetails(int JobOrderId)
        {
            var detail = (from d in db.TblJobOrderDetail.Where(x => x.JobOrderId == JobOrderId) select d).ToList();

         
            //  if (detail == null)
            //  detail = new TransferDetail();

            return detail;
        }

        public List<TblJobOrderProperties> GetProperties(int JobOrderId)
        {
            var detail = (from d in db.TblJobOrderProperties.Where(x => x.JobOrderId == JobOrderId) select d).ToList();


            //  if (detail == null)
            //  detail = new TransferDetail();

            return detail;
        }



        public void SetDetail(TblJobOrderDetail ob)
        {
            db.TblJobOrderDetail.Add(ob);
            /* if (ob.TransferId == 0)
             { db.TransferDetail.Add(ob); }
             else { db.Entry(ob).State = EntityState.Modified; }*/
        }

        public void SetProperty(TblJobOrderProperties ob)
        {
            db.TblJobOrderProperties.Add(ob);
            /* if (ob.TransferId == 0)
             { db.TransferDetail.Add(ob); }
             else { db.Entry(ob).State = EntityState.Modified; }*/
        }
    }
}


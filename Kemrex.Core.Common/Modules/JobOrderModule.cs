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
                        .Include(x => x.UndergroundType).FirstOrDefault() ?? new TblJobOrder()
                        {

                        };


            if (jobOrder != null)
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

                jobOrder.SaleOrder = (from q in db.TblSaleOrder.Include(x => x.TblSaleOrderDetail)
                                      where q.SaleOrderId == jobOrder.SaleOrderId
                                      select q).FirstOrDefault();

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


        public List<TblJobOrder> Gets(int page = 1, int size = 0
            , string src = "", int id = 0)
        {
            var data = db.TblJobOrder


           
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
                job.CreateDateToString = Converting.ToDDMMYYYY(job.CreateDate);

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
                db.TblJobOrderAttachmentType.RemoveRange(db.TblJobOrderAttachmentType.Where(o=>o.JobOrderId==ob.JobOrderId));
                db.TblJobOrderProjectType.RemoveRange(db.TblJobOrderProjectType.Where(o => o.JobOrderId == ob.JobOrderId));
                db.TblJobOrderEquipmentType.RemoveRange(db.TblJobOrderEquipmentType.Where(o => o.JobOrderId == ob.JobOrderId));
                db.TblJobOrderLandType.RemoveRange(db.TblJobOrderLandType.Where(o => o.JobOrderId == ob.JobOrderId));
                db.TblJobOrderObstructionType.RemoveRange(db.TblJobOrderObstructionType.Where(o => o.JobOrderId == ob.JobOrderId));
                db.TblJobOrderUndergroundType.RemoveRange(db.TblJobOrderUndergroundType.Where(o => o.JobOrderId == ob.JobOrderId));
                db.TblJobOrder.Update(ob);
                


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


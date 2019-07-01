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

        private IQueryable<TblJobOrder> Filter(IQueryable<TblJobOrder> data, string src = "", int JobOrderId = 0)
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.JobName.Contains(src)); }
            if(JobOrderId > 0)
            { data = data.Where(x => x.JobOrderId == JobOrderId); }
            return data;
        }

        public TblJobOrder Get(int id)
        {
          /*  public virtual List<TblJobOrderAttachmentType> AttachmentType { get; set; }
        public virtual List<TblJobOrderEquipmentType> EquipmentType { get; set; }
        public virtual List<TblJobOrderLandType> LandType { get; set; }
        public virtual List<TblJobOrderObstructionType> rObstructionType { get; set; }
        public virtual List<TblJobOrderProjectType> ProjectType { get; set; }
        public virtual List<TblJobOrderUndergroundType> UndergroundType { get; set; }
        */
            return db.TblJobOrder
                .Where(x => x.JobOrderId == id)
             .Include(x => x.AttachmentType)
               .Include(x => x.EquipmentType)
                 .Include(x => x.LandType)
                  .Include(x => x.ObstructionType)
                     .Include(x => x.ProjectType)
                     .Include(x => x.UndergroundType)
                .FirstOrDefault() ?? new TblJobOrder() { };
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
    }
}

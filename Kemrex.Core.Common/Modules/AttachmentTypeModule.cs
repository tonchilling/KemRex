using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class AttachmentTypeModule : IModule<TblJobOrderAttachmentType, int>
    {

       /* public virtual DbSet<TblJobOrderAttachmentType> TblJobOrderAttachmentType { get; set; }
        public virtual DbSet<TblJobOrderEquipmentType> TblJobOrderEquipmentType { get; set; }
        public virtual DbSet<TblJobOrderLandType> TblJobOrderLandType { get; set; }
        public virtual DbSet<TblJobOrderObstructionType> TblJobOrderObstructionType { get; set; }
        public virtual DbSet<TblJobOrderProjectType> TblJobOrderProjectType { get; set; }
        public virtual DbSet<TblJobOrderUndergroundType> TblJobOrderUndergroundType { get; set; }
        */

        private readonly mainContext db;
        public AttachmentTypeModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "")
        {
            var data = db.TblJobOrderAttachmentType.AsQueryable();
            data = Filter(data, src);
            return data.Count();
        }

        public void Delete(TblJobOrderAttachmentType ob)
        {
            if (IsExist(ob.AttachmentTypeId))
            { db.TblJobOrderAttachmentType.Remove(ob); }
        }

        private IQueryable<TblJobOrderAttachmentType> Filter(IQueryable<TblJobOrderAttachmentType> data, string src = "")
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.AttachmentType.CategoryName.Contains(src)); }
            return data;
        }

        public TblJobOrderAttachmentType Get(int id)
        {
            return db.TblJobOrderAttachmentType
                .Include(x => x.AttachmentType)
                .Where(x => x.AttachmentTypeId == id).FirstOrDefault() ?? new TblJobOrderAttachmentType()
                    {
                      
                    };
        }

        public List<TblJobOrderAttachmentType> Gets(int page = 1, int size = 0
            , string src = "")
        {
            var data = db.TblJobOrderAttachmentType
                .Include(x => x.AttachmentType)
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.AttachmentType.CategoryName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TblJobOrderAttachmentType.Where(x => x.AttachmentTypeId == id).Count() > 0 ? true : false; }

        public void Set(TblJobOrderAttachmentType ob)
        {
            if (ob.AttachmentTypeId == 0)
            { db.TblJobOrderAttachmentType.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

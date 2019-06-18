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
    public class SaleOrderAttachmentModule : IModule<TblSaleOrderAttachment, int>
    {
        private readonly mainContext db;
        public SaleOrderAttachmentModule(mainContext context)
        {
            db = context;
        }

        public void Delete(int id)
        {
            TblSaleOrderAttachment ob = db.TblSaleOrderAttachment.Where(t => t.Id == id).Single();
            db.TblSaleOrderAttachment.Remove(ob);
        }

        public void Delete(TblSaleOrderAttachment ob)
        {
            throw new NotImplementedException();
        }

        public TblSaleOrderAttachment Get(int id)
        {
            return db.TblSaleOrderAttachment
                  .Where(x => x.Id == id)
                  .FirstOrDefault() ?? new TblSaleOrderAttachment();
        }
        public int GetLastOrder(int sid)
        {
            return db.TblSaleOrderAttachment
                  .Where(x => x.SaleOrderId == sid)
                  .Count();
        }
        public List<TblSaleOrderAttachment> Gets(int id = 0)
        {
            return db.TblSaleOrderAttachment
                 .Where(a => a.SaleOrderId == id)
                 .ToList();
        }

        public bool IsExist(int id)
        {
            { return db.TblSaleOrderAttachment.Where(x => x.Id == id).Count() > 0 ? true : false; }
        }

        public void Set(TblSaleOrderAttachment ob)
        {
            if (ob.Id <= 0)
            { db.TblSaleOrderAttachment.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }
    }
}

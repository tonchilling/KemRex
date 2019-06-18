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
        public string GetLastId()
        {
            return (from q in db.TblQuotation
                    select q.QuotationNo).Max();
        }
        public List<TblQuotation> Gets(int page = 1, int size = 0,string src="")
        {
            var data = db.TblQuotation.Include(c => c.Status)
                        .Include(c=>c.Customer)
                        .OrderByDescending(c=>c.QuotationId)
                .AsQueryable();

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
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

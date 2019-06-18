using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class TeamSaleDetailModule : IModule<TeamSaleDetail, int>
    {
        private readonly mainContext db;
        public TeamSaleDetailModule(mainContext context)
        {
            db = context;
        }

        public int Count(int teamId = 0, string src = "")
        {
            var data = db.TeamSaleDetail.AsQueryable();
            data = Filter(data, teamId, src);
            return data.Count();
        }

        public void Delete(TeamSaleDetail ob)
        {
            if (IsExist(ob.Id))
            { db.TeamSaleDetail.Remove(ob); }
        }

        private IQueryable<TeamSaleDetail> Filter(IQueryable<TeamSaleDetail> data, int teamId = 0, string src = "")
        {
            if (teamId > 0)
            { data = data.Where(x => x.TeamId == teamId); }
            if (!string.IsNullOrWhiteSpace(src))
            {
                data = data.Where(x =>
                    x.Account.AccountName.Contains(src)
                    || x.Account.AccountEmail.Contains(src));
            }
            return data;
        }

        public TeamSaleDetail Get(int id)
        {
            return db.TeamSaleDetail
                .Where(x => x.Id == id)
                .Include(x => x.Account)
                .FirstOrDefault() ?? new TeamSaleDetail();
        }

        public List<TeamSaleDetail> Gets(int page = 1, int size = 0
            , int teamId = 0, string src = "")
        {
            var data = db.TeamSaleDetail
                .Include(x => x.Account)
                .AsQueryable();
            data = Filter(data, teamId, src)
                .OrderBy(x => x.Account.AccountName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TeamSaleDetail.Where(x => x.Id == id).Count() > 0 ? true : false; }

        public void Set(TeamSaleDetail ob)
        {
            if (ob.Id == 0)
            { db.TeamSaleDetail.Add(ob); }
            else { db.TeamSaleDetail.Update(ob); }
        }
    }
}

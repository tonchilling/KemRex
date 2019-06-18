using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class TeamOperationDetailModule : IModule<TeamOperationDetail, int>
    {
        private readonly mainContext db;
        public TeamOperationDetailModule(mainContext context)
        {
            db = context;
        }

        public int Count(int teamId = 0, string src = "")
        {
            var data = db.TeamOperationDetail.AsQueryable();
            data = Filter(data, teamId, src);
            return data.Count();
        }

        public void Delete(TeamOperationDetail ob)
        {
            if (IsExist(ob.Id))
            { db.TeamOperationDetail.Remove(ob); }
        }

        private IQueryable<TeamOperationDetail> Filter(IQueryable<TeamOperationDetail> data, int teamId = 0, string src = "")
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

        public TeamOperationDetail Get(int id)
        {
            return db.TeamOperationDetail
                .Where(x => x.Id == id)
                .Include(x => x.Account)
                .FirstOrDefault() ?? new TeamOperationDetail();
        }

        public List<TeamOperationDetail> Gets(int page = 1, int size = 0
            , int teamId = 0, string src = "")
        {
            var data = db.TeamOperationDetail
                .Include(x => x.Account)
                .AsQueryable();
            data = Filter(data, teamId, src)
                .OrderBy(x => x.Account.AccountName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TeamOperationDetail.Where(x => x.Id == id).Count() > 0 ? true : false; }

        public void Set(TeamOperationDetail ob)
        {
            if (ob.Id == 0)
            { db.TeamOperationDetail.Add(ob); }
            else { db.TeamOperationDetail.Update(ob); }
        }
    }
}

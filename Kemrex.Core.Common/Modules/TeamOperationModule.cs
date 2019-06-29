using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class TeamOperationModule : IModule<TeamOperation, int>
    {
        private readonly mainContext db;
        public TeamOperationModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "", int managerId = 0)
        {
            var data = db.TeamOperation.AsQueryable();
            data = Filter(data, src, managerId);
            return data.Count();
        }
        public void Delete(TeamOperation ob)
        {
            if (IsExist(ob.TeamId))
            { db.TeamOperation.Remove(ob); }
        }

        private IQueryable<TeamOperation> Filter(IQueryable<TeamOperation> data, string src = "", int managerId = 0)
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.TeamName.Contains(src)); }
            if (managerId > 0)
            { data = data.Where(x => x.ManagerId == managerId); }
            return data;
        }

        public TeamOperation Get(int id)
        {
            TeamOperation teamObj= db.TeamOperation
                .Where(x => x.TeamId == id)
                .Include(x => x.Manager)
                .Include(x => x.TeamOperationDetail)
                .FirstOrDefault() ?? new TeamOperation();

            if (teamObj.TeamOperationDetail != null)
            {
                foreach (TeamOperationDetail detail in teamObj.TeamOperationDetail)
                {
                    detail.Account = db.SysAccount.Where(x => x.AccountId == detail.AccountId).FirstOrDefault();
                }
            }

            return teamObj;
        }

        public List<TeamOperation> Gets(int page = 1, int size = 0
            , string src = "", int managerId = 0)
        {
            List<TeamOperation> teamList = null;
            var data = (from team in db.TeamOperation
                 .Include(x => x.Manager)
                 select new TeamOperation {
                  TeamId= team.TeamId,
                     TeamName = team.TeamName,
        ManagerId = team.ManagerId,
    CreatedBy= team.CreatedBy ,
CreatedDate = team.CreatedDate,
      UpdatedBy = team.UpdatedBy,
    UpdatedDate= team.UpdatedDate,
      Manager = team.Manager,
      TeamOperationDetail=db.TeamOperationDetail.Where(detail=>detail.TeamId==team.TeamId).ToList()
    })

                       //   .Include(x => x.TeamOperationDetail.Where(d=>d.TeamId==x.TeamId))
                       .AsQueryable();
           
            data = Filter(data, src)
                .OrderBy(x => x.TeamName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }

            teamList = data.ToList();
           
            return data.ToList();
        }


        public List<TeamOperation> GetAll()
        {
            var data = db.TeamOperation;
              //  .Include(x => x.Manager)
               // .Include(x => x.TeamOperationDetail)
                //.AsQueryable();
        
            return data.ToList();
        }

        public List<SysAccount> GetNotMembers(string src = "")
        {
            var data = db.SysAccount
                .Where(x =>
                    !db.TeamOperation.Select(y => y.ManagerId).Contains(x.AccountId)
                    && !db.TeamOperationDetail.Select(y => y.AccountId).Contains(x.AccountId));
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.AccountName.Contains(src) || x.AccountEmail.Contains(src)); }
            data = data.OrderBy(x => x.AccountName);
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TeamOperation.Where(x => x.TeamId == id).Count() > 0 ? true : false; }

        public void Set(TeamOperation ob)
        {
            if (ob.TeamId == 0)
            { db.TeamOperation.Add(ob); }
            else { db.TeamOperation.Update(ob); }
        }
    }
}

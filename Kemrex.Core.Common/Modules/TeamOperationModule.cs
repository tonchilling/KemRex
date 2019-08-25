using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class TeamOperationModule
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

            TeamOperation teamOperation = db.TeamOperation

                .Where(x => x.TeamId == id)
                .Include(x => x.Manager)
                .Include(x => x.TeamOperationDetail)
                .FirstOrDefault() ?? new TeamOperation();


            teamOperation.TeamOperationDetail = (from team in db.TeamOperationDetail.Where(o=> o.TeamId== teamOperation.TeamId) select new TeamOperationDetail {
               Id= team.Id,
        TeamId = team.TeamId,
                AccountId = team.AccountId,
                TeamRemark = team.TeamRemark,
                CreatedBy = team.CreatedBy,
                CreatedDate = team.CreatedDate,
                UpdatedBy = team.UpdatedBy,
                UpdatedDate = team.UpdatedDate,
                Account = db.SysAccount.Where(o=>o.AccountId==team.AccountId).FirstOrDefault()
    }).ToList();



            return teamOperation;



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

        public List<TeamOperation> GetTeamNotIn(List<int> notinTeam)
        {
            var data = db.TeamOperation.Where(o=> !notinTeam.Contains(o.TeamId));
            //  .Include(x => x.Manager)
            // .Include(x => x.TeamOperationDetail)
            //.AsQueryable();

            return data.ToList();
        }

        public List<SysAccount> GetNotMembers(string src = "")
        {
            var data = db.SysAccount
                .Where(x =>
                    //!db.TeamOperation.Select(y => y.ManagerId).Contains(x.AccountId)
                    //&&
                    !db.TeamOperationDetail.Select(y => y.AccountId).Contains(x.AccountId)
                    && db.SysAccountRole.Where(z => z.RoleId == 203).Select(y => y.AccountId).Contains(x.AccountId)
                    );
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
        public List<TeamOperation> Manager(long AccountId)
        {
            var data = (from s in db.TeamOperation
                        where s.ManagerId == AccountId
                        select s).ToList();
            return data;
        }

        public Team CheckTeamOperation(long AccountId)
        {
            var data = (from a in db.TeamOperationDetail
                        join s in db.TeamOperation on a.TeamId equals s.TeamId
                        where a.AccountId == AccountId
                        || s.ManagerId == AccountId
                        select new Team
                        {
                            EmpId = 0,
                            AccountId = a.AccountId,
                            TeamId = a.TeamId,
                            ManagerId = s.ManagerId
                        }
                       ).FirstOrDefault();
            return data;
        }
    }
}

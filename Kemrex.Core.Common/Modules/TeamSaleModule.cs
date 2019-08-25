using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class TeamSaleModule
    {
        private readonly mainContext db;
        public TeamSaleModule(mainContext context)
        {
            db = context;
        }

        public int Count(string src = "", int managerId = 0)
        {
            var data = db.TeamSale.AsQueryable();
            data = Filter(data, src, managerId);
            return data.Count();
        }
        public void Delete(TeamSale ob)
        {
            if (IsExist(ob.TeamId))
            { db.TeamSale.Remove(ob); }
        }

        private IQueryable<TeamSale> Filter(IQueryable<TeamSale> data, string src = "", int managerId = 0)
        {
            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.TeamName.Contains(src)); }
            if(managerId>0)
            { data = data.Where(x => x.ManagerId == managerId); }
            return data;
        }

        public TeamSale Get(int id)
        {
            return db.TeamSale
                .Where(x => x.TeamId == id)
                .Include(x => x.Manager)
                .Include(x => x.TeamSaleDetail)
                    .ThenInclude(x => x.Account)
                .FirstOrDefault() ?? new TeamSale();
        }

       


        public List<TeamSale> Gets(int page = 1, int size = 0
            , string src = "", int managerId = 0)
        {
            var data = db.TeamSale
                .Include(x => x.Manager)
                .Include(x => x.TeamSaleDetail)
                    .ThenInclude(x => x.Account)
                .AsQueryable();
            data = Filter(data, src)
                .OrderBy(x => x.TeamName);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public List<SysAccount> GetNotMembers(string src = "")
        {
            var data = db.SysAccount
                .Where(x =>
                    !db.TeamSale.Select(y => y.ManagerId).Contains(x.AccountId)
                    && !db.TeamSaleDetail.Select(y => y.AccountId).Contains(x.AccountId)
                    && db.SysAccountRole.Where(z => z.RoleId==202).Select(y => y.AccountId).Contains(x.AccountId)
                    );

            //var data = (from a in db.SysAccount
            //            join r in db.SysAccountRole on a.AccountId equals r.AccountId
            //            where r.RoleId == 202
            //            && !db.TeamSale.Select(y => y.ManagerId).Contains(x.AccountId)
            //            select new Team
            //            {
            //                EmpId = e.EmpId,
            //                AccountId = (e.AccountId.HasValue ? e.AccountId.Value : 0),
            //                TeamId = d.TeamId
            //            })


            if (!string.IsNullOrWhiteSpace(src))
            { data = data.Where(x => x.AccountName.Contains(src) || x.AccountEmail.Contains(src)); }
            data = data.OrderBy(x => x.AccountName);
            return data.ToList();
        }

        public bool IsExist(int id)
        { return db.TeamSale.Where(x => x.TeamId == id).Count() > 0 ? true : false; }

        public void Set(TeamSale ob)
        {
            if (ob.TeamId == 0)
            { db.TeamSale.Add(ob); }
            else { db.TeamSale.Update(ob); }
        }

        public Team GetManager(int EmpId)
        {
            var data = (from a in
                           (from e in db.TblEmployee
                            join d in db.TeamSaleDetail on e.AccountId equals d.AccountId
                            where e.EmpId == EmpId
                            select new Team
                            {
                                EmpId = e.EmpId,
                                AccountId = (e.AccountId.HasValue ? e.AccountId.Value : 0),
                                TeamId = d.TeamId
                            })
                        join s in db.TeamSale on a.TeamId equals s.TeamId
                        select new Team
                        {
                            EmpId = a.EmpId,
                            AccountId = a.AccountId,
                            TeamId = a.TeamId,
                            ManagerId = s.ManagerId
                        }
                       ).FirstOrDefault();
            return data;
        }
        public TblEmployee IsManager(int EmpId)
        {
            var data = (from e in db.TblEmployee
                        join s in db.TeamSale on e.AccountId equals s.ManagerId
                        where e.EmpId == EmpId
                        select e).FirstOrDefault();
            return data;
        }
        public TblEmployee defaultManager()
        {
            var data = (from e in db.TblEmployee
                        join s in db.TeamSale on e.AccountId equals s.ManagerId
                        orderby s.TeamId ascending
                        select e).FirstOrDefault();
            return data;
        }
        public TeamSale Manager(long AccountId)
        {
            var data = (from s in db.TeamSale
                        where s.ManagerId == AccountId
                        select s).FirstOrDefault();
            return data;
        }
        
        public Team CheckTeamSale(long AccountId)
        {
            var data = (from a in db.TeamSaleDetail
                        join s in db.TeamSale on a.TeamId equals s.TeamId
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

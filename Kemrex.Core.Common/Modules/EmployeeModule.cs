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
    public class EmployeeModule : IModule<TblEmployee, int>
    {
        private readonly mainContext db;
        public EmployeeModule(mainContext context)
        {
            db = context;
        }

        public int Counts(string src = "", string phone = "", string email = "")
        {
            var data = db.TblEmployee.AsQueryable();
            data = Filter(data, src, phone, email);
            return data.Count();
        }

        public void Delete(TblEmployee ob)
        {
            if (IsExist(ob.EmpId))
            { db.TblEmployee.Remove(ob); }
        }

        public TblEmployee Get(int id)
        {
            return db.TblEmployee
                .Where(x => x.EmpId == id)
                .Include(x => x.Prefix)
                .FirstOrDefault() ?? new TblEmployee() { Prefix = new EnmPrefix() };
        }


        public TblEmployee GetByCondition(int id)
        {
            return db.TblEmployee
                .Where(x => x.EmpId == id)
              
                .FirstOrDefault() ?? new TblEmployee() { Prefix = new EnmPrefix() };
        }

        public List<TblEmployee> Gets(int page = 1, int size = 0
            , string src = "", string phone = "", string email = "")
        {
            var data = db.TblEmployee.Include(a=>a.Department)
                .AsQueryable();
            data = Filter(data, src, phone, email);
            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }

        public List<TblEmployee> GetByEmployeeCode(string id)
        {
            //return db.TblEmployee
            //    .Where(x => x.EmpCode == EmpCode).ToList();
            var empList = (from emp in db.TblEmployee.Where(c => c.EmpCode.Contains(id) || c.EmpNameTh.Contains(id) || c.EmpNameEn.Contains(id))
                           select new TblEmployee
                           {
                               EmpId = emp.EmpId,
                               EmpCode = emp.EmpCode,
                               EmpNameTh = emp.EmpNameTh,
                               EmpNameEn = emp.EmpNameEn,
                               EmpEmail = emp.EmpEmail,
                               EmpMobile = emp.EmpMobile,
                               Status = emp.Status
                           }).ToList();


            return empList;
        }
        public TblEmployee GetEmployeeByEmpCode(string id)
        {
            return db.TblEmployee
                .Where(x => x.EmpCode == id)

                .FirstOrDefault() ?? new TblEmployee() { Prefix = new EnmPrefix() };
        }

        private IQueryable<TblEmployee> Filter(IQueryable<TblEmployee> data, string src, string phone, string email)
        {
            if (!string.IsNullOrWhiteSpace(src))
            {
                data = data.Where(x =>
                    x.EmpNameTh.Contains(src)
                    || x.EmpNameTh.Contains(src));
            }
            if (!string.IsNullOrWhiteSpace(phone)) { data = data.Where(x => x.EmpMobile.Contains(phone)); }
            if (!string.IsNullOrWhiteSpace(email)) { data = data.Where(x => x.EmpEmail.Contains(email)); }
            return data;
        }


        public string GetLastEmpCode()
        {
            return (from emp in db.TblEmployee.OrderByDescending(o => o.EmpId) select emp.EmpCode).FirstOrDefault();

        }
        public bool IsExist(int id)
        { return db.TblEmployee.Where(x => x.EmpId == id).Count() > 0 ? true : false; }

        public void Set(TblEmployee ob)
        {
            if (ob.EmpId <= 0)
            { db.TblEmployee.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }

        #region Business Functions
        public bool IsExistsCode(int empId, string empCode)
        {
            bool result = true;
            if (db.TblEmployee.Where(x => x.EmpCode == empCode && x.EmpId != empId).Count() == 0) { result = false; }
            return result;
        }
        public bool IsExistsEmail(int empId, string email)
        {
            bool result = true;
            if (db.TblEmployee.Where(x => x.EmpEmail == email && x.EmpId != empId).Count() == 0) { result = false; }
            return result;
        }
        #endregion
    }
}

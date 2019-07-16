using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Kemrex.Core.Common.Helper;
namespace Kemrex.Core.Common.Modules
{
   public class TransferStockModule
    {
        private readonly mainContext db;
        private WebDB webdb;
        public TransferStockModule(mainContext context)
        {
            db = context;
            webdb = new WebDB();
        }
        public void Delete(TransferStockHeader ob)
        {
            if (IsExist(ob.TransferNo))
            { db.TransferStockHeader.Remove(ob); }
        }
        public void DeleteDetail(TransferStockDetail ob)
        {
            if (IsExist(ob.TransferStockId))
            { db.TransferStockDetail.Remove(ob); }
        }
        public int Count(int groupId = 0, string TransferType = "")
        {
            var data = db.TransferStockHeader.Where(o=>o.TransferType== TransferType).AsQueryable();
            return data.Count();
        }
       

        public string GetLastId(string pre)
        {
            return (from q in db.TransferStockHeader
                    .Where(n=>n.TransferNo.Contains(pre))
                    select q.TransferNo).Max();
        }
        public TransferStockHeader GetHeader(int id)
        {
            TransferStockHeader transferStockHeader = (from q in db.TransferStockHeader
                .Where(x => x.TransferStockId == id) select new TransferStockHeader {
                    TransferStockId=q.TransferStockId,
                    TransferNo = q.TransferNo,
                    TransferType = q.TransferType,
                    TransferDate = q.TransferDate,
                    TransferTime = q.TransferTime,
                    ReceiveTo = q.ReceiveTo,
                    Reason = q.Reason,
                    CarType = q.CarType,
                    Company = q.Company,
                    CarNo = q.CarNo,
                    CarBrand = q.CarBrand,
                    SendToDepartment = q.SendToDepartment,
                    Remark = q.Remark,
                    EmpId = q.EmpId,
                    BillNo = q.BillNo,
                    TransferStatus = q.TransferStatus,
                    Note1 = q.Note1,
                    CreateDate = q.CreateDate,
                    CreateBy = q.CreateBy,
                    UpdateDate = q.UpdateDate,
                    Status = q.Status
                })
                .FirstOrDefault() ?? new TransferStockHeader()
                {

                };
            return transferStockHeader;
        }
        public TransferStockHeader Get(int id)
        {
          
                
                
               TransferStockHeader transferStockHeader= db.TransferStockHeader
                   .Where(x => x.TransferStockId == id)
                   .FirstOrDefault() ?? new TransferStockHeader()
                   {
                       
                   };

            transferStockHeader.TransferStockDetail = (from q in db.TransferStockDetail.Include(x => x.Product)
                                 where q.TransferStockId == id
                                 select new TransferStockDetail
                                 {
                                     TransferStockId = q.TransferStockId,
                                     Seq = q.Seq,
                                     ProductId= q.ProductId,
                                     CurrentQty = q.CurrentQty,
                                     RequestQty = q.RequestQty,
                                     RequestUnit = q.RequestUnit,
                                     RequestUnitFactor = q.RequestUnitFactor,
                                     LastModified = q.LastModified,
                                     Product=db.TblProduct.Where(p=>p.ProductId== q.ProductId).FirstOrDefault()
      
    }).ToList();

            return transferStockHeader;


        }


        public TransferStockDetail GetDetail(int id = 0)
        {
           // var detail = (from d in db.TransferDetail.Where(x => x.TransferId == id) select d);

          //  if (detail == null)
              //  detail = new TransferDetail();

            return new TransferStockDetail();
        }


        public TransferStockDetail GetDetail(int transferStockId, int seq)
        {
             var detail = (from d in db.TransferStockDetail
                           .Where(x => x.TransferStockId == transferStockId && x.Seq== seq)
                           select new TransferStockDetail
                           {
                               TransferStockId = d.TransferStockId,
                               Seq = d.Seq
                           });

            //  if (detail == null)
            //  detail = new TransferDetail();

            return detail.FirstOrDefault();
        }

        public int GetDetails(int id)
        {

            return (from detail in db.TransferStockDetail.Where(x => x.TransferStockId == id) select detail.ProductId).Count();
            //return db.TransferDetail
              //  .Where(x => x.TransferId == id).ToList();
        }



        public List<TransferStockHeader> Gets(int page = 1, int size = 0, string src = "",string TransferType="")
        {
            var data = db.TransferStockHeader.Where(o=>o.TransferType== TransferType)
                        .OrderByDescending(c => c.TransferNo)
                .AsQueryable();

          //  if (month > 0) { data = data.Where(x => x.TransferDate.Value.Month == month); }

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public List<TransferStockHeader> GetList()
        {
             var data = db.TransferStockHeader
                        .OrderByDescending(c => c.TransferNo)
                .AsQueryable();
            return data.ToList();
        }
        public List<TransferStockHeader> GetList(string TransferType = "")
        {
            var data = db.TransferStockHeader.Where(o => o.TransferType == TransferType)
                       .OrderByDescending(c => c.TransferNo)
               .AsQueryable();
            return data.ToList();
        }
        public bool IsExist(string id)
        {
             return db.TransferStockHeader.Where(x => x.TransferNo == id).Count() > 0 ? true : false; 
        }

        public bool IsExist(int id)
        {
            return db.TransferStockHeader.Where(x => x.TransferStockId == id).Count() > 0 ? true : false;
        }

        public void Set(TransferStockHeader ob)
        {
            if (ob.TransferStockId == 0)
            { db.TransferStockHeader.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }
        }

        public void SetDetail(TransferStockDetail ob)
        {
            db.TransferStockDetail.Add(ob);
            /* if (ob.TransferId == 0)
             { db.TransferDetail.Add(ob); }
             else { db.Entry(ob).State = EntityState.Modified; }*/
        }

        public bool Add(TransferStockDetail dto)
        {
            bool result = false;
            string sql = "sp_TransferStockDetail_Insert";


            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TransferStockId", dto.TransferStockId)); 
            paramList.Add(new SqlParameter("@ProductId", dto.ProductId));
            paramList.Add(new SqlParameter("@RequestQty", dto.RequestQty));
            paramList.Add(new SqlParameter("@Seq", dto.Seq));
            try
            {

                result = webdb.ExcecuteNonQuery(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("sp_TransferStockDetail_Insert::" + ex.ToString());
            }
            finally
            { }
            return result;
        }
        public bool Del(TransferStockDetail dto)
        {
            bool result = false;
            string sql = "sp_TransferStockDetail_Delete";


            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TransferStockId", dto.TransferStockId));
            paramList.Add(new SqlParameter("@Seq", dto.Seq));
            try
            {

                result = webdb.ExcecuteNonQuery(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("sp_TransferStockDetail_Delete::" + ex.ToString());
            }
            finally
            { }
            return result;
        }
        public bool TransferStockInApprove(int transferstockid)
        {
            bool result = false;
            string sql = "sp_TransferStockInHeader_Approve";
            List<SqlParameter> paramList = new List<SqlParameter>();
            try
            {
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@TransferStockId", transferstockid));

                result = webdb.ExcecuteNonQuery(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("TransferStockInApprove.Approve::" + ex.ToString());
            }
            finally
            { }
            return result;
        }
        public bool TransferStockOutApprove(int transferstockid)
        {
            bool result = false;
            string sql = "sp_TransferStockOutHeader_Approve";
            List<SqlParameter> paramList = new List<SqlParameter>();
            try
            {
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@TransferStockId", transferstockid));

                result = webdb.ExcecuteNonQuery(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("TransferStockOutApprove.Approve::" + ex.ToString());
            }
            finally
            { }
            return result;
        }
    }
}

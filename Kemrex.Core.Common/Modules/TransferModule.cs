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
   public class TransferModule 
    {
        private readonly mainContext db;
        private WebDB webdb;
        public TransferModule(mainContext context)
        {
            db = context;
            webdb = new WebDB();
        }
        public void Delete(TransferHeader ob)
        {
            if (IsExist(ob.TransferNo))
            { db.TransferHeader.Remove(ob); }
        }
        public void DeleteDetail(TransferDetail ob)
        {
            if (IsExist(ob.TransferId))
            { db.TransferDetail.Remove(ob); }
        }
        public int Count(int groupId = 0, string TransferType = "")
        {
            var data = db.TransferHeader.Where(o=>o.TransferType== TransferType).AsQueryable();
            return data.Count();
        }
       

        public string GetLastId(string pre,string type)
        {
            return (from q in db.TransferHeader
                    .Where(n=>n.TransferNo.Contains(pre) && n.TransferType== type)
                    select q.TransferNo).Max();
        }
        public TransferHeader Get(int id)
        {
          
                
                
               TransferHeader transferHeader = (from q in db.TransferHeader.Where(t => t.TransferId == id)
                                                select new TransferHeader
                                                {
                                                    TransferId = q.TransferId,
                                                    TransferNo = q.TransferNo,
                                                    TransferType = q.TransferType,
                                                    TransferDate = q.TransferDate,
                                                    TransferTime = q.TransferTime,
                                                    JobOrderId = q.JobOrderId,
                                                    RefTransferId = q.RefTransferId,
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
                                                    CreatedDate = q.CreatedDate,
                                                    UpdatedDate = q.UpdatedDate
                                                }).FirstOrDefault() ?? new TransferHeader()
                   {
                       
                   };

            var detail = (from q in db.TransferDetail.Where(t => t.TransferId == id) select q).ToList();

            if (detail != null && detail.Count > 0)
            {
                transferHeader.TransferDetail = (from q in db.TransferDetail.Include(x => x.Product)
                                                 where q.TransferId == id
                                                 select new TransferDetail
                                                 {
                                                     TransferId = q.TransferId,
                                                     RefTransferId = q.RefTransferId,
                                                     RefTransferNo = (q.RefTransferId==0?null:(from tDetail in db.TransferHeader.Where(t => t.TransferId == q.RefTransferId) select tDetail.TransferNo).FirstOrDefault().ToString()),
                                                     Seq = q.Seq,
                                                     ProductId = q.ProductId,
                                                     CurrentQty = q.CurrentQty,
                                                     RequestQty = q.RequestQty,
                                                     RequestUnit = q.RequestUnit,
                                                     RequestUnitFactor = q.RequestUnitFactor,
                                                     LastModified = q.LastModified,
                                                     Product = db.TblProduct.Where(p => p.ProductId == q.ProductId).FirstOrDefault()

                                                 }).ToList();
            }


            transferHeader.JobOrder = (from q in db.TblJobOrder.Where(o => o.JobOrderId == transferHeader.JobOrderId)
                                                     .Include(x => x.SaleOrder)
                                       .Include(x=>x.JobOrderDetail) select q).FirstOrDefault();

            /*if (transferHeader.RefTransferId != null && transferHeader.RefTransferId>0)
            {
                transferHeader.RefTransfer = (from q in db.TransferHeader.Where(t => t.TransferId == transferHeader.RefTransferId) select new TransferHeader {
                     TransferId =q.TransferId,
                    TransferNo = q.TransferNo ,
                    TransferType = q.TransferType ,
                     TransferDate = q.TransferDate,
                    TransferTime = q.TransferTime ,
                    JobOrderId = q.JobOrderId,
                    RefTransferId = q.RefTransferId,
                    ReceiveTo = q.ReceiveTo ,
                    Reason = q.Reason ,
                    CarType = q.CarType,
                    Company = q.Company ,
                    CarNo = q.CarNo ,
                    CarBrand = q.CarBrand ,
                    SendToDepartment = q.SendToDepartment,
                    Remark = q.Remark ,
                    EmpId = q.EmpId ,
                    BillNo = q.BillNo ,
                    TransferStatus = q.TransferStatus,
                    Note1 = q.Note1 ,
                    CreateDate = q.CreateDate,
                    UpdateDate = q.UpdateDate
                }).FirstOrDefault()??new TransferHeader
                {

                };
            }*/
         

            return transferHeader;


        }


        public RefTransferHeader GetTransferHeader(int transferid)
        {
            bool result = false;
            string sql = "sp_TransfetHeader_GetById";
            RefTransferHeader header = null;
            SqlCommand sqlCommand = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlConnection conn = null;
            SqlDataReader reader = null;

            try
            {
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@TransferId", transferid));

                webdb.OpenConnection();
                conn = webdb.Connection;
                 sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = conn;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();
                //   if (reader.Read())
                //  {

                header = Converting.ConvertDataReaderToObjList<RefTransferHeader>(reader).FirstOrDefault();
                if (header != null)
                {

                    reader.NextResult();

                    header.TransferDetail = Converting.ConvertDataReaderToObjList<TransferDetail>(reader);
                    reader.NextResult();

                    header.TransferRefHeader = Converting.ConvertDataReaderToObjList<TransferRefHeader>(reader);
                }



                // }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                webdb.CloseConnection();
            }








            return header;
        }

        public int GetDetails(int id)
        {

            return (from detail in db.TransferDetail.Where(x => x.TransferId == id) select detail.ProductId).Count();
            //return db.TransferDetail
              //  .Where(x => x.TransferId == id).ToList();
        }


        public List<TransferDetail> GetTransferProductDetails(List<string> transferIds)
        {
            

            List<TransferDetail> list = new List<TransferDetail>();
            list = (from q in db.TransferDetail.Include(x => x.Product)
                                             where  transferIds.Contains(q.TransferId.ToString())
                    select new TransferDetail
                                             {
                                                 TransferId = q.TransferId,
                                                 TransferNo= db.TransferHeader.Where(p => p.TransferId == q.TransferId).FirstOrDefault().TransferNo,
                                                 Seq = q.Seq,
                                                 ProductId = q.ProductId,
                                                 CurrentQty = q.RequestQty.ToString(),
                                                 RequestQty = q.RequestQty,
                                                 RequestUnit = q.RequestUnit,
                                                 RequestUnitFactor = q.RequestUnitFactor,
                                                 LastModified = q.LastModified,
                                                 Product = db.TblProduct.Where(p => p.ProductId == q.ProductId).FirstOrDefault()

                                             }).ToList();

            return list;


        }


        public List<TransferHeader> Gets( string TransferType = "",string stDate="", string endDate="",string status="")
        {
            var data = (from d in db.TransferHeader.Where(o => o.TransferType == TransferType && (o.TransferStatus.ToString()== status || status==""))
                        select new TransferHeader
                        {
                            TransferId = d.TransferId,
                            TransferNo = d.TransferNo,
                            TransferType = d.TransferType,
                            TransferDate = d.TransferDate,
                            TransferTime = d.TransferTime,
                            JobOrderId = d.JobOrderId,
                            ReceiveTo = d.ReceiveTo,
                            Reason = d.Reason,
                            CarType = d.CarType,
                            Company = d.Company,
                            CarNo = d.CarNo,
                            CarBrand = d.CarBrand,
                            SendToDepartment = d.SendToDepartment,
                            Remark = d.Remark,
                            EmpId = d.EmpId,
                            BillNo = d.BillNo,
                            TransferStatus = d.TransferStatus,
                            Note1 = d.Note1,
                            CreatedDate = d.CreatedDate,
                            UpdatedDate = d.UpdatedDate

                        }).OrderByDescending(c => c.TransferNo)
                .AsQueryable();
            return data.ToList();
        }
        public List<TransferHeader> GetList(string TransferType = "")
        {
            var data = (from d in db.TransferHeader.Where(o => o.TransferType == TransferType)
                        select new TransferHeader
                        {
                            TransferId = d.TransferId,
                            TransferNo = d.TransferNo,
                            TransferType = d.TransferType,
                            TransferDate = d.TransferDate,
                            StrTransferDate = "",
                            TransferTime = d.TransferTime,
                            JobOrderId = d.JobOrderId,
                            ReceiveTo = d.ReceiveTo,
                            Reason = d.Reason,
                            CarType = d.CarType,
                            Company = d.Company,
                            CarNo = d.CarNo,
                            CarBrand = d.CarBrand,
                            SendToDepartment = d.SendToDepartment,
                            Remark = d.Remark,
                            EmpId = d.EmpId,
                            BillNo = d.BillNo,
                            TransferStatus = d.TransferStatus,
                            StrTransferStatus = Converting.TransferStatus(d.TransferStatus.ToString()),
                            Note1 = d.Note1,
                            CreatedDate = d.CreatedDate,
                            UpdatedDate = d.UpdatedDate
                        });
            return data.ToList();
        }

        public List<TransferHeader> Gets(int page = 1, int size = 0, string src = "",string TransferType="")
        {
            var data = (from d in db.TransferHeader.Where(o=>o.TransferType== TransferType) 
                        select new TransferHeader {
 TransferId= d.TransferId,
  TransferNo = d.TransferNo,
 TransferType= d.TransferType,
  TransferDate= d.TransferDate,
   TransferTime =d.TransferTime,
    JobOrderId =d.JobOrderId,
     ReceiveTo=d.ReceiveTo,
     Reason=d.Reason,
      CarType=d.CarType,
      Company=d.Company,
       CarNo=d.CarNo,
       CarBrand=d.CarBrand,
      SendToDepartment=d.SendToDepartment,
      Remark=d.Remark,
     EmpId=d.EmpId,
     BillNo=d.BillNo,
     TransferStatus=d.TransferStatus,
    Note1=d.Note1,
     CreatedDate=d.CreatedDate,
       UpdatedDate =d.UpdatedDate

                        }).OrderByDescending(c => c.TransferNo)
                .AsQueryable();

          //  if (month > 0) { data = data.Where(x => x.TransferDate.Value.Month == month); }

            if (size > 0)
            { data = data.Skip((page - 1) * size).Take(size); }
            return data.ToList();
        }
        public bool IsExist(string id)
        {
             return db.TransferHeader.Where(x => x.TransferNo == id).Count() > 0 ? true : false; 
        }

        public bool IsExist(int id)
        {
            return db.TransferHeader.Where(x => x.TransferId == id).Count() > 0 ? true : false;
        }

        public void Set(TransferHeader ob)
        {
            TrasferInHeaderAdd(ob);
         /*   if (ob.TransferId == 0)
            { db.TransferHeader.Add(ob); }
            else { db.Entry(ob).State = EntityState.Modified; }*/
        }

        public TransferDetail GetDetail(int id = 0)
        {
            // var detail = (from d in db.TransferDetail.Where(x => x.TransferId == id) select d);

            //  if (detail == null)
            //  detail = new TransferDetail();

            return new TransferDetail();
        }


        public TransferDetail GetDetail(int transferId, int seq)
        {
            var detail = (from d in db.TransferDetail.Where(x => x.TransferId == transferId && x.Seq == seq) select new TransferDetail {


       TransferId =d.TransferId,

       Seq=d.Seq,
       ProductId =d.ProductId,
       CurrentQty=d.CurrentQty,
        RequestQty=d.RequestQty,
        RequestUnit=d.RequestUnit,
        RequestUnitFactor =d.RequestUnitFactor,
         LastModified = d.LastModified
            });

            //  if (detail == null)
            //  detail = new TransferDetail();

            return detail.FirstOrDefault();
        }

        public void SetDetail(TransferDetail ob)
        {
            db.TransferDetail.Add(ob);
            /* if (ob.TransferId == 0)
             { db.TransferDetail.Add(ob); }
             else { db.Entry(ob).State = EntityState.Modified; }*/
        }

        public bool Add(TransferHeader ob)
        {
            bool result = false;
            string sql = "sp_TransferHeader_Insert";


            List<SqlParameter> paramList = new List<SqlParameter>();





            try
            {
            /*    @TransferId int

          , @TransferNo nvarchar(15)
           ,@TransferType nvarchar(3)
           ,@TransferDate nvarchar(50)
           ,@TransferTime nvarchar(5)
           ,@JobOrderId int
           , @RefTransferId int
 
            , @ReceiveTo nvarchar(50)
           ,@Reason nvarchar(100)
           ,@CarType int
           , @Company nvarchar(100)
           ,@CarNo nchar(10)
           ,@CarBrand nchar(10)
           ,@SendToDepartment int
           , @Remark nvarchar(100)
           ,@EmpId nvarchar(10)
           ,@BillNo nvarchar(50)
           ,@TransferStatus int
           , @Note1 varchar(50)
           ,@CreatedDate datetime
           , @CreatedBy int
 
            , @UpdatedDate datetime
           ,@UpdatedBy int
           , @ApprovedBy int
           */

                 paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@TransferId", ob.TransferId));
                paramList.Add(new SqlParameter("@TransferType", ob.TransferType));
                paramList.Add(new SqlParameter("@TransferDate", ob.TransferDate));
                paramList.Add(new SqlParameter("@TransferTime", ob.TransferTime));
                paramList.Add(new SqlParameter("@JobOrderId", ob.JobOrderId));
                paramList.Add(new SqlParameter("@RefTransferId", ob.RefTransferId));


                paramList.Add(new SqlParameter("@ReceiveTo", ob.ReceiveTo));
           paramList.Add(new SqlParameter("@Reason", ob.Reason));
                paramList.Add(new SqlParameter("@CarType", ob.CarType));
                paramList.Add(new SqlParameter("@Company", ob.Company));
                paramList.Add(new SqlParameter("@CarNo", ob.CarNo));
                paramList.Add(new SqlParameter("@CarBrand", ob.CarBrand));
                paramList.Add(new SqlParameter("@SendToDepartment", ob.SendToDepartment));
                paramList.Add(new SqlParameter("@Remark", ob.Remark));
                paramList.Add(new SqlParameter("@EmpId", ob.EmpId));
                paramList.Add(new SqlParameter("@BillNo", ob.BillNo));
                paramList.Add(new SqlParameter("@TransferStatus", ob.TransferStatus));
                paramList.Add(new SqlParameter("@Note1", ob.Note1));
             //   paramList.Add(new SqlParameter("@CreatedDate", ob.CreatedDate));
                paramList.Add(new SqlParameter("@CreatedBy", ob.CreatedBy));


             //   paramList.Add(new SqlParameter("@UpdatedDate", ob.CreatedBy));
        paramList.Add(new SqlParameter("@UpdatedBy", ob.CreatedBy));
      paramList.Add(new SqlParameter("@ApprovedBy", ob.CreatedBy));
                result = webdb.ExcecuteNonQuery(sql, paramList);


                result = webdb.ExcecuteNonQuery(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("TransferDetail.Insert::" + ex.ToString());
            }
            finally
            { }





            return result;
        }


        public bool Add(string TransferId,List<TransferDetail> dtoList)
        {
            bool result = false;
            string sql = "sp_TransferDetail_ClearAll";


            List<SqlParameter> paramList = new List<SqlParameter>();


          

           
            try
            {

                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@TransferId", TransferId));
                result = webdb.ExcecuteNonQuery(sql, paramList);


                if (dtoList != null && dtoList.Count>0)
                {
                    sql = "sp_TransferDetail_Insert";

                    foreach (var dto in dtoList)
                    {
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@TransferId", dto.TransferId));
                        paramList.Add(new SqlParameter("@RefTransferId", dto.RefTransferId));
                        paramList.Add(new SqlParameter("@ProductId", dto.ProductId));
                        paramList.Add(new SqlParameter("@RequestQty", dto.RequestQty));
                        paramList.Add(new SqlParameter("@CurrentQty", dto.CurrentQty));
                        paramList.Add(new SqlParameter("@Seq", dto.Seq));
                        result = webdb.ExcecuteNonQuery(sql, paramList);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("TransferDetail.Insert::" + ex.ToString());
            }
            finally
            { }





            return result;
    }

        public bool TrasferOutApprove(int transferid)
        {
            bool result = false;
            string sql = "sp_TransferOutHeader_Approve";


            List<SqlParameter> paramList = new List<SqlParameter>();



            try
            {
       
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@TransferId", transferid));

                    result = webdb.ExcecuteNonQuery(sql, paramList);
           
            }
            catch (Exception ex)
            {
                throw new Exception("TrasferOutApprove.Approve::" + ex.ToString());
            }
            finally
            { }





            return result;
        }

        public bool TrasferInApprove(int transferid)
        {
            bool result = false;
            string sql = "sp_TransferInHeader_Approve";


            List<SqlParameter> paramList = new List<SqlParameter>();



            try
            {

                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@TransferId", transferid));

                result = webdb.ExcecuteNonQuery(sql, paramList);

            }
            catch (Exception ex)
            {
                throw new Exception("TrasferInApprove.Approve::" + ex.ToString());
            }
            finally
            { }


            return result;
        }


        public bool TrasferInHeaderAdd(TransferHeader header)
        {
            bool result = false;
            string sql = "sp_TransferInHeader_Add";


            List<SqlParameter> paramList = new List<SqlParameter>();



            try
            {

             /*    [TransferId]
      ,[TransferNo]
      ,[TransferType]
      ,[TransferDate]
      ,[TransferTime]
      ,[JobOrderId]
      ,[RefTransferId]
      ,[ReceiveTo]
      ,[Reason]
      ,[CarType]
      ,[Company]
      ,[CarNo]
      ,[CarBrand]
      ,[SendToDepartment]
      ,[Remark]
      ,[EmpId]
      ,[BillNo]
      ,[TransferStatus]
      ,[Note1]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[UpdatedDate]
      ,[UpdatedBy]
      ,[ApprovedBy]*/

        paramList = new List<SqlParameter>();
                SqlParameter pvNewId = new SqlParameter("@TransferId", SqlDbType.NVarChar,20);
                pvNewId.Value = header.TransferId;
                pvNewId.Direction = ParameterDirection.InputOutput;


                paramList.Add(pvNewId);
                paramList.Add(new SqlParameter("@TransferNo", header.TransferNo));
                paramList.Add(new SqlParameter("@TransferType", header.TransferType));
                paramList.Add(new SqlParameter("@TransferDate", header.TransferDate));
                paramList.Add(new SqlParameter("@TransferTime", header.TransferTime));
                paramList.Add(new SqlParameter("@JobOrderId", header.JobOrderId));
                paramList.Add(new SqlParameter("@RefTransferId", header.RefTransferId));
                paramList.Add(new SqlParameter("@ReceiveTo", header.ReceiveTo));
                paramList.Add(new SqlParameter("@Reason", header.Reason));
                paramList.Add(new SqlParameter("@CarType", header.CarType));
                paramList.Add(new SqlParameter("@Company", header.Company));
                paramList.Add(new SqlParameter("@CarNo", header.CarNo));
                paramList.Add(new SqlParameter("@CarBrand", header.CarBrand));
                paramList.Add(new SqlParameter("@SendToDepartment", header.SendToDepartment));
                paramList.Add(new SqlParameter("@Remark", header.Remark));
                paramList.Add(new SqlParameter("@EmpId", header.EmpId));
                paramList.Add(new SqlParameter("@BillNo", header.BillNo));
                paramList.Add(new SqlParameter("@TransferStatus", header.TransferStatus));
                paramList.Add(new SqlParameter("@Note1", header.Note1));
                paramList.Add(new SqlParameter("@CreatedBy", header.CreatedBy));
                paramList.Add(new SqlParameter("@UpdatedBy", header.UpdatedBy));
                webdb.ExcecuteWitTranNonQuery(sql, paramList);
               // header.TransferId = Convert.ToInt32(webdb.ExcecuteNonScalar(sql, paramList));
              header.TransferId = Convert.ToInt32(pvNewId.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("TrasferInApprove.Approve::" + ex.ToString());
            }
            finally
            { }


            return result;
        }


        public void UpdateDetail(TransferDetail ob)
        {
            db.Entry(ob).State = EntityState.Modified;
            /* if (ob.TransferId == 0)
             { db.TransferDetail.Add(ob); }
             else { db.Entry(ob).State = EntityState.Modified; }*/
        }
    }
}

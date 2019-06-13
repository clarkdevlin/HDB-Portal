using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class PolicyHolder
    {
        public string TransactionNo { get; set; }
        public string FullName { get; set; }
        public string NRICFIN { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }

        public enum ReferenceType
        {
            ByTransactionNo,
            ByHDBReferenceNo,
            ByNRICFIN,
            ByFullName
        }

        public bool GetPolicyHolder(ReferenceType referenceType, string searchReference)
        {
            try
            {
                PolicyHolder output = null; ;
                switch (referenceType)
                {
                    case ReferenceType.ByTransactionNo:
                        using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                        {
                            output = cnn.QueryFirstOrDefault<PolicyHolder>(@"select *
                                                               from dbo.[TRANSACTIONPOLICYHOLDER]
                                                               where TransactionNo = @TransactionNo", new { TransactionNo = searchReference });
                        }
                        break;
                    case ReferenceType.ByHDBReferenceNo:
                        using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                        {
                            output = cnn.QueryFirstOrDefault<PolicyHolder>(@"select *
                                                               from dbo.[TRANSACTIONPOLICYHOLDER]
                                                               where HDBReferenceNo = @HDBReferenceNo", new { HDBReferenceNo = searchReference });
                        }
                        break;
                    case ReferenceType.ByNRICFIN:
                        using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                        {
                            output = cnn.QueryFirstOrDefault<PolicyHolder>(@"select *
                                                               from dbo.[TRANSACTIONPOLICYHOLDER]
                                                               where NRICFIN = @NRICFIN", new { NRICFIN = searchReference });
                        }
                        break;
                    case ReferenceType.ByFullName:
                        using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                        {
                            output = cnn.QueryFirstOrDefault<PolicyHolder>(@"select *
                                                               from dbo.[TRANSACTIONPOLICYHOLDER]
                                                               where FullName like '%@FullName%'", new { FullName = searchReference });
                        }
                        break;
                    default:
                        break;
                }

                this.TransactionNo = output.TransactionNo;
                this.FullName = output.FullName;
                this.NRICFIN = output.NRICFIN;
                this.Email = output.Email;
                this.Mobile = output.Mobile;
                this.DateOfBirth = output.DateOfBirth;
                this.Gender = output.Gender;

                return true;
            }
            catch (Exception ex)
            {
                //TODO: log error
                return false;
            }
        }

        public bool SavePolicyHolder(IDbConnection cnn, IDbTransaction trans)
        {
            try
            {
                var p = new
                {
                    TransactionNo,
                    FullName,
                    NRICFIN,
                    Email,
                    Mobile,
                    DateOfBirth,
                    Gender
                };

                cnn.Execute(@"insert into dbo.TRANSACTIONSPOLICYHOLDER
                                      values (
                                            @TransactionNo,
                                            @FullName,
                                            @NRICFIN,
                                            @Email,
                                            @Mobile,
                                            @DateOfBirth,
                                            @Gender
                                            )", p, transaction: trans);

                return true;
            }
            catch (Exception ex)
            {

                //TODO: log error
                return false;
            }

        }
        
        public bool SavePolicyHolder()
        {
            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                try
                {
                    var p = new
                    {
                        TransactionNo,
                        FullName,
                        NRICFIN,
                        Email,
                        Mobile,
                        DateOfBirth,
                        Gender
                    };

                    cnn.Execute(@"insert into dbo.[TRANSACTIONSPOLICYHOLDER]
                                      values (
                                            @TransactionNo,
                                            @FullName,
                                            @NRICFIN,
                                            @Email,
                                            @Mobile,
                                            @DateOfBirth,
                                            @Gender
                                            )", p);

                    return true;
                }
                catch (Exception ex)
                {
                    //TODO: log error
                    return false;
                }
            }
        }
    }
}

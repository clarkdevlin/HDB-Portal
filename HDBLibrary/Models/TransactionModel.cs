using Dapper;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Reporting;
using Telerik.Reporting.Processing;

namespace HDBLibrary.Models
{
    public static class TransactionModel
    {
        public static List<Transaction> GetTransactions()
        {
            var sql = @"select *
                        from db.[TRANSACTIONS]";

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                return cnn.Query<Transaction>(sql).ToList();
            }
        }

        public struct ReferenceNos
        {
            public string TransactionNo;
            public string ReceiptNo;
            public string PolicyNo;
            public string CertificateNo;
        }

        public struct TransactionOuput
        {
            public ReferenceNos HDB;
            public ReferenceNos HC;
        }

        public static TransactionOuput CreateTransaction(APITransaction apiModel)
        {
            var output = new TransactionOuput();

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                cnn.Open();
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        var userId = Thread.CurrentPrincipal.Identity.Name;

                        if (apiModel.HDB != null)
                        {
                            //get new transaction number from series table
                            var transactionNo = GetNewNo(cnn, trans, "TransactionNo");
                            //// get new policy number from series table
                            //var policyNo = GetNewNo(cnn, trans, "PolicyNo");

                            //// get new policy number from series table
                            //var receiptNo = GetNewNo(cnn, trans, "ReceiptNo");

                            //create the transaction number with formating.
                            apiModel.HDB.TransactionNo = $"{transactionNo.Prefix}{DateTime.Today:yy}{transactionNo.CurrentNo:00000}";

                            apiModel.HDB.EntryDate = DateTime.Now;
                            apiModel.HDB.EntryUserId = new Guid(userId);

                            //create the policy number with formating.
                            //apiModel.HDB.PolicyNo = $"{policyNo.Prefix}{DateTime.Today:yy}{policyNo.CurrentNo:00000}";
                            apiModel.HDB.PolicyNo = "HDBMASTER";
                            ////create the receipt number with formating.
                            //apiModel.HDB.ReceiptNo = $"{receiptNo.Prefix}{DateTime.Today:yy}{receiptNo.CurrentNo:00000}";
                            apiModel.HDB.ReceiptNo = $"RCP{apiModel.HDB.HDBReferenceNo}";
                            apiModel.HDB.CertificateNo = apiModel.HDB.HDBReferenceNo;

                            //insert first the transaction record
                            InsertHDBTransaction(apiModel.HDB, cnn, trans);
                            apiModel.HDB.PolicyHolder.TransactionNo = apiModel.HDB.TransactionNo;

                            apiModel.HDB.PolicyHolder.SavePolicyHolder(cnn, trans);
                            //then update the HDBMaster table
                            UpdateHDBMasterRecord(apiModel.HDB, cnn, trans);

                            output.HDB.TransactionNo = apiModel.HDB.TransactionNo;
                            output.HDB.ReceiptNo = apiModel.HDB.ReceiptNo;
                            output.HDB.PolicyNo = apiModel.HDB.PolicyNo;
                            output.HDB.CertificateNo = apiModel.HDB.CertificateNo;
                        }

                        if (apiModel.HC != null)
                        {
                            var transactionNo = GetNewNo(cnn, trans, "TransactionNo");
                            var policyNo = GetNewNo(cnn, trans, "HCPolicyNo");
                            var receiptNo = GetNewNo(cnn, trans, "ReceiptNo");
                            var certificateNo = GetNewNo(cnn, trans, "CertificateNo");

                            apiModel.HC.TransactionNo = $"{transactionNo.Prefix}{DateTime.Today:yy}{transactionNo.CurrentNo:00000}";
                            apiModel.HC.PolicyNo = $"{policyNo.Prefix}{DateTime.Today:yy}{policyNo.CurrentNo:00000}";
                            apiModel.HC.ReceiptNo = $"{receiptNo.Prefix}{DateTime.Today:yy}{receiptNo.CurrentNo:00000}";
                            apiModel.HC.CertificateNo = $"{certificateNo.Prefix}{DateTime.Today:yy}{certificateNo.CurrentNo:00000}";

                            apiModel.HC.EntryDate = DateTime.Now;
                            apiModel.HC.EntryUserId = new Guid(userId);

                            InsertHCTransaction(apiModel.HC, cnn, trans);
                            apiModel.HC.PolicyHolder.TransactionNo = apiModel.HC.TransactionNo;
                            apiModel.HC.PolicyHolder.SavePolicyHolder(cnn, trans);

                            output.HC.TransactionNo = apiModel.HC.TransactionNo;
                            output.HC.ReceiptNo = apiModel.HC.ReceiptNo;
                            output.HC.PolicyNo = apiModel.HC.PolicyNo;
                            output.HC.CertificateNo = apiModel.HC.CertificateNo;
                        }

                        //the line below will be reached and records will be committed and table locks will be released
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        //on any error it will rollback the changes and release table locks.
                        trans.Rollback();
                        //TODO: log error
                    }

                    return output;
                }
            }
        }

        public enum DocumentType
        {
            HDBFireCertificate
        }


        public static bool CreateDocument(Hashtable prms, DocumentType documentType,string fileNameReference = null)
        {
            try
            {
                var uriReportSource = new UriReportSource();
                var templatePath = DBHelper.GetAppSettings("DocumentTemplatePath");

                uriReportSource.Uri = $@"{templatePath}\{documentType.ToString()}.trdp";

                foreach (DictionaryEntry prm in prms)
                {
                    uriReportSource.Parameters.Add($"{prm.Key}", prm.Value);
                }

                var reportProcessor = new ReportProcessor();

                var result = reportProcessor.RenderReport("PDF", uriReportSource, new System.Collections.Hashtable());
                var fileName = $"HDBCerticate_{fileNameReference}.{result.Extension}";
                var certPath = $@"{DBHelper.GetAppSettings("CertificatesPath")}\{DateTime.Today:yyyy}\{DateTime.Today:MMM}";

                if (!Directory.Exists(certPath)) Directory.CreateDirectory(certPath);

                var certFile = System.IO.Path.Combine(certPath, fileName);
                var pdfSource = certFile.Replace(".pdf", "_p.pdf");
                var pdfProtected = certFile;

                using (var fs = new FileStream(pdfSource, FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }
                //PROTECT THE PDF FILE WITH PDFSharp
                PdfDocument document = PdfReader.Open(pdfSource);
                PdfSecuritySettings securitySettings = document.SecuritySettings;

                // Setting one of the passwords automatically sets the security level to 
                // PdfDocumentSecurityLevel.Encrypted128Bit.
                securitySettings.UserPassword = "";
                securitySettings.OwnerPassword = "JVH051377";

                // Don't use 40 bit encryption unless needed for compatibility reasons
                //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

                // Restrict some rights.
                securitySettings.PermitAccessibilityExtractContent = false;
                securitySettings.PermitAnnotations = false;
                securitySettings.PermitAssembleDocument = false;
                securitySettings.PermitExtractContent = false;
                securitySettings.PermitFormsFill = false;
                securitySettings.PermitFullQualityPrint = true;
                securitySettings.PermitModifyDocument = false;
                securitySettings.PermitPrint = true;

                // Save the document...
                document.Save(pdfProtected);
                File.Delete(pdfSource);
                return true;
            }
            catch (Exception ex)
            {
                //TODO: log error
                return false;
            }
        }

        private static void UpdateHDBMasterRecord(HDBTransaction transaction, IDbConnection cnn, IDbTransaction trans = null)
        {
            var uSql = @"update dbo.HDBMASTER
                         set InsuranceEffectiveDate = @InsuranceEffectiveDate, InsuranceTerm = @InsuranceTerm, TransactionType = @TransactionType,
                            PremiumAmount = @PremiumAmount, PremiumAmountGST = @PremiumAmountGST, NameOfInsurer = @NameOfInsurer
                         where HDBReferenceNo = @HDBReferenceNo";

            var p = new
            {
                transaction.HDBReferenceNo,
                transaction.InsuranceEffectiveDate,
                transaction.InsuranceTerm,
                transaction.TransactionType,
                transaction.PremiumAmount,
                transaction.PremiumAmountGST,
                NameOfInsurer = "ECICS LIMITED"
            };

            if (trans != null)
            {
                cnn.Execute(uSql, p, transaction: trans);
            }
            else
            {
                cnn.Execute(uSql, p);
            }
            
        }

        private static void InsertHDBTransaction(HDBTransaction transaction, IDbConnection cnn, IDbTransaction trans = null)
        {
            var iSql = @"insert into dbo.[TRANSACTIONSHDB]
                         values (
                                @TransactionNo,
                                @ReceiptNo,
                                @TransactionType,
                                @HDBReferenceNo,
                                @PolicyNo,
                                @CertificateNo,
                                @InsuranceEffectiveDate,
                                @InsuranceTerm,
                                @SumInsured,
                                @PremiumAmount,
                                @PremiumAmountGST,
                                @PremiumDiscountAmount,
                                @TotalPremiumAmount,
                                @IssueId,
                                @PaymentMethod,
                                @PaymentChequeNo,
                                @PaymentCreditCardNo,
                                @PaymentReferenceNo,
                                @PaymentAmount,
                                @EntryUserId,
                                @EntryDate
                                )";

            if (trans != null)
            {
                cnn.Execute(iSql, transaction, transaction: trans);
            }
            else
            {
                cnn.Execute(iSql, transaction);
            }
        }

        private static void InsertHCTransaction(HCTransaction transaction, IDbConnection cnn, IDbTransaction trans = null)
        {
            var iSql = @"insert into dbo.[TRANSACTIONSHC]
                         values (
                                @TransactionNo,
                                @ReceiptNo,
                                @HDBReferenceNo,
                                @PolicyNo,
                                @AddressBlk,
                                @AddressLevel,
                                @AddressUnit,
                                @AddressStreetName,
                                @AddressPostalCode,
                                @PlanName,
                                @Section1SumInsured,
                                @Section2SumInsured,
                                @Section3SumInsured,
                                @TotalSumInsured,
                                @PremiumAmount,
                                @PremiumAmountGST,
                                @PremiumDiscountAmount,
                                @TotalPremiumAmount,
                                @PromoCode,
                                @InsuranceEffectiveDate,
                                @InsuranceTerm,
                                @IssueId,
                                @PaymentMethod,
                                @PaymentChequeNo,
                                @PaymentCreditCardNo,
                                @PaymentReferenceNo,
                                @PaymentAmount,
                                @EntryUserId,
                                @EntryDate
                                )";

            if (trans != null)
            {
                cnn.Execute(iSql, transaction, transaction: trans);
            }
            else
            {
                cnn.Execute(iSql, transaction);
            }
        }

        private static Series GetNewNo(IDbConnection cnn, IDbTransaction trans, string module)
        {
            var p = new { Module = module };
            var transaction = cnn.QueryFirstOrDefault<Series>(@"select * 
                                                                  from dbo.[SERIES]
                                                                  where Module = @Module", p, transaction: trans);

            //reset current number to 0 after every new year and set current year to new year
            if (transaction.CurrentYear != DateTime.Today.Year)
            {
                transaction.CurrentYear = DateTime.Today.Year;
                transaction.CurrentNo = 0;
            }
            //add 1 to current number
            transaction.CurrentNo++;

            //update current number so it would lock the table because of transaction isolation level read committed
            cnn.Execute(@"update dbo.[SERIES]
                          set CurrentYear = @CurrentYear, CurrentNo = @CurrentNo
                          where Module = @Module", transaction, transaction: trans);
            return transaction;
        }

       

    }
}

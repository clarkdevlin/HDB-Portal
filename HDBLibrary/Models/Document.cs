using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Reporting;
using Telerik.Reporting.Processing;

namespace HDBLibrary.Models
{
    public class Document : IDisposable
    {
        #region IDisposable Implementation
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
            }
            this.disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        //private void GenerateDocument()
        //{
        //    try
        //    {
        //        var uriReportSource = new UriReportSource();
        //        var server = (Convert.ToBoolean(HelperB2B.GetAppSettings("Production")) ? "prd" : "uat");

        //        var tsaGroup = (int)HelperB2B.ExecSql($"SELECT ISNULL(groupSOA,0) FROM {serverName}.dbo.viewTSAMaster WHERE tsaCode = '{TSACode}'", CommandType.Text, HelperB2B.dbType.MainDirect);

        //        //uriReportSource.Uri = $"{HelperB2B.GetSettingValue(Environment.MachineName == "PC10030" ? "ReportsTemplatesPathDev" : "ReportsTemplatesPath").Replace("server", server)}\\{docType}.trdp";

        //        string reportPath;
        //        if (tsaGroup == 22)
        //        {
        //            reportPath = "ReportsTemplatesPathWahHong";
        //        }
        //        else
        //        {
        //            reportPath = "ReportsTemplatesPath";
        //        }

        //        uriReportSource.Uri = $"{HelperB2B.GetSettingValue(reportPath).Replace("server", server)}\\{docType}.trdp";
        //        uriReportSource.Parameters.Add(new Telerik.Reporting.Parameter("policy_no", PolicyNo));

        //        var reportProcessor = new ReportProcessor();
        //        var deviceInfo = new Hashtable();
        //        var result = reportProcessor.RenderReport("PDF", uriReportSource, deviceInfo);
        //        var fileName = $"{result.DocumentName}.{result.Extension}";
        //        var path = $"{HelperB2B.GetSettingValue(Environment.MachineName == "PC10030" ? "TempPathDev" : "TempPath").Replace("server", server)}\\{PolicyNo}\\";
        //        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        //        var filePath = System.IO.Path.Combine(path, fileName);
        //        var pdfSource = filePath.Replace(".pdf", "_p.pdf");
        //        var pdfProtected = filePath;

        //        using (var fs = new FileStream(pdfSource, FileMode.Create))
        //        {
        //            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
        //        }
        //        //PROTECT THE PDF FILE WITH PDFSharp
        //        PdfDocument document = PdfReader.Open(pdfSource);
        //        PdfSecuritySettings securitySettings = document.SecuritySettings;

        //        // Setting one of the passwords automatically sets the security level to 
        //        // PdfDocumentSecurityLevel.Encrypted128Bit.
        //        securitySettings.UserPassword = "";
        //        securitySettings.OwnerPassword = "ownerECICSL!mit3d";

        //        // Don't use 40 bit encryption unless needed for compatibility reasons
        //        //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

        //        // Restrict some rights.
        //        securitySettings.PermitAccessibilityExtractContent = false;
        //        securitySettings.PermitAnnotations = false;
        //        securitySettings.PermitAssembleDocument = false;
        //        securitySettings.PermitExtractContent = false;
        //        securitySettings.PermitFormsFill = false;
        //        securitySettings.PermitFullQualityPrint = true;
        //        securitySettings.PermitModifyDocument = false;
        //        securitySettings.PermitPrint = true;

        //        // Save the document...
        //        document.Save(pdfProtected);

        //        if (UploadFileToDB(filePath, PolicyNo, ApprovedDate, result.MimeType))
        //        {
        //            try
        //            {
        //                Directory.Delete(path, true);
        //            }
        //            catch (Exception ex)
        //            {
        //                using (var obj = new ObjErrorLogB2B(DateTime.Now, ApprovedByUId, "ObjPolicyRequestB2B.mGendDocsWorker_DoWork()", ex.Message))
        //                {
        //                    obj.SaveLog();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        using (var obj = new ObjErrorLogB2B(DateTime.Now, ApprovedByUId, "ObjPolicyRequestB2B.GeneratePolicyDocs()", ex.Message))
        //        {
        //            obj.SaveLog();
        //        }
        //    }
        //}
    }
}

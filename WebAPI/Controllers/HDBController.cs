using HDBLibrary;
using HDBLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [RequireHttps]
    [BasicAuthentication]
    public class HDBController : ApiController
    {
        private string username = Thread.CurrentPrincipal.Identity.Name;
          
        // POST /hdb/getvalidhdbrecord
        [HttpPost]
        [Route("hdb/getrecord")]
        public HttpResponseMessage GetValidHDBRecord([FromBody]APIVerifyModel model)
        {
            //How to get username from authenticated API Caller
            var username = Thread.CurrentPrincipal.Identity.Name;

            var output = HDBModel.GetValidHDBRecord(model);

            if (output.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Record not found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, output);
            }
        }

        // GET /hdb/validatehdbrecord
        [HttpGet]
        [Route("hdb/validate")]
        public HttpResponseMessage ValidateHDBRecord(string ReferenceNo, string PostalCode, string Level, string Unit)
        {
            //How to get username from authenticated API Caller
            var username = Thread.CurrentPrincipal.Identity.Name;

            var model = new APIVerifyModel
            {
                HDBReferenceNo = ReferenceNo,
                AddressPostalCode = PostalCode,
                AddressLevel = Level,
                AddressUnit = Unit
            };
            
            var output = HDBModel.GetValidHDBRecord(model);

            if (output.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Record not found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, output);
            }

            //return different message for differnt type of errors
        }

        [HttpGet]
        [Route("hdb/gethdbpremiumbyflattype")]
        public HttpResponseMessage GetHCPlanBySection1(string FlatType)
        {
            var output = new HDBFlatPremium();
            if (output.LoadFlatPremium(FlatType))
            {
                return Request.CreateResponse(HttpStatusCode.OK, output);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Record not found");
            }
            
        }


        [HttpGet]
        [Route("hdb/getallhcplans")]
        public HttpResponseMessage GetAllHCPlans()
        {
            var output = HCPlanPremiumModel.GetHCPlanPremiums();

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [HttpGet]
        [Route("hdb/getplanbyflattype")]
        public HttpResponseMessage GetHCPlanByFlatType(string FlatType)
        {
            var output = new HCPlanPremium();
            if (output.LoadHCPlanPremiumByFlatType(FlatType))
            {
                return Request.CreateResponse(HttpStatusCode.OK, output);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Record not found");
            }
        }

        [HttpGet]
        [Route("hdb/getplanbysection1")]
        public HttpResponseMessage GetHCPlanBySection1(decimal section1)
        {
            var output = new HCPlanPremium();
            if (output.LoadHCPlanPremiumBySection1(section1))
            {
                return Request.CreateResponse(HttpStatusCode.OK, output);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Record not found");
            }
        }


        // POST /hdb/issuepolicy
        [HttpPost]
        [Route("hdb/posttransaction")]
        public HttpResponseMessage PostTransaction([FromBody]APITransaction model)
        {
            
            try
            {
                var output = TransactionModel.CreateTransaction(model);

                if (output.HDB.TransactionNo != null)
                {
                    var p = new Hashtable
                    {
                        {"TransactionNo", output.HDB.TransactionNo}
                    };
                    TransactionModel.CreateDocument(p, TransactionModel.DocumentType.HDBFireCertificate, output.HDB.CertificateNo);
                }

                if (output.HC.TransactionNo != null)
                {
                    var p = new Hashtable
                    {
                        {"TransactionNo", output.HDB.TransactionNo}
                    };
                    //TransactionModel.CreateDocument(p, TransactionModel.DocumentType.HDBFireCertificate, output.HDB.TransactionNo);
                }

                return Request.CreateResponse(HttpStatusCode.OK, output);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                //TODO log error
            }
        }
    }
}

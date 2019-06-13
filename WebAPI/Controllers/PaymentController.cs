using HDBLibrary;
using HDBLibrary.Models.CybersourcePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [RequireHttps]
    public class PaymentController : ApiController
    {
        // POST /pay/getcspaymentsettings
        [HttpPost]
        [BasicAuthentication]
        [Route("pay/getcspaymentsettings")]
        public HttpResponseMessage GetCSPaymentGatewaySettings([FromBody]List<CSParamaters> model)
        {
            try
            {
                var isProduction = bool.Parse(DBHelper.ExecuteScalarSQL<string>("select Value from dbo.SETTINGS where Setting = 'Production'"));

                string secretKey;
                if (isProduction)
                {
                    //Production
                    model.Add(new CSParamaters { Name = "access_key", Value = "cd597bcffad33c6a852f638ff7259d0a" });
                    model.Add(new CSParamaters { Name = "profile_id", Value = "BE90AAB1-00BA-4D45-A3BD-F74C28CCD475" });
                    model.Add(new CSParamaters { Name = "form_action", Value = "https://secureacceptance.cybersource.com/pay" });
                    secretKey = "d4607a05a04445bd88a4155a2eb93605971763019a1745f198378f6e0a4a7e999062bf1a727541a0865d23e0244bd4694465373e608d46c690e1e826894ef4bf902a264fa3a34bef8dc7d8875951715d1c597fd3bbd74a06aa7065d8107f9a576228ea1fd3ac4d0c8b397de1791367f3046e8009985e49d49d9371134703e35b";
                }
                else
                {
                    //Test
                    model.Add(new CSParamaters { Name = "access_key", Value = "d8231af7b87b39c39774066f98c16bf4" });
                    model.Add(new CSParamaters { Name = "profile_id", Value = "E131ED2F-AC0F-4689-A9AB-446DBBF78BB3" });
                    model.Add(new CSParamaters { Name = "form_action", Value = "https://testsecureacceptance.cybersource.com/pay" });
                    secretKey = "75fe31a9be1f461b83fe3715ddfea42e111cab30695c425fa284847b07c044ccef3390b26e414004b3f37be2c76d6b74c3751325e0114fffaeb5c12edf89e4b2d23deef468fc40cf8bda294ef5029ca9a0c990979a5d4bfeaeccb1db3cf4d79cd9863e467f53451c90ab4037956d9ea40b80131267914ca49452ef4fa09f2c91";
                }

               
                model.Add(new CSParamaters { Name = "transaction_uuid", Value = Security.GetUUID() });
                model.Add(new CSParamaters { Name = "signed_date_time", Value = Security.GetUTCDateTime() });
                //model.Add(new CSParamaters { Name = "signed_field_names", Value = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency" });
                model.Add(new CSParamaters { Name = "unsigned_field_names", Value = "" });
                model.Add(new CSParamaters { Name = "locale", Value = "en" });
                model.Add(new CSParamaters { Name = "transaction_type", Value = "sale" });

                var signature = Security.GetSignature(model, secretKey);
                model.Add(new CSParamaters { Name = "signature", Value = signature });

                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPost]
        [Route("pay/return")]
        public HttpResponseMessage CSReturnURL([FromBody]CSReply reply)
        {
            if (reply.reason_code == "100")
            {
                try
                {

                }
                catch (Exception ex)
                {
                    //TODO log error
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
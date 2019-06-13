using HDBLibrary.Models.CybersourcePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var prms = new List<CSParamaters>();

            prms.Add(new CSParamaters { Name = "reference_number", Value = "88866655512" });
            prms.Add(new CSParamaters { Name = "transaction_type", Value = "sale" });
            prms.Add(new CSParamaters { Name = "currency", Value = "SGD" });
            prms.Add(new CSParamaters { Name = "amount", Value = "3.21" });
            prms.Add(new CSParamaters { Name = "locale", Value = "en" });
            prms.Add(new CSParamaters { Name = "access_key", Value = "d8231af7b87b39c39774066f98c16bf4" });
            prms.Add(new CSParamaters { Name = "profile_id", Value = "E131ED2F-AC0F-4689-A9AB-446DBBF78BB3" });
            prms.Add(new CSParamaters { Name = "transaction_uuid", Value = Guid.NewGuid().ToString() });
            prms.Add(new CSParamaters { Name = "signed_date_time", Value = $"{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")}" });
            prms.Add(new CSParamaters { Name = "signed_field_names", Value = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency" });
            prms.Add(new CSParamaters { Name = "unsigned_field_names", Value = "" });
            prms.Add(new CSParamaters { Name = "bill_to_forename", Value = "Jeffrey Hermosa" });
            prms.Add(new CSParamaters { Name = "bill_to_email", Value = "jeffreyhermosa@gmail.com" });
            prms.Add(new CSParamaters { Name = "bill_to_address_line1", Value = "26 #02-15 Canberra Drive" });
            prms.Add(new CSParamaters { Name = "bill_to_address_city", Value = "Singapore" });
            prms.Add(new CSParamaters { Name = "bill_to_address_postal_code", Value = "768428" });
            prms.Add(new CSParamaters { Name = "bill_to_address_country", Value = "SG" });
            prms.Add(new CSParamaters { Name = "signature", Value = Security.GetSignature(prms)});

            foreach (var prm in prms)
            {
                Console.WriteLine($"{prm.Name} = {prm.Value}");
            }

            Console.ReadKey();
        }
    }
}

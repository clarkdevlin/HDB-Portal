using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models.CybersourcePayment
{
    public class CSReply
    {
        public string transaction_id { get; set; }
        public string decision { get; set; }
        public string message { get; set; }
        public string req_access_key { get; set; }
        public string req_profile_id { get; set; }
        public string req_transaction_uuid { get; set; }
        public string req_transaction_type { get; set; }
        public string req_reference_number { get; set; }
        public string req_amount { get; set; }
        public string req_tax_amount { get; set; }
        public string req_currency { get; set; }
        public string req_locale { get; set; }
        public string req_payment_method { get; set; }
        public string req_consumer_id { get; set; }
        public string req_bill_to_forename { get; set; }
        public string req_bill_to_surname { get; set; }
        public string req_bill_to_email { get; set; }
        public string req_bill_to_address_line1 { get; set; }
        public string req_bill_to_address_state { get; set; }
        public string req_bill_to_address_country { get; set; }
        public string req_card_number { get; set; }
        public string req_card_type { get; set; }
        public string req_card_expiry_date { get; set; }
        public string reason_code { get; set; }
        public string auth_avs_code { get; set; }
        public string auth_avs_code_raw { get; set; }
        public string auth_response { get; set; }
        public string auth_amount { get; set; }
        public string auth_time { get; set; }
        public string req_payment_token { get; set; }
        public string signed_field_names { get; set; }
        public string signed_date { get; set; }
        public string signature { get; set; }
        public string merchant_defined_data5 { get; set; }
        public string merchant_defined_data6 { get; set; }
        public string merchant_defined_data7 { get; set; }
        public string merchant_defined_data8 { get; set; }
        public string merchant_defined_data9 { get; set; }
        public string merchant_defined_data10 { get; set; }
        public string merchant_defined_data11 { get; set; }
        public string merchant_defined_data12 { get; set; }
        public string merchant_defined_data13 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models.CybersourcePayment
{
    public static class Security
    {
        //Test
        private const string SECRET_KEY = "75fe31a9be1f461b83fe3715ddfea42e111cab30695c425fa284847b07c044ccef3390b26e414004b3f37be2c76d6b74c3751325e0114fffaeb5c12edf89e4b2d23deef468fc40cf8bda294ef5029ca9a0c990979a5d4bfeaeccb1db3cf4d79cd9863e467f53451c90ab4037956d9ea40b80131267914ca49452ef4fa09f2c91";

        //Production
        //private const String SECRET_KEY = "e3f056c76f6441b2aa3481c5a134c006a35d79c6a83f4388b86c3b7e3c0df957011f178aa69b46bda6a6596ad3390e1210b95c11bdf0452d99e46ba397b472e6e1c1a33fb6724b288e88310c3f0a2de98690f941270040f48034c830330f53f26902037fd38041498debf1b3f7fcade633a501a4e6c34004ad70a84ab03049af";

        //public static String sign(IDictionary<string, string> paramsArray)
        //{
        //    return sign(buildDataToSign(paramsArray), SECRET_KEY);
        //}

        public static string GetSignature(List<CSParamaters> paramaters, string secretKey = SECRET_KEY)
        {
            return sign(BuildDataToSign(paramaters),SECRET_KEY);
        }


        private static string sign(string data, string secretKey = SECRET_KEY)
        {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static string BuildDataToSign(List<CSParamaters> paramaters)
        {
            var signedFields = paramaters.Find(x => x.Name == "signed_field_names").Value.Split(',');
            IList<string> dataToSign = new List<string>();
            foreach (var signedField in signedFields)
            {
                dataToSign.Add($"{signedField}={paramaters.Find(x => x.Name == signedField).Value}");
            }

            return commaSeparate(dataToSign);
        }

        //private static String buildDataToSign(IDictionary<string, string> paramsArray)
        //{
        //    String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
        //    IList<string> dataToSign = new List<string>();

        //    foreach (String signedFieldName in signedFieldNames)
        //    {
        //        dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
        //    }

        //    return commaSeparate(dataToSign);
        //}

        private static string commaSeparate(IList<string> dataToSign)
        {
            return string.Join(",", dataToSign);
        }

        public static string GetUUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static string GetUTCDateTime()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            return time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace kdniao.core
{
    public class KDNiaoSDK
    {
        private string appid { get; set; }

        private string secret { get; set; }

        private string api = "http://api.kdniao.cc/Ebusiness/EbusinessOrderHandle.aspx";

        public KDNiaoSDK(string appid, string secret)
        {
            this.appid = appid;
            this.secret = secret;
        }

        /// <summary>
        /// 部分快递公司代码
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> Company()
        {
            return new Dictionary<string, string>()
            {
                {"DBL" ,"德邦"},
                {"EMS" ,"EMS"},
                {"HHTT" ,"天天快递"},
                {"HTKY" ,"百世快递"},
                {"SF" ,"顺丰快递"},
                {"STO" ,"申通快递"},
                {"YD" ,"韵达快递"},
                {"YTO" ,"圆通速递"},
                {"YZPY" ,"邮政平邮"},
                {"ZTO" ,"中通速递"}
            };
        }


        public dynamic Query(string company, string code)
        {
            var client = new HttpClient();
            var RequestData = "{'OrderCode':'','ShipperCode':'"+company+"','LogisticCode':'"+code+"'}";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("RequestData", WebUtility.UrlEncode(RequestData)),
                new KeyValuePair<string, string>("EBusinessID", appid),
                new KeyValuePair<string, string>("RequestType", "1002"),
                new KeyValuePair<string, string>("DataSign", WebUtility.UrlEncode(Encrypt(RequestData,secret))),
                new KeyValuePair<string, string>("DataType", "2"),
            });
            var result = client.PostAsync(api, content).Result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<dynamic>(result);
        }


        private string Encrypt(string body,string secret)
        {
            return Base64(MD5(body + secret));
        }


        private string MD5(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToLower();
            }
        }

        private string Base64(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }




    }
}

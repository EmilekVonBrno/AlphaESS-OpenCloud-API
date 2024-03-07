using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace AlphaESS
{
    class AlphaEssAPI 
    {
        protected const string APIURL = "https://openapi.alphaess.com/api/";
        protected const string SSN = @"ALB001021120008";

        private const string APPID = @"alpha40a32c2da9e52b51";
        private const string APPSECRET = @"86a4b4c0a4ea481f81cdd930f8b61e11";
        
        public AlphaEssAPI()
        {

        }

        private String aPIName;
        private String aPIMethod;
        private String aPIParameters = "?sysSn={0}";
        private String aPIUrl;
        private String aPIData;

        private HttpWebRequest aPIrequest;
        private HttpWebResponse aPIResponse;

        public string APIName { get => aPIName; set => aPIName = value; }
        public string APIMethod { get => aPIMethod; set => aPIMethod = value; }
        public string APIParameters { get => aPIParameters; set => aPIParameters = value; }
        public string APIUrl { get => aPIUrl; set => aPIUrl = value; }
        public string APIData { get => aPIData; set => aPIData = value; }
        protected HttpWebRequest APIrequest { get => aPIrequest; set => aPIrequest = value; }
        protected HttpWebResponse APIResponse { get => aPIResponse; set => aPIResponse = value; }


        protected String CreateGetAPIUrl ()
        {
            return String.Format(APIURL + APIName + APIParameters, SSN);
        }

        protected String CreatePostAPIUrl()
        {
            return String.Format(APIURL + APIName);
        }

        private void CreateRequest()
        {
            aPIrequest = (HttpWebRequest)WebRequest.Create(aPIUrl);
            aPIrequest.Method = aPIMethod;
            aPIrequest.ContentType = "application/json";

            string timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            string sign = APPID + APPSECRET + timeStamp;
            string sha512hash = SHA512hash(sign);

            aPIrequest.Headers.Add("appId", APPID);
            aPIrequest.Headers.Add("timeStamp", timeStamp);
            aPIrequest.Headers.Add("sign", sha512hash);
#if DEBUG
            Logger.LogLine(APPID);
            Logger.LogLine(APPSECRET);
            Logger.LogLine(timeStamp);
            Logger.LogLine(sign);
            Logger.LogLine(sha512hash);
#endif
        }

        protected void CreateGETRequest()
        {
            CreateRequest();
        }

        protected void CreatePOSTRequest()
        {
            CreateRequest();
            using (var streamWriter = new StreamWriter(aPIrequest.GetRequestStream()))
            {
                streamWriter.Write(APIData);
            }
        }

        protected String GetResponse()
        {
            String response = null;
            try
            {
                aPIResponse = (HttpWebResponse)aPIrequest.GetResponse();
                Stream webStream = aPIResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();
                responseReader.Close();
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
            return response;
        }

        protected static string SHA512hash(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (SHA512 hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("x2"));
                return hashedInputStringBuilder.ToString();
            }
        }
        public class APIParams
        {
            public int Code { get; set; }                   //Return Code
            public String Msg { get; set; }                 //Return Message
            public String ExpMsg { get; set; }              //Return Exp Message
            public String Extra { get; set; }               //Return Extra data
        }
        protected class DisChargeConfigInfoParams: APIParams
        {
            public DisChargeConfigInfo? Data { get; set; }  //Data List
        }
        protected class DisChargeConfigInfo
        {
            public String SysSn { get; set; }               //System S/N
            public double BatUseCap { get; set; }           //Discharging Cutoff SOC [%]
            public int CtrDis { get; set; }                 //Enable Battery Discharge Time Control (0/1)
            public String TimeDise1 { get; set; }           //Discharging  Period 1 end time, the time format is HH:mm, such as: 00:00, the maximum is 23:45, the minimum is 00:00, and the interval is 15 minutes, such as: 00:15, 00:30, 00:45, otherwise no effect
            public String TimeDise2 { get; set; }           //Discharging  Period 2 end time, the time format is HH:mm, such as: 00:00, the maximum is 23:45, the minimum is 00:00, and the interval is 15 minutes, such as: 00:15, 00:30, 00:45, otherwise no effect
            public String TimeDisf1 { get; set; }           //Discharging  Period 1 start time, the time format is HH:mm, such as: 00:00, the maximum is 23:45, the minimum is 00:00, and the interval is 15 minutes, such as: 00:15, 00:30, 00:45, otherwise no effect
            public String TimeDisf2 { get; set; }           //Discharging  Period 2 start time, the time format is HH:mm, such as: 00:00, the maximum is 23:45, the minimum is 00:00, and the interval is 15 minutes, such as: 00:15, 00:30, 00:45, otherwise no effect
        }
        protected class ChargeConfigInfoParams: APIParams
        {
            public ChargeConfigInfo? Data { get; set; }     //Data List
        }
        protected class ChargeConfigInfo
        {
            public String SysSn { get; set; }               //System S/N
            public double BatHighCap { get; set; }          //Charging Stops at SOC [%]
            public int GridCharge { get; set; }             //Enable Grid Charging Battery
            public String TimeChae1 { get; set; }           //Charging Period 1 end time, the time format is HH:mm, such as: 00:00, the maximum is 23:45, the minimum is 00:00, and the interval is 15 minutes, such as: 00:15, 00:30, 00:45, otherwise no effect
            public String TimeChae2 { get; set; }           //Charging Period 2 end time, the time format is HH:mm, such as: 00:00, the maximum is 23:45, the minimum is 00:00, and the interval is 15 minutes, such as: 00:15, 00:30, 00:45, otherwise no effect
            public String TimeChaf1 { get; set; }           //Charging Period 1 start time, the time format is HH:mm, such as: 00:00, the maximum is 23:45, the minimum is 00:00, and the interval is 15 minutes, such as: 00:15, 00:30, 00:45, otherwise no effect
            public String TimeChaf2 { get; set; }           //Charging Period 2 start time, the time format is HH:mm, such as: 00:00, the maximum is 23:45, the minimum is 00:00, and the interval is 15 minutes, such as: 00:15, 00:30, 00:45, otherwise no effect
        }
    }
}

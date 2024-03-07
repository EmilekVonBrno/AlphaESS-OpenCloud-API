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
using System.Collections;

namespace AlphaESS
{
    class AlphaEssWebAPI
    {
        protected const string APIURL = "https://api.alphaess.com/ras/v2/";
        protected const string SSN = @"ALB001021120008";

        private const string APPID = @"alpha40a32c2da9e52b51";
        private const string APPSECRET = @"86a4b4c0a4ea481f81cdd930f8b61e11";
        private const string LOGIN = @"PetrBozek";
        private const string EMAIL = @"bozek@centrum.cz";
        private const string PASSWORD = @"eMilek0114";

        public AlphaEssWebAPI()
        {

        }

        private String aPIName;
        private String aPIMethod;
        private String aPIParameters;
        private String aPIUrl;
        private String aPIData;

        private HttpWebRequest aPIrequest;
        private HttpWebResponse aPIResponse;

        public string APIName { get => aPIName; set => aPIName = value; }
        public string APIMethod { get => aPIMethod; set => aPIMethod = value; }
        public string APIParameters { get => aPIParameters; set => aPIParameters = value; }
        public string APIUrl { get => aPIUrl; set => aPIUrl = value; }
        public string APIData { get => aPIData; set => aPIData = value; }
        public HttpWebRequest APIrequest { get => aPIrequest; set => aPIrequest = value; }
        public HttpWebResponse APIResponse { get => aPIResponse; set => aPIResponse = value; }

        public void WebApiLogin()
        {
            this.APIName = "Login";
            this.APIMethod = "POST";
            this.APIUrl = CreateAPIUrl();
            Logger.LogLine(APIUrl);

            string timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            SortedList slstParams = new SortedList();
            slstParams.Add("api_account ", APPID.ToString());
            slstParams.Add("timestamp", timeStamp.ToString());
            slstParams.Add("username", LOGIN);
            slstParams.Add("email", EMAIL);
            slstParams.Add("secretkey", APPSECRET);
            StringBuilder strParams = new StringBuilder();
            for (var i = 0; i < slstParams.Count; i++)
            {
                strParams.AppendFormat("{0}={1}", slstParams.GetKey(i), slstParams.GetByIndex(i));
            }
            this.APIData = MD5Helper.GenerateMD5Hash(strParams.ToString());
            Logger.LogLine(this.APIData);

            this.CreatePOSTRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                WebAPIParams jsonResponse = JsonConvert.DeserializeObject<WebAPIParams>(response);
                Logger.LogLine(jsonResponse.ReturnCode);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public String CreateAPIUrl ()
        {
            return String.Format(APIURL + aPIName);
        }

        public void CreateGETRequest()
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

            Logger.LogLine(APPID);
            Logger.LogLine(APPSECRET);
            Logger.LogLine(timeStamp);
            Logger.LogLine(sign);
            Logger.LogLine(sha512hash);
        }

        public void CreatePOSTRequest()
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

            using (var streamWriter = new StreamWriter(aPIrequest.GetRequestStream()))
            {
                /*string APIData = "{\"user\":\"test\"," +
                              "\"password\":\"bla\"}";*/

                streamWriter.Write(APIData);
            }

            Logger.LogLine(APPID);
            Logger.LogLine(APPSECRET);
            Logger.LogLine(timeStamp);
            Logger.LogLine(sign);
            Logger.LogLine(sha512hash);
        }

        public String GetResponse()
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

        public static string EncryptStringToBytes_Aes(string plainText)
        {
            string encrypted = string.Empty;
            byte[] clearBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes aesAlg = Aes.Create())
            {
                byte[] k;
                byte[] iv;
                byte[] bytes = Encoding.UTF8.GetBytes(APPSECRET);
                k = SHA256.Create().ComputeHash(bytes);
                iv = MD5.Create().ComputeHash(bytes);
                aesAlg.Key = k;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(clearBytes, 0, clearBytes.Length);
                    }
                    encrypted = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            return encrypted;
        }

        public class MD5Helper
        {
            public static string GenerateMD5Hash(string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new ArgumentException("argument cannot be null", "input");
                }
                using (MD5 md5Hash = MD5.Create())
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                    {
                        sb.Append(data[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
        }

        public class WebAPIParams
        {
            public int ReturnCode { get; set; }                 //Return Code
            public String UserType { get; set; }                //Return Message
            public String Token { get; set; }                   //Authentication token (the timeout period is 90 minutes
            //public String Extra { get; set; }                 //Return Extra data
        }
    }
}

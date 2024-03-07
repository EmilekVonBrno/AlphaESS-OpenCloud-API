using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetVerificationCode : AlphaEssAPI
    {
        /*
         * According to SN get the check code according to SN
        */

        private String checkCode;
        public string CheckCode { get => checkCode; set => checkCode = value; }

        public GetVerificationCode(string checkCode)
        {
            this.CheckCode = checkCode; 
            GetVerificationCodeRun();
        }

        public void GetVerificationCodeRun()
        {
            this.APIName = "getVerificationCode";
            this.APIParameters = "?sysSn={0}&checkCode={1}";
            this.APIMethod = "GET";
            this.APIUrl = CreateAPIUrl(CheckCode);
            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                VerificationCodeParams jsonResponse = JsonConvert.DeserializeObject<VerificationCodeParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public String CreateAPIUrl(String checkCode)
        {
            return String.Format(APIURL + APIName + APIParameters, SSN, checkCode);
        }

        public class VerificationCodeParams : APIParams
        {
            public String? Data { get; set; }
        }
    }
}

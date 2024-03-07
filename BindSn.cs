using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class BindSn : AlphaEssAPI
    {

        /*
         * According to SN and check code Bind the system bind the system
        */

        public BindSn()
        {
            this.APIName = "bindSn";
            this.APIMethod = "POST";
            this.APIUrl = CreatePostAPIUrl();
            Logger.LogLine(APIUrl);

            BindSnInfo requestData = new BindSnInfo();
            requestData.SysSn = SSN;
            requestData.Code = "";

            this.APIData = JsonConvert.SerializeObject(requestData, Formatting.Indented);
            Logger.LogLine(this.APIData);

            this.CreatePOSTRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                BindSnReturnParams jsonResponse = JsonConvert.DeserializeObject<BindSnReturnParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class BindSnReturnParams : APIParams
        {
            public String? Data { get; set; }               //Data List
        }

        protected class BindSnInfo
        {
            public String SysSn { get; set; }               //System S/N
            public String Code { get; set; }                //Verification Code
        }

    }
}

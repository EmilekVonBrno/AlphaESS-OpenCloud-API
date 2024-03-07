using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class UnBindSn : AlphaEssAPI
    {
        /*
         * According to SN Unbind the system
        */
        public UnBindSn()
        {
            this.APIName = "unBindSn";
            this.APIMethod = "POST";
            this.APIUrl = CreatePostAPIUrl();
            Logger.LogLine(APIUrl);

            UnBindSnInfo requestData = new UnBindSnInfo();
            requestData.SysSn = SSN;

            this.APIData = JsonConvert.SerializeObject(requestData, Formatting.Indented);
            Logger.LogLine(this.APIData);

            this.CreatePOSTRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                UnBindSnReturnParams jsonResponse = JsonConvert.DeserializeObject<UnBindSnReturnParams>(response);
                Logger.LogLine(jsonResponse.Data.ToString());
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class UnBindSnReturnParams : APIParams
        {
            public String? Data { get; set; }               //Data List
        }

        protected class UnBindSnInfo
        {
            public String SysSn { get; set; }               //System S/N
        }

    }
}

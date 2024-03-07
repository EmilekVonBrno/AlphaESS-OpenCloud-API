using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetDisChargeConfigInfo : AlphaEssAPI
    {
        /*
         * According to SN discharge setting information
        */
        public GetDisChargeConfigInfo()
        {
            this.APIName = "getDisChargeConfigInfo";
            this.APIParameters = "?sysSn={0}";
            this.APIMethod = "GET";
            this.APIUrl = CreateGetAPIUrl();
            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                DisChargeConfigInfoParams jsonResponse = JsonConvert.DeserializeObject<DisChargeConfigInfoParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }
    }
}

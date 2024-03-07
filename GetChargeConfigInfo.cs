using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetChargeConfigInfo : AlphaEssAPI
    {
        /*
         * According SN to get charging setting information
         */
        public GetChargeConfigInfo()
        {
            this.APIName = "getChargeConfigInfo";
            this.APIParameters = "?sysSn={0}";
            this.APIMethod = "GET";
            this.APIUrl = CreateGetAPIUrl();
            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                ChargeConfigInfoParams jsonResponse = JsonConvert.DeserializeObject<ChargeConfigInfoParams>(response);
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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class UpdateChargeConfigInfo : AlphaEssAPI
    {
        /*
         * According SN to Set charging information，Setting frequency 24 hours, set once a day
         */
        public UpdateChargeConfigInfo()
        {
            this.APIName = "updateChargeConfigInfo";
            this.APIMethod = "POST";
            this.APIUrl = CreatePostAPIUrl();
            Logger.LogLine(APIUrl);

            ChargeConfigInfo requestData = new ChargeConfigInfo();
            requestData.SysSn = SSN;
            requestData.GridCharge = 1;
            requestData.BatHighCap = 25;

            requestData.TimeChaf1 = "11:00";
            requestData.TimeChae1 = "14:00";
            requestData.TimeChaf2 = "16:30";
            requestData.TimeChae2 = "17:00";

            this.APIData = JsonConvert.SerializeObject(requestData, Formatting.Indented);
            Logger.LogLine(this.APIData);

            this.CreatePOSTRequest();
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

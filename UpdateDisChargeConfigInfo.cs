using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class UpdateDisChargeConfigInfo : AlphaEssAPI
    {
        /*
         * According to SN Set discharge information，Setting frequency 24 hours, set once a day
        */
        public UpdateDisChargeConfigInfo()
        {
            this.APIName = "updateDisChargeConfigInfo";
            this.APIMethod = "POST";
            this.APIUrl = CreatePostAPIUrl();
            Logger.LogLine(APIUrl);

            DisChargeConfigInfo requestData = new DisChargeConfigInfo();
            requestData.SysSn = SSN;
            requestData.CtrDis = 0;
            requestData.BatUseCap = 25;

            requestData.TimeDisf1 = "11:00";
            requestData.TimeDise1 = "14:00";
            requestData.TimeDisf2 = "16:30";
            requestData.TimeDise2 = "17:30";

            this.APIData = JsonConvert.SerializeObject(requestData, Formatting.Indented);
            Logger.LogLine(this.APIData);

            this.CreatePOSTRequest();
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

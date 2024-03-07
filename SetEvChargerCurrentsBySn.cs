using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class SetEvChargerCurrentsBySn : AlphaEssAPI
    {
        /*
         * Set charging pile household current setting according to SN
         */
        public SetEvChargerCurrentsBySn()
        {
            this.APIName = "setEvChargerCurrentsBySn";
            this.APIMethod = "POST";
            this.APIUrl = CreatePostAPIUrl();
            Logger.LogLine(APIUrl);

            EvChargerCurrentsBySnInfo chargeConfigInfo = new EvChargerCurrentsBySnInfo();
            chargeConfigInfo.SysSn = SSN;
            chargeConfigInfo.Currentsetting = 0;

            this.APIData = JsonConvert.SerializeObject(chargeConfigInfo, Formatting.Indented);
            Logger.LogLine(this.APIData);

            this.CreatePOSTRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                EvChargerCurrentsBySnParams jsonResponse = JsonConvert.DeserializeObject<EvChargerCurrentsBySnParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class EvChargerCurrentsBySnParams : APIParams
        {
            public String? Data { get; set; }  //Data List
        }

        protected class EvChargerCurrentsBySnInfo
        {
            public String SysSn { get; set; }               //System S/N
            public double Currentsetting { get; set; }      //Household current setup
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetEvChargerCurrentsBySn: AlphaEssAPI
    {

        /*
         * Obtain the current setting of charging pile household according to SN
         */

        public GetEvChargerCurrentsBySn()
        {
            this.APIName = "getEvChargerCurrentsBySn";
            this.APIParameters = "?sysSn={0}";
            this.APIMethod = "GET";
            this.APIUrl = CreateGetAPIUrl();

            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
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
            public EvChargerCurrentsBySnData? Data { get; set; }  //Data List
        }

        public class EvChargerCurrentsBySnData
        {
            public String currentsetting { get; set; }         //Household current setup            
        }

    }
}

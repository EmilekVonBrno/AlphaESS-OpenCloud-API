using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetEvChargerStatusBySn: AlphaEssAPI
    {

        /*
         * Obtain charging pile status according to SN+charging pile SN
         */

        public GetEvChargerStatusBySn()
        {
            this.APIName = "getEvChargerStatusBySn";
            this.APIParameters = "?sysSn={0}&evchargerSn={1}";
            this.APIMethod = "GET";
            this.APIUrl = CreateAPIUrl();

            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                EvChargerStatusBySnParams jsonResponse = JsonConvert.DeserializeObject<EvChargerStatusBySnParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public new String CreateAPIUrl()
        {
            return String.Format(APIURL + APIName + APIParameters, SSN,0);
        }

        public class EvChargerStatusBySnParams : APIParams
        {
            public EvChargerStatusBySnData? Data { get; set; }  //Data List
        }

        public class EvChargerStatusBySnData
        {
            public int EvchargerStatus { get; set; }         //EV-charger SN
        }

    }
}

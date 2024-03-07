using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetEvChargerConfigList : AlphaEssAPI
    {
        /*
        * Obtain the SN of the charging pile according to the SN, and set the model
        */

        public GetEvChargerConfigList()
        {
            this.APIName = "getEvChargerConfigList";
            this.APIParameters = "?sysSn={0}";
            this.APIMethod = "GET";
            this.APIUrl = CreateGetAPIUrl();

            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                EvChargerConfigListParams jsonResponse = JsonConvert.DeserializeObject<EvChargerConfigListParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class EvChargerConfigListParams : APIParams
        {
            public EvChargerConfigListData? Data { get; set; }  //Data List
        }

        public class EvChargerConfigListData
        {
            public String evchargerSn { get; set; }         //EV-charger SN
            public String evchargerModel { get; set; }      //EV-charger model
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetOneDateEnergyBySn : AlphaEssAPI
    {
        /*
         * According SN to get System Energy Data
        */
        public GetOneDateEnergyBySn()
        {
            this.APIName = "getOneDateEnergyBySn";
            this.APIParameters = "?sysSn={0}&queryDate={1}";
            this.APIMethod = "GET";
            this.APIUrl = CreateAPIUrl();

            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                OneDateEnergyBySnParams jsonResponse = JsonConvert.DeserializeObject<OneDateEnergyBySnParams>(response);
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
            return String.Format(APIURL + APIName + APIParameters, SSN, DateTime.Today.ToString("yyyy-MM-dd"));
        }

        public class OneDateEnergyBySnParams
        {
            public int Code { get; set; }
            public String Msg { get; set; }
            public OneDateEnergyBySnData? Data { get; set; }
        }

        public class OneDateEnergyBySnData
        {
            public double ECharge { get; set; }
            public double EChargingPile { get; set; }
            public double EDischarge { get; set; }
            public double EGridCharge { get; set; }
            public double EInput { get; set; }
            public double EOutput { get; set; }
            public double Epv { get; set; }
            public String SysSn { get; set; }
            public String TheDate { get; set; }
        }

    }
}

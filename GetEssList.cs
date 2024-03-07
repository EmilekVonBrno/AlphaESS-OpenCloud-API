using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetEssList : AlphaEssAPI
    {
        /*
         * According to SN to get system list data
        */
        public GetEssList()
        {
            this.APIName = "getEssList";
            this.APIMethod = "GET";
            this.APIUrl = CreatePostAPIUrl();
            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                GetEssListParams jsonResponse = JsonConvert.DeserializeObject<GetEssListParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class GetEssListParams: APIParams
        {
            public List<GetEssListData>? Data { get; set; }  //Data List
        }

        public class GetEssListData
        {
            public double Cobat { get; set; }           //battery capacity
            public String EmsStatus { get; set; }       //EMS status
            public String Mbat { get; set; }            //battery model
            public String Minv { get; set; }            //	Inverter model
            public double Poinv { get; set; }           //Inverter nominal Power
            public double Popv { get; set; }            //Pv nominal Power
            public double SurplusCobat { get; set; }    //Battery capacity remaining
            public String SysSn { get; set; }           //System S/N
            public double UsCapacity { get; set; }      //Battery Available Percentage
        }

    }
}

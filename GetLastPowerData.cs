using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetLastPowerData : AlphaEssAPI
    {
        /*
         * Get real-time power data based on SN
         */
        public GetLastPowerData()
        {
            this.APIName = "getLastPowerData";
            this.APIParameters = "?sysSn={0}";
            this.APIMethod = "GET";
            this.APIUrl = CreateGetAPIUrl();

            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                LastPowerDataParams jsonResponse = JsonConvert.DeserializeObject<LastPowerDataParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class LastPowerDataParams : APIParams
        {
            public LastPowerDataData? Data { get; set; }  //Data List
        }

        public class LastPowerDataData
        {
            public double Ppv { get; set; }                     //Pv total power, unit: W
            public PpvDetail PPvDetail { get; set; }            //Data entity
            public double Pload { get; set; }                   //Load, unit: W
            public double Soc { get; set; }                     //soc
            public double Pgrid { get; set; }                   //When pgrid is positive, it means taking electricity from the mains; when pgrid is negative, it means selling electricity. Unit:W
            public PgridDetail PGridDetail { get; set; }        //Data entity
            public double Pbat { get; set; }                    //Battery power, unit: W
            public double PrealL1 { get; set; }                 //
            public double PrealL2 { get; set; }                 //
            public double PrealL3 { get; set; }                 //
            public double Pev { get; set; }                     // Total power of charging pile, unit: W
            public PevDetail PEvDetail { get; set; }            //Data entity
        }

        public class PpvDetail
        {
            public double PPV1 { get; set; }                     //Pv total power, unit: W
            public double PPV2 { get; set; }                     //Pv total power, unit: W
            public double PPV3 { get; set; }                     //Pv total power, unit: W
            public double PPV4 { get; set; }                     //Pv total power, unit: W
            public double PmeterDc { get; set; }                 //Pv total power, unit: W
        }

        public class PgridDetail
        {
            public double PmeterL1 { get; set; }                     //Pv total power, unit: W
            public double PmeterL2 { get; set; }                     //Pv total power, unit: W
            public double PmeterL3 { get; set; }                     //Pv total power, unit: W
        }

        public class PevDetail
        {
            public double EV1Power { get; set; }                     //Pv total power, unit: W
            public double EV2Power { get; set; }                     //Pv total power, unit: W
            public double EV3Power { get; set; }                     //Pv total power, unit: W
            public double EV4Power { get; set; }                     //Pv total power, unit: W
        }

    }
}

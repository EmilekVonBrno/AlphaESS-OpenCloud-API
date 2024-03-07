using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class RemoteControlEvCharger : AlphaEssAPI
    {
        /*
         * According to SN+ charging pile SN remote control charging pile to start charging/stop charging
         */
        public RemoteControlEvCharger()
        {
            this.APIName = "remoteControlEvCharger";
            this.APIMethod = "POST";
            this.APIUrl = CreatePostAPIUrl();
            Logger.LogLine(APIUrl);

            RemoteControlEvChargerInfo requestData = new RemoteControlEvChargerInfo();
            requestData.SysSn = SSN;
            requestData.EvchargerSn = "1";
            requestData.ControlMode = 0;
            this.APIData = JsonConvert.SerializeObject(requestData, Formatting.Indented);
            Logger.LogLine(this.APIData);

            this.CreatePOSTRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                RemoteControlEvChargerParams jsonResponse = JsonConvert.DeserializeObject<RemoteControlEvChargerParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class RemoteControlEvChargerParams : APIParams
        {
            public String? Data { get; set; }  //Data List
        }
        protected class RemoteControlEvChargerInfo
        {
            public String SysSn { get; set; }               //System S/N
            public String EvchargerSn { get; set; }         //EV-charger SN
            public int ControlMode { get; set; }            //0-Stop Charging，1-Start Charging
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaESS
{
    class GetSumDataForCustomer : AlphaEssAPI
    {
        /*
         * According SN to get System Summary data
         */
        public GetSumDataForCustomer()
        {
            this.APIName = "getSumDataForCustomer";
            this.APIParameters = "?sysSn={0}";
            this.APIMethod = "GET";
            this.APIUrl = CreateGetAPIUrl();
            Logger.LogLine(APIUrl);

            this.CreateGETRequest();
            String response = this.GetResponse();
            Logger.LogLine(response);

            try
            {
                SumDataForCustomerReturnParams jsonResponse = JsonConvert.DeserializeObject<SumDataForCustomerReturnParams>(response);
                Logger.LogLine(jsonResponse.Msg);
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class SumDataForCustomerReturnParams : APIParams
        {
            public SumDataForCustomerData? Data { get; set; }  //Data List
        }

        public class SumDataForCustomerData
        {
            public double EPVtoday { get; set; }            //Today’s Generation,unit：kwh
            public double EPVtotal { get; set; }            //Total Generation,unit：kwh
            public double ELoad { get; set; }               //Today’s Load,unit：kwh
            public double EOutput { get; set; }             //Today’s Feed-in,unit：kwh
            public double EInput { get; set; }              //
            public double ECharge { get; set; }             //
            public double EDischarge { get; set; }          //
            public double TodayIncome { get; set; }
            public double TotalIncome { get; set; }
            public double ESelfConsumption { get; set; }
            public double ESelfSufficiency { get; set; }
            public double TreeNum { get; set; }
            public double CarbonNum { get; set; }
            public String MoneyType { get; set; }
        }

    }
}

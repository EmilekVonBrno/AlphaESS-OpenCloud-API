using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace AlphaESS
{
    class GetOneDayPowerBySn : AlphaEssAPI
    {
        /*
         * According SN to get system power data
         */
        private String queryDate;                   //Date，Format：yyyy-MM-dd
        public string QueryDate { get => queryDate; set => queryDate = value; }

        private static GetOneDayPowerBySnReturnParams jsonResponse;
        public GetOneDayPowerBySnReturnParams ResponseData { get => jsonResponse; } //set => jsonResponse = value;

        private static ManualResetEvent allDone = new ManualResetEvent(false);

        public GetOneDayPowerBySn()
        {
            this.QueryDate = DateTime.Today.ToString("yyyy-MM-dd");
        }

        public GetOneDayPowerBySn(string queryDate)
        {
            this.QueryDate = queryDate!=null? queryDate : DateTime.Today.ToString("yyyy-MM-dd");
        }

        public void RunSync()
        {
            this.APIName = "getOneDayPowerBySn";
            this.APIParameters = "?sysSn={0}&queryDate={1}"; //Date，Format：yyyy-MM-dd
            this.APIMethod = "GET";
            this.APIUrl = String.Format(APIURL + APIName + APIParameters, SSN, QueryDate);
#if DEBUG
            Logger.LogLine(this.APIUrl);
#endif
            this.CreateGETRequest();
            String response = this.GetResponse();
#if DEBUG
            Logger.LogLine(response);
#endif

            try
            {
                jsonResponse = JsonConvert.DeserializeObject<GetOneDayPowerBySnReturnParams>(response);
#if DEBUG
                Logger.LogLine(jsonResponse.Msg);
#endif
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine("Method: RunSync");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public void RunASync()
        {
            this.APIName = "getOneDayPowerBySn";
            this.APIParameters = "?sysSn={0}&queryDate={1}"; //Date，Format：yyyy-MM-dd
            this.APIMethod = "GET";
            this.APIUrl = String.Format(APIURL + APIName + APIParameters, SSN, QueryDate);
#if DEBUG
            Logger.LogLine(this.APIUrl);
#endif
            CreateGETRequest();
            //APIrequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), APIrequest); //only for POST method
            APIrequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), APIrequest);
            //allDone.WaitOne();
        }
        private static void GetRequestStreamCallback(IAsyncResult asynchronousResult) //only for POST method
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            Stream postStream = request.EndGetRequestStream(asynchronousResult);

            Console.WriteLine("Please enter the input data to be posted:");
            string postData = "";// Console.ReadLine();

            // Convert the string into a byte array. 
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Write to the request stream.
            postStream.Write(byteArray, 0, postData.Length);
            postStream.Close();

            // Start the asynchronous operation to get the response
            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }

        private static void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();
            Console.WriteLine(responseString);
            // Close the stream object
            streamResponse.Close();
            streamRead.Close();

            // Release the HttpWebResponse
            response.Close();
            //allDone.Set();

            try
            {
                jsonResponse = JsonConvert.DeserializeObject<GetOneDayPowerBySnReturnParams>(responseString);
#if DEBUG
                Logger.LogLine(jsonResponse.Msg);
#endif
            }
            catch (Exception e)
            {
                Logger.LogLine("------------ Error ------------");
                Logger.LogLine("Method: GetResponseCallback");
                Logger.LogLine(e.Message);
                Logger.LogLine("------------ Error ------------");
            }
        }

        public class GetOneDayPowerBySnReturnParams : APIParams
        {
            public List<GetOneDayPowerBySnData>? Data { get; set; }  //Data List
        }

        public class GetOneDayPowerBySnData
        {
            public double Cbat { get; set; }           //battery ?
            public double FeedIn { get; set; }          //Feed-in
            public double gridCharge { get; set; }      //Grid purchase real-time power
            public double Load { get; set; }            //Load
            public double pChargingPile { get; set; }   //Charging pile power
            public double ppv { get; set; }             //PV power
            public String SysSn { get; set; }           //System S/N
            public DateTime UploadTime { get; set; }    //upload Time
        }
    }
}

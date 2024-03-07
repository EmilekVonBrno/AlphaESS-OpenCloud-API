using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
//using System.Text.Json;

namespace AlphaESS
{

    /*
     Developer ID (AppID)	alpha40a32c2da9e52b51 
        The developer ID is the development identification code of AlphaCloud, and the platform interface can be called with the developer key
     Developer key (AppSecret)	8070b6d7e53c45fc8d719550077c1530 
        	The developer key is a password to verify the identity of the AlphaCloud developer, which has extremely high security. 
            When the interface is called, the developer ID, developer key and timestamp need to be encrypted with SHA512
     System SN ALB001021120008
     */


    /*     
     Description -According SN to get System Summary data
     Request URL https://openapi.alphaess.com/api/getSumDataForCustomer 

        Headers Parameters 
    appId       AppID
    timeStamp   Unix timestamp is used to confirm validity of your request. If the timespan between request timestamp and server timestamp exceeds 300 seconds, then the request will be rejected.How to calculate the unix timestamp?(DateTime.UtcNow-new DateTime(1970,1,1)).TotalSeconds，For example，2022-12-20 10:54:06（Beijing Time） ，this timestamp is 1671507191，url：https://tool.lu/timestamp/
    sign        appId+appSecret+timeStamp，then use Sha512 Encryption 。For example：appId：alphaef7900ee81dbbce9，appSecret：c2d2ef6c047c49678e2c332fb2d74c3c，timeStamp：1676353875，before encryption：alphaef7900ee81dbbce9c2d2ef6c047c49678e2c332fb2d74c3c1676353875，SHA512after encryption：0f023c2287b8f6b21b0994947465f8e9de0e1542567b1735bdc6c427336b9b6406285cd94f9215c3e9af958df37fb11c2c9fe792713d8afbdb8c463359a1add8
         
       Request parameter
    sysSn	Yes	string	System S/N
     */

    class Program
    {
        static void Main(string[] args)
        {
            //Program.CreateObject();

            /*AlphaEssWebAPI w = new AlphaEssWebAPI();
            w.WebApiLogin();*/

            //GetOneDayPowerBySn x = new GetOneDayPowerBySn(DateTime.Today.ToString("yyyy-MM-dd"));
            GetOneDayPowerBySn x = new GetOneDayPowerBySn();
            x.QueryDate = DateTime.Today.ToString("yyyy-MM-dd");
            x.RunASync();
            GetDisChargeConfigInfo getDisChargeConfigInfo = new GetDisChargeConfigInfo();
            //Logger.LogLine(x.ResponseData.Data[0].Cbat);

            /*
             * BindSn x = new BindSn();
             * 
             * GetDisChargeConfigInfo getDisChargeConfigInfo = new GetDisChargeConfigInfo();
             * GetEssList x = new GetEssList();
             * 
             * GetEvChargerConfigList evList = new GetEvChargerConfigList();
             * GetEvChargerCurrentsBySn evChargerData = new GetEvChargerCurrentsBySn();
             * GetEvChargerStatusBySn evList = new GetEvChargerStatusBySn();
             * 
             * GetChargeConfigInfo getChargeConfigInfo = new GetChargeConfigInfo();
             * GetLastPowerData x = new GetLastPowerData();
             * GetOneDateEnergyBySn x = new GetOneDateEnergyBySn();
             * GetOneDayPowerBySn x = new GetOneDayPowerBySn();
             * 
             * GetSumDataForCustomer evSumData = new GetSumDataForCustomer();
             * GetVerificationCode x = new GetVerificationCode("111");
             * 
             * RemoteControlEvCharger x = new RemoteControlEvCharger();
             * SetEvChargerCurrentsBySn x = new SetEvChargerCurrentsBySn();
             * 
             * UnBindSn x = new UnBindSn();
             * UpdateDisChargeConfigInfo updateChargeConfigInfo = new UpdateDisChargeConfigInfo(); 
             * UpdateChargeConfigInfo updateChargeConfigInfo = new UpdateChargeConfigInfo();
            */
        }
    }
}

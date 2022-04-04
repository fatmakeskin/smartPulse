using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace smartPulse.Api.Business
{
    public class TradeHelper
    {
        #region Singleton

        private static Lazy<TradeHelper> instance = new Lazy<TradeHelper>(() => new TradeHelper());
        public static TradeHelper Instance => instance.Value;

        #endregion

        #region Members

        HttpClient client = new HttpClient();
        //HttpResponseMessage response;
        string responseFromServer;

        #endregion

        #region Methods
        public string SerializeBody(string data)
        {
            //var dataBody = JsonConvert.DeserializeXmlNode(data);
            //var dat = JsonConvert.SerializeXmlNode(dataBody);
            var datjson = JsonConvert.DeserializeObject(data);
            var a = datjson;
            var dat = JsonConvert.SerializeObject(data);
            var body = data.StartsWith("body");
            return "";
        }

        public string GetData(string begDate, string endDate)
        {
            string url = "https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history?endDate=" + endDate + "&startDate=" + begDate + "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history?endDate=" + endDate + "&startDate=" + begDate + "");
            httpWebRequest.Method = "GET";
            //httpWebRequest.ContentType = "application/json";
            httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = httpWebRequest.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
            }
            SerializeBody(responseFromServer);
            HttpResponseMessage responses = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            HttpContent content = responses.Content;
            if (responses.IsSuccessStatusCode)
            {
                // Parse the response body.
                //TODO: Gelen result düzenlenip body kısmı alınmalı
                var dataObjects = content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                //responses.RequestMessage.Content.
                foreach (var d in dataObjects)
                {
                    //Console.WriteLine("{0}", d);
                }
            }
            //response =  client.GetAsync("https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history?endDate="+endDate+"&startDate="+begDate+"");
            //await Task.FromResult(response.Content);
            return responseFromServer;
        }


        #endregion
        #region Model
        public class IntraDayTradeHistoryList
        {
            public int id { get; set; }
            public string date { get; set; }
            public string contract { get; set; }
            public double price { get; set; }
            public int quantity { get; set; }
        }
        #endregion

    }
}

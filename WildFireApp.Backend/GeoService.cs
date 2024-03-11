using WildFireApp.Backend.Model;
using static WildFireApp.Backend.Model.GeoServer;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Dynamic;
using System.Formats.Asn1;


namespace WildFireApp.Backend
{
    public class GeoService
    {
        public async Task<WildFire> GetWildFireDetails()
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(new URLBuilder().GetUrl());
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }

            if(string.IsNullOrEmpty(response))
                return new WildFire();

            WildFire rootObject = JsonConvert.DeserializeObject<WildFire>(response);

            if(rootObject.numberReturned == 0)
            {
                return new WildFire();
            }

            return rootObject;
        }

        public async Task<WildFire> GetWildFireResultsByMultipleFilters(Dictionary<string, object> customFilters)
        {
            var response = string.Empty;

            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(new URLBuilder().AddCQL(customFilters).GetUrl());
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }

            if (string.IsNullOrEmpty(response))
                return new WildFire();

            GeoServer.WildFire rootObject = JsonConvert.DeserializeObject<WildFire>(response);


            if (rootObject != null && rootObject.numberReturned == 0)
            {
                return new WildFire();
            }

            return rootObject;

        }


        public void DownloadWildFireResults(string data)
        {

        }

       

        //public  DataTable? UseNewtonsoftJson(string sampleJson )
        //{
        //    DataTable? dataTable = new();
        //    if (string.IsNullOrWhiteSpace(sampleJson))
        //    {
        //        return dataTable;
        //    }
        //    var dataSet = JsonConvert.DeserializeObject<DataSet>(sampleJson);
        //     dataTable = dataSet.Tables[0];

        //  //  dataTable = JsonConvert.DeserializeObject<DataTable>(sampleJson);
        //    return dataTable;
        //}
    }
}

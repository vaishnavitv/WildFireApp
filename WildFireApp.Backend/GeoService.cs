using WildFireApp.Backend.Model;
using Newtonsoft.Json;

using static WildFireApp.Backend.Model.GeoServer;

namespace WildFireApp.Backend
{
    public class GeoService
    {
        public async Task<WildFire> GetAllWildFireResults()
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                var urlToFetch = new URLBuilder().GetUrl();
                HttpResponseMessage result = await client.GetAsync(urlToFetch);
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
                var urlToFetch = new URLBuilder().AddCQL(customFilters).GetUrl();
                HttpResponseMessage result = await client.GetAsync(urlToFetch);
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
      
    }
}

using Newtonsoft.Json;
using WildFireApp.Backend.Util;
using static WildFireApp.Backend.Model.GeoServer;

namespace WildFireApp.Backend
{
    public class GeoService
    {
        //TODO: Do away with Strings and use Streams Only.
        public async Task<WildFire> GetAllWildFireResults()
        {
            WildFire rootObject = new WildFire();
            var response = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var urlToFetch = new URLBuilder().GetUrl();
                    HttpResponseMessage result = await client.GetAsync(urlToFetch);
                    if (result.IsSuccessStatusCode)
                    {
                        response = await result.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception _)
            {
                //Ignore, we return next.
            }

            if (string.IsNullOrEmpty(response))
                return rootObject;

            rootObject = JsonConvert.DeserializeObject<WildFire>(response);

            return rootObject;
        }

        public async Task<WildFire> GetWildFireResultsByMultipleFilters(Dictionary<string, object> customFilters)
        {
            WildFire rootObject = new WildFire();
            var response = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    //TODO: Add Validation to Filters.
                    var urlToFetch = new URLBuilder().AddCQL(customFilters).GetUrl();
                    HttpResponseMessage result = await client.GetAsync(urlToFetch);
                    if (result.IsSuccessStatusCode)
                    {
                        response = await result.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception _)
            {
                //Ignore, we return next.
            }

            if (string.IsNullOrEmpty(response))
                return rootObject;

            rootObject = JsonConvert.DeserializeObject<WildFire>(response);

            return rootObject;

        }

    }
}

using System.Text;

namespace WildFireApp.Backend
{
    public class URLBuilder
    {
        string baseUrl = "https://openmaps.gov.bc.ca";
        string baseUri = "/geo/pub/ows";
        Dictionary<string, object> queryParams = new Dictionary<string, object>()
        {
            { "service", "WFS" },
            { "version", "2.0.0" },
            { "request", "GetFeature" },
            { "typeName", "pub:WHSE_LAND_AND_NATURAL_RESOURCE.PROT_CURRENT_FIRE_PNTS_SP" },
            { "outputFormat", "application/json" }
        };

        public string GetUri()
        {
            return string.Concat(baseUri, ToQueryString());
        }
        public string GetUrl()
        {
            return string.Concat(baseUrl, GetUri());
        }

        public URLBuilder AddCQL(Dictionary<string, object> cqlParameters)
        {
            queryParams.Add("cql_filter", new CQLBuilder().WithParameters(cqlParameters).ToQueryString());
            return this;
        }
       
        private string ToQueryString()
        {
            StringBuilder sb = new StringBuilder("?");

            bool first = true;

            foreach (string key in queryParams.Keys)
            {
                if (!first)
                {
                    sb.Append("&");
                }

                sb.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(queryParams[key].ToString()));

                first = false;
            }

            return sb.ToString();
        }
    }
}

using System.Text;

namespace WildFireApp.Backend
{
    public class CQLBuilder
    {
        Dictionary<string, object> queryParams = new Dictionary<string, object>();

        public CQLBuilder WithParameters(Dictionary<string, object> userQueryParams)
        {
            foreach (string key in userQueryParams.Keys)
            {
                queryParams[key] = userQueryParams[key];
            }
            return this;
        }

        public string ToQueryString()
        {
            //Key parameters to come form UI
            bool first = true;

            StringBuilder sb = new StringBuilder();


            foreach (var item in queryParams)
            {
                if (!first)
                {
                    sb.Append(" AND ");
                }

                if (item.Value.GetType().Equals(typeof(int)))
                    sb.Append($"{item.Key}={item.Value}");

                //TODO: Sanitize value to avoid SQL Injection
                if (item.Value.GetType().Equals(typeof(string)))
                    sb.Append($"{item.Key}='{item.Value}'");

                first = false;

            }
            return sb.ToString();
        }

    }
}

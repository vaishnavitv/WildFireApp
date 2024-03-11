using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFireApp.Backend.Model;

namespace WildFireApp.Backend
{
    public class CSVBuilder
    {
        public string JsonToCsv(string jsonContent, string delimeter)
        {
            var csvText = "";
            var response = JsonConvert.DeserializeObject<GeoServer.WildFire>(jsonContent);

            using (TextWriter writer = new StringWriter())
            {
                CsvConfiguration csvConfiguration = new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture);
                csvConfiguration.Delimiter = delimeter;
                using (var csv = new CsvHelper.CsvWriter(writer, csvConfiguration))
                {
                    csv.WriteRecords(response.features);
                }
                csvText = writer.ToString();
            }

            return csvText;
        }
     }
}

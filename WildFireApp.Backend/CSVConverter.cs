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
    public class CSVConverter
    {
        public string JsonToCsv(GeoServer.WildFire givenResults)
        {
            var csvText = "";

            using (TextWriter writer = new StringWriter())
            {
                CsvConfiguration csvConfiguration = new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture);
                using (var csv = new CsvHelper.CsvWriter(writer, csvConfiguration))
                {
                    csv.WriteRecords(givenResults.features);
                }
                csvText = writer.ToString();
            }

            return csvText;
        }
     }
}

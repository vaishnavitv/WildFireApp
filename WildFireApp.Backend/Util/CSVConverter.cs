using CsvHelper;
using CsvHelper.Configuration;

using WildFireApp.Backend.Model;

namespace WildFireApp.Backend.Util
{
    public class CSVConverter
    {
        private CsvConfiguration csvConfiguration = new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture);

        public void JsonToCsv(GeoServer.WildFire wildfire, TextWriter writer)
        {
            if (wildfire?.features == null)
            {
                return;
            }

            ;
            using (var csv = new CsvWriter(writer, csvConfiguration))
            {
                csv.WriteRecords(wildfire.features);
                csv.Flush();
            }
        }
    }
}

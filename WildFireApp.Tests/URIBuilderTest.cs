using System.Globalization;
using WildFireApp.Backend;
namespace WildFireApp.Tests;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class URIBuilderTests
{
    string fireStatus = "Out";
    string fireCause = "Person";
    string geographicDescription = "Goose Creek";
    int fireyear = 2023;

    [Test]
    [TestCase(2023,"Out","Lightning", "Goose Creek", @"FIRE_STATUS='Out' AND FIRE_CAUSE='Lightning' AND FIRE_YEAR=2023 AND GEOGRAPHIC_DESCRIPTION='Goose Creek'")]
    public void CQLFilterTest(int fireYear, string fireStatus, string fireCause, string geographicDescription, string expectedOutput)
    {
        Dictionary<string, object> customFilters = new Dictionary<string, object>();
        customFilters.Add("FIRE_STATUS", fireStatus);
        customFilters.Add("FIRE_CAUSE", fireCause);
        customFilters.Add("FIRE_YEAR", fireYear);
        customFilters.Add("GEOGRAPHIC_DESCRIPTION", geographicDescription);
        string customFilter = new CQLBuilder().WithParameters(customFilters).ToQueryString();
        Assert.That(customFilter, Is.EqualTo(expectedOutput));
    }

    [Test]
    [TestCase(@"https://openmaps.gov.bc.ca/geo/pub/ows?service=WFS&version=2.0.0&request=GetFeature&typeName=pub%3AWHSE_LAND_AND_NATURAL_RESOURCE.PROT_CURRENT_FIRE_PNTS_SP&outputFormat=application%2Fjson")]
    public void URIBuilderTest(string expected)
    {
        string url = new URLBuilder().GetUrl();
        Assert.That(expected, Is.EqualTo(url));
    }

    [Test]
    //Since we have an = sign in the value of the encoded query Parameter cql_filter,
    //It is safe to encode it so that the value remains parseable by conforming HTTP
    //Implementations. This was also tested on a wide matrix of browsers.
    [TestCase(2023, "Out", "Lightning", "Goose Creek", @"https://openmaps.gov.bc.ca/geo/pub/ows?service=WFS&version=2.0.0&request=GetFeature&typeName=pub%3AWHSE_LAND_AND_NATURAL_RESOURCE.PROT_CURRENT_FIRE_PNTS_SP&outputFormat=application%2Fjson&cql_filter=FIRE_STATUS%3D%27Out%27%20AND%20FIRE_CAUSE%3D%27Lightning%27%20AND%20FIRE_YEAR%3D2023%20AND%20GEOGRAPHIC_DESCRIPTION%3D%27Goose%20Creek%27")]
    public void URIBuilderTestWithCQLFilter(int fireYear, string fireStatus, string fireCause, string geographicDescription, string expected)
    {
        Dictionary<string, object> customFilters = new Dictionary<string, object>();
        customFilters.Add("FIRE_STATUS", fireStatus);
        customFilters.Add("FIRE_CAUSE", fireCause);
        customFilters.Add("FIRE_YEAR", fireYear);
        customFilters.Add("GEOGRAPHIC_DESCRIPTION", geographicDescription);

        string completeUrl = new URLBuilder().AddCQL(customFilters).GetUrl();
        Assert.That(expected, Is.EqualTo(completeUrl));
    }


    [Test]
    public void BuildCSVFromJsonTest()
    {
        String delimiter = ",";
        string json =
@"
{
  ""type"": ""FeatureCollection"",
  ""features"": [
    {
      ""type"": ""Feature"",
      ""id"": ""WHSE_LAND_AND_NATURAL_RESOURCE.PROT_CURRENT_FIRE_PNTS_SP.N52664"",
      ""geometry"": {
        ""type"": ""Point"",
        ""coordinates"": [
          1602547.7597,
          525768.5529
        ]
      },
      ""geometry_name"": ""SHAPE"",
      ""properties"": {
        ""FIRE_NUMBER"": ""N52664"",
        ""FIRE_YEAR"": 2023,
        ""RESPONSE_TYPE_DESC"": ""Full"",
        ""IGNITION_DATE"": ""2023-08-09Z"",
        ""FIRE_OUT_DATE"": ""2023-08-11Z"",
        ""FIRE_STATUS"": ""Out"",
        ""FIRE_CAUSE"": ""Lightning"",
        ""FIRE_CENTRE"": 6,
        ""ZONE"": 5,
        ""FIRE_ID"": 2664,
        ""FIRE_TYPE"": ""Fire"",
        ""INCIDENT_NAME"": ""Goose Creek"",
        ""GEOGRAPHIC_DESCRIPTION"": ""Goose Creek"",
        ""LATITUDE"": 49.4292,
        ""LONGITUDE"": -117.6804,
        ""CURRENT_SIZE"": 0.01,
        ""FIRE_URL"": ""https://wildfiresituation.nrs.gov.bc.ca/incidents?fireYear=2023&incidentNumber=N52664"",
        ""FEATURE_CODE"": ""JA70003000"",
        ""OBJECTID"": 12975155,
        ""SE_ANNO_CAD_DATA"": null
      }
    },
    {
      ""type"": ""Feature"",
      ""id"": ""WHSE_LAND_AND_NATURAL_RESOURCE.PROT_CURRENT_FIRE_PNTS_SP.N52764"",
      ""geometry"": {
        ""type"": ""Point"",
        ""coordinates"": [
          1602547.7597,
          525768.5529
        ]
      },
      ""geometry_name"": ""SHAPE"",
      ""properties"": {
        ""FIRE_NUMBER"": ""N52764"",
        ""FIRE_YEAR"": 2023,
        ""RESPONSE_TYPE_DESC"": ""Full"",
        ""IGNITION_DATE"": ""2023-08-09Z"",
        ""FIRE_OUT_DATE"": ""2023-09-08Z"",
        ""FIRE_STATUS"": ""Out"",
        ""FIRE_CAUSE"": ""Lightning"",
        ""FIRE_CENTRE"": 6,
        ""ZONE"": 5,
        ""FIRE_ID"": 2764,
        ""FIRE_TYPE"": ""Fire"",
        ""INCIDENT_NAME"": ""Goose Creek"",
        ""GEOGRAPHIC_DESCRIPTION"": ""Goose Creek"",
        ""LATITUDE"": 49.4292,
        ""LONGITUDE"": -117.6804,
        ""CURRENT_SIZE"": 10.41,
        ""FIRE_URL"": ""https://wildfiresituation.nrs.gov.bc.ca/incidents?fireYear=2023&incidentNumber=N52764"",
        ""FEATURE_CODE"": ""JA70003000"",
        ""OBJECTID"": 12976244,
        ""SE_ANNO_CAD_DATA"": null
      }
    }
  ],
  ""totalFeatures"": 2,
  ""numberMatched"": 2,
  ""numberReturned"": 2,
  ""timeStamp"": ""2024-03-11T01:01:32.436Z"",
  ""crs"": {
    ""type"": ""name"",
    ""properties"": {
      ""name"": ""urn:ogc:def:crs:EPSG::3005""
    }
  }
}
";
        string outputCsv=new CSVBuilder().JsonToCsv(json, delimiter);
        Console.WriteLine(outputCsv);
    }
}

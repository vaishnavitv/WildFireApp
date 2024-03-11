using WildFireApp.Backend.Util;
namespace WildFireApp.Tests;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class URIBuilderTests
{

    [Test]
    [TestCase(2023, "Out", "Lightning", "Goose Creek", @"FIRE_STATUS='Out' AND FIRE_CAUSE='Lightning' AND FIRE_YEAR=2023 AND GEOGRAPHIC_DESCRIPTION='Goose Creek'")]
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
    [TestCase(2023, "Out", "Person", "Myers Frontage Rd", @"https://openmaps.gov.bc.ca/geo/pub/ows?service=WFS&version=2.0.0&request=GetFeature&typeName=pub%3AWHSE_LAND_AND_NATURAL_RESOURCE.PROT_CURRENT_FIRE_PNTS_SP&outputFormat=application%2Fjson&cql_filter=FIRE_STATUS%3D%27Out%27%20AND%20FIRE_CAUSE%3D%27Person%27%20AND%20FIRE_YEAR%3D2023%20AND%20GEOGRAPHIC_DESCRIPTION%3D%27Myers%20Frontage%20Rd%27")]
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

}

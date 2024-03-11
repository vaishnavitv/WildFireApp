using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFireApp.Backend.Model
{
    public class GeoServer
    {
        public class WildFire
        {
            public string type { get; set; }
            public List<Feature> features { get; set; }
            public int totalFeatures { get; set; }
            public int numberMatched { get; set; }
            public int numberReturned { get; set; }
            public DateTime timeStamp { get; set; }
            public Crs crs { get; set; }
        }

        public class Crs
        {
            public string type { get; set; }
            public Properties1 properties { get; set; }
        }

        public class Properties1
        {
            public string name { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public string id { get; set; }
            public Geometry geometry { get; set; }
            public string geometry_name { get; set; }
            public Properties properties { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public float[] coordinates { get; set; }
        }

        public class Properties
        {
            public string FIRE_NUMBER { get; set; }
            public int FIRE_YEAR { get; set; }
            public string RESPONSE_TYPE_DESC { get; set; }
            public string IGNITION_DATE { get; set; }
            public string FIRE_OUT_DATE { get; set; }
            //TODO : Convert to enum
            public string FIRE_STATUS { get; set; }
            public string FIRE_CAUSE { get; set; }
            public int FIRE_CENTRE { get; set; }
            public int ZONE { get; set; }
            public int FIRE_ID { get; set; }
            //TODO : Convert to enum
            public string FIRE_TYPE { get; set; }
            public string INCIDENT_NAME { get; set; }
            public string GEOGRAPHIC_DESCRIPTION { get; set; }
            public float LATITUDE { get; set; }
            public float LONGITUDE { get; set; }
            public float CURRENT_SIZE { get; set; }
            public string FIRE_URL { get; set; }
            public string FEATURE_CODE { get; set; }
            public int OBJECTID { get; set; }
            public object SE_ANNO_CAD_DATA { get; set; }
        }

    }

}

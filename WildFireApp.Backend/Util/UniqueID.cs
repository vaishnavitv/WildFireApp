using System.Globalization;

namespace WildFireApp.Backend.Util
{
    public class UniqueID
    {
        public static String getCurrentDateAndTime()
        {
            return DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ssZ", CultureInfo.InvariantCulture);
        }
    }
}

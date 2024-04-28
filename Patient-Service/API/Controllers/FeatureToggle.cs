using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;

namespace API.Controllers
{ 
    public class FeatureToggle : IFeatureToggle
    {
        EdgeFeatureHubConfig _config = null;
        private string _sdkkey = "24b959e4-3525-4c95-9f98-d73c84f5e4ec/oabgko9JSwMDdgBjsenvi3PqiciAoWPQct5t9hxf";


        public FeatureToggle()
        {
            _config = new EdgeFeatureHubConfig("http://featurehub:8085", _sdkkey);
            FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s + "\n");
            FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s + "\n");
            FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s + "\n");
            FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s + "\n");
        }

        public async Task<bool> CanDoctorDelete()
        {
            var fh = await _config.NewContext().Build();
            if (fh["DeletePatient"].IsEnabled)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CanDoctorGet()
        {
            var fh = await _config.NewContext().Build();
            if (fh["GetPatient"].IsEnabled)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsFromDenmark(string Country)
        {
            StrategyAttributeCountryName SACM;
            var couldParse = Enum.TryParse(Country, true, out SACM);
            if (couldParse)
            {
                var fh = await _config.NewContext().Country(SACM).Build();
                if (fh["DanishAccess"].IsEnabled)
                {
                    return true;
                }
            }
            return false;
        }

    }

   
}
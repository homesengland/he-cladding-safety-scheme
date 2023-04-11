using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Settings;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.Providers
{
    public class AnalyticsDataProvider: IAnalyticsDataProivder
    {
        private readonly AnalyticsSettings _analyticsSettings;

        public AnalyticsDataProvider(IOptions<AnalyticsSettings> analyticsSettings)
        {
            _analyticsSettings = analyticsSettings.Value;
        }

        public string GetAnalyticsId()
        {
            return _analyticsSettings.GOOGLE_ANALYTICS_ID;
        }
    }
}

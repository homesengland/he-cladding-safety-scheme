using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class BuildingSafetyRegulatorRegistrationCodeViewModel
    {
        public string ReturnUrl { get; set; }
        public string BuildingSafetyRegulatorRegistrationCode { get; set; }
        public EApplicationScheme ApplicationScheme { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}

using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class BuildingSafetyRegulatorRegistrationCodeViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public string BuildingSafetyRegulatorRegistrationCode { get; set; }
        public int Version { get; set; }
        public ESubmitAction SubmitAction { get; set; }
        public string ReturnUrl { get; set; }
    }
}

using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class BuildingControlRequiredViewModel
    {
        public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
        public bool? BuildingControlRequired { get; set; }
        public string ReturnUrl { get; set; }
        public string BuildingName { get; set; }
        public bool ShowBuildingSafetyRegulatorRegistrationCode { get; set; }
        public bool? WorksPermissionApplied { get; set; }
        public EYesNoNonBoolean? WorksPermissionRequired { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public int Version { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}

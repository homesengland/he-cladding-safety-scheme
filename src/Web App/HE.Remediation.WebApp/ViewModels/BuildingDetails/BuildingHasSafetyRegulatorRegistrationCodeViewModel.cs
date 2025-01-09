using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingHasSafetyRegulatorRegistrationCodeViewModel
{
    public string ReturnUrl { get; set; }
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}
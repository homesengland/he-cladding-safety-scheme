using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

public class VariationRequestBaseViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public ESubmitAction SubmitAction { get; set; }

    public string ReturnUrl { get; set; }
}

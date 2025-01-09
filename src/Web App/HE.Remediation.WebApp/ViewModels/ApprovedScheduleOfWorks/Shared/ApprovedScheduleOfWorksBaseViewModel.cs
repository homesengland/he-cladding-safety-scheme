using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ApprovedScheduleOfWorks.Shared;

public class ApprovedScheduleOfWorksBaseViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public ESubmitAction SubmitAction { get; set; }

    public string ReturnUrl { get; set; }
}

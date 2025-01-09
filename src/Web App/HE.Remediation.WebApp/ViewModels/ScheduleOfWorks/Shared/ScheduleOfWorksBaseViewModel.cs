using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

public class ScheduleOfWorksBaseViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public ESubmitAction SubmitAction { get; set; }

    public string ReturnUrl { get; set; }
}

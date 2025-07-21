using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeEntryViewModel
{
    public string PostCode { get; set; }

    public string ReturnUrl { get; set; }

    public bool NonResidentialUnits { get; set; }

    public EResponsibleEntityType ResponsibleEntityType { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public int ProgressReportVersion { get; set; }
    public bool IsProgressReportGcoComplete { get; set; }
    public bool ProgressReportHasVisitedCheckYourAnswers { get; set; }
}

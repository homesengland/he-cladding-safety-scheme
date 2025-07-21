
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.GetInformedLeaseholder;

public class GetInformedLeaseholderResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool? LeaseholdersInformed { get; set; } 
    public bool HasVisitedCheckYourAnswers { get; set; }
}

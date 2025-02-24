
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.CheckYourAnswers.Get;

public class GetCheckYourAnswersResponse
{
    public DateTime? StartDate { get; set; }
    
    public DateTime? UnsafeCladdingRemovalDate { get; set; }
    
    public DateTime? ExpectedDateForCompletion { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; internal set; }
}

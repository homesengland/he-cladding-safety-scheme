
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Get;

public class GetKeyDatesResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public int? StartDateMonth { get; set; }
    public int? StartDateYear { get; set; }
    
    public int? UnsafeCladdingRemovalDateMonth { get; set; }
    public int? UnsafeCladdingRemovalDateYear { get; set; }
    
    public int? ExpectedDateForCompletionMonth { get; set; }
    public int? ExpectedDateForCompletionYear { get; set; }
    
    public bool IsSubmitted { get; set; }
}

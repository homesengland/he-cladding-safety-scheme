
namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Get;

public class GetConfirmKeyDatesResponse
{
    public int? StartDateMonth { get; set; }
    public int? StartDateYear { get; set; }
    
    public int? UnsafeCladdingRemovalDateMonth { get; set; }
    public int? UnsafeCladdingRemovalDateYear { get; set; }
    
    public int? ExpectedDateForCompletionMonth { get; set; }
    public int? ExpectedDateForCompletionYear { get; set; }
}

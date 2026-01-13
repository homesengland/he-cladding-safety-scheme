
namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Get;

public class GetConfirmKeyDatesResponse
{
    public int? StartDateMonth { get; set; }
    public int? StartDateYear { get; set; }
    
    public int? UnsafeCladdingRemovalDateMonth { get; set; }
    public int? UnsafeCladdingRemovalDateYear { get; set; }

    public int? PracticalCompletionDateMonth { get; set; }
    public int? PracticalCompletionDateYear { get; set; }
}

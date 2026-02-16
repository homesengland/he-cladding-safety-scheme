using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Set;

public class SetConfirmKeyDatesRequest : IRequest
{
    public int? StartDateMonth { get; set; }
    public int? StartDateYear { get; set; }
    
    public int? UnsafeCladdingRemovalDateMonth { get; set; }
    public int? UnsafeCladdingRemovalDateYear { get; set; }
    
    public int? PracticalCompletionDateMonth { get; set; }
    public int? PracticalCompletionDateYear { get; set; }
}

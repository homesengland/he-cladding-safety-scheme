
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Set;

public class SetKeyDatesRequest : IRequest
{
    public int? StartDateMonth { get; set; }
    public int? StartDateYear { get; set; }
    
    public int? UnsafeCladdingRemovalDateMonth { get; set; }
    public int? UnsafeCladdingRemovalDateYear { get; set; }
    
    public int? ExpectedDateForCompletionMonth { get; set; }
    public int? ExpectedDateForCompletionYear { get; set; }
}

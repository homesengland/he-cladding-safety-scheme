using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.RefurbishmentCompletionDate.Set;

public class SetRefurbishmentCompletionDateRequest : IRequest
{
    public int? RefurbishmentCompletionDateMonth { get; set; }
    public int? RefurbishmentCompletionDateYear { get; set; }
}

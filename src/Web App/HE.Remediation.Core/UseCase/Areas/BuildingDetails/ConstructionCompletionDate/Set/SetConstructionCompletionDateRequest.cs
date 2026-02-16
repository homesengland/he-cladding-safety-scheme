using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Set;

public class SetConstructionCompletionDateRequest : IRequest
{
    public int? ConstructionCompletionDateMonth { get; set; }
    public int? ConstructionCompletionDateYear { get; set; }
}

using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.WorksAlreadyCompleted.SetWorksAlreadyCompleted;

public class SetWorksAlreadyCompletedRequest : IRequest
{
    public bool? WorksAlreadyCompleted { get; set; }
}
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.WorksAlreadyCompleted.GetWorksAlreadyCompleted;

public class GetWorksAlreadyCompletedRequest : IRequest<GetWorksAlreadyCompletedResponse>
{
    private GetWorksAlreadyCompletedRequest()
    {
    }

    public static readonly GetWorksAlreadyCompletedRequest Request = new();
}
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.GetDeveloperContacted;

public class GetDeveloperContactedRequest : IRequest<GetDeveloperContactedResponse>
{
    private GetDeveloperContactedRequest()
    {
    }

    public static readonly GetDeveloperContactedRequest Request = new();
}
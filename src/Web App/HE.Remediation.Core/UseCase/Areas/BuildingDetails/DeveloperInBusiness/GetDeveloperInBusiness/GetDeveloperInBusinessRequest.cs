using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.GetDeveloperInBusiness;

public class GetDeveloperInBusinessRequest : IRequest<GetDeveloperInBusinessResponse>
{
    private GetDeveloperInBusinessRequest()
    {
    }

    public static readonly GetDeveloperInBusinessRequest Request = new();
}
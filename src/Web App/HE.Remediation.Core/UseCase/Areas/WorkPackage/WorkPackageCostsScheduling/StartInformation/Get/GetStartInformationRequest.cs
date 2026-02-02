using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.StartInformation.Get;

public class GetStartInformationRequest : IRequest<GetStartInformationResponse>
{
    private GetStartInformationRequest()
    {
    }

    public static GetStartInformationRequest Request => new();
}

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;

public class GetBaseInformationRequest : IRequest<GetBaseInformationResponse>
{
    private GetBaseInformationRequest()
    {
    }

    public static GetBaseInformationRequest Request => new();
}

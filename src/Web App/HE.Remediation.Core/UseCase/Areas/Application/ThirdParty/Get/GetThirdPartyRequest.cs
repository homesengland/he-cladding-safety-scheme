using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;

public class GetThirdPartyRequest : IRequest<GetThirdPartyResponse>
{
    private GetThirdPartyRequest()
    {
    }

    public static readonly GetThirdPartyRequest Request = new();
}

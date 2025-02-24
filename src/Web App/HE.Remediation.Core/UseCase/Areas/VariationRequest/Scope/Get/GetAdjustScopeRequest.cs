using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Get;

public class GetAdjustScopeRequest : IRequest<GetAdjustScopeResponse>
{
    private GetAdjustScopeRequest()
    {
    }

    public static readonly GetAdjustScopeRequest Request = new();
}

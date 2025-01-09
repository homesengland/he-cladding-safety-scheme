using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Submitted.Get;

public class GetSubmittedRequest : IRequest<GetSubmittedResponse>
{
    private GetSubmittedRequest()
    {
    }

    public static readonly GetSubmittedRequest Request = new();
}

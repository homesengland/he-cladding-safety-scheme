using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetPursuedSourcesFunding;

public class GetPursuedSourcesFundingRequest : IRequest<GetPursuedSourcesFundingResponse>
{
    private GetPursuedSourcesFundingRequest()
    {
    }

    public static readonly GetPursuedSourcesFundingRequest Request = new();
}
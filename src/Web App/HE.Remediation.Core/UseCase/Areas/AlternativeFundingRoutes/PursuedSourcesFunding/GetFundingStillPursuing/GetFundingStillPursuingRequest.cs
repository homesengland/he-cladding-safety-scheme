using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetFundingStillPursuing;

public class GetFundingStillPursuingRequest : IRequest<GetFundingStillPursuingResponse>
{
    private GetFundingStillPursuingRequest()
    {
    }

    public static readonly GetFundingStillPursuingRequest Request = new();
}
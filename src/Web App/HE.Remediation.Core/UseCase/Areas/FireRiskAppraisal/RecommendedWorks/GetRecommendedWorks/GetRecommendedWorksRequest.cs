using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.GetRecommendedWorks;

public class GetRecommendedWorksRequest: IRequest<GetRecommendedWorksResponse>
{
    private GetRecommendedWorksRequest()
    {
    }

    public static readonly GetRecommendedWorksRequest Request = new();
}

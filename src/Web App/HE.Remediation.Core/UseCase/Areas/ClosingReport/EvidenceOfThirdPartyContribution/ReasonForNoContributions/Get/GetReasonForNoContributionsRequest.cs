using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Get;

public class GetReasonForNoContributionsRequest : IRequest<GetReasonForNoContributionsResponse>
{
    private GetReasonForNoContributionsRequest()
    {
    }

    public static readonly GetReasonForNoContributionsRequest Request = new();
}

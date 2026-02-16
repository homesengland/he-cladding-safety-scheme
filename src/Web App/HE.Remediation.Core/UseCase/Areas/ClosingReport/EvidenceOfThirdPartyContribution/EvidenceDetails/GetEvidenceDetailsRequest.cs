using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class GetEvidenceDetailsRequest : IRequest<GetEvidenceDetailsResponse>
    {
        public Guid? EvidenceId { get; set; }
        public static GetEvidenceDetailsRequest Request => new GetEvidenceDetailsRequest();
    }
}

using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public partial class SetEvidenceSubmitHandler : IRequestHandler<SetEvidenceSubmitRequest>
    {
        private readonly IEvidenceOfThirdPartyContributionRepository _evidenceOfThirdPartyContributionRepository;

        public SetEvidenceSubmitHandler(
            IEvidenceOfThirdPartyContributionRepository evidenceOfThirdPartyContributionRepository)
        {
            _evidenceOfThirdPartyContributionRepository = evidenceOfThirdPartyContributionRepository;
        }

        public async Task<Unit> Handle(SetEvidenceSubmitRequest request, CancellationToken cancellationToken)
        {
            await _evidenceOfThirdPartyContributionRepository.UpdateClosingReportThirdPartyEvidenceAsSubmitted(request.ApplicationId, request.EvidenceId);
            return Unit.Value;
        }
    }

    public class SetEvidenceSubmitRequest : IRequest<Unit>
    {
        public Guid ApplicationId { get; set; }
        public Guid EvidenceId { get; set; }
        public static SetEvidenceSubmitRequest Request => new SetEvidenceSubmitRequest();
    }
}

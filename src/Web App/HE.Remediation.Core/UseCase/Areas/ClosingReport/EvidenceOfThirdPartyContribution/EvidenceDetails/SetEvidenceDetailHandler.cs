using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class SetEvidenceDetailHandler(IApplicationDataProvider applicationDataProvider,
    IEvidenceOfThirdPartyContributionRepository evidenceOfThirdPartyContributionRepository) : IRequestHandler<SetEvidenceDetailRequest, SetEvidenceDetailResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
        private readonly IEvidenceOfThirdPartyContributionRepository _evidenceOfThirdPartyContributionRepository = evidenceOfThirdPartyContributionRepository;

        public async Task<SetEvidenceDetailResponse> Handle(SetEvidenceDetailRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var applicationId = _applicationDataProvider.GetApplicationId();
            request.ApplicationId = applicationId;
            return await _evidenceOfThirdPartyContributionRepository.UpsertClosingReportEvidenceDetail(request);
        }
    }
}

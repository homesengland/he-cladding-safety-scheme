using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport.EvidenceOfThirdPartyContribution;
using HE.Remediation.Core.Interface;
using Mediator;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public partial class GetEvidenceDetailsHandler : IRequestHandler<GetEvidenceDetailsRequest, GetEvidenceDetailsResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IEvidenceOfThirdPartyContributionRepository _evidenceOfThirdPartyContributionRepository;
        private readonly IClosingReportRepository _closingReportRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly ILogger<GetEvidenceDetailsHandler> _logger;

        public GetEvidenceDetailsHandler(
            IApplicationDataProvider applicationDataProvider,
            IEvidenceOfThirdPartyContributionRepository evidenceOfThirdPartyContributionRepository,
            IClosingReportRepository closingReportRepository,
            IApplicationRepository applicationRepository,
            IBuildingDetailsRepository buildingDetailsRepository,
        ILogger<GetEvidenceDetailsHandler> logger)
        {
            _applicationDataProvider = applicationDataProvider;
            _evidenceOfThirdPartyContributionRepository = evidenceOfThirdPartyContributionRepository;
            _closingReportRepository = closingReportRepository;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
            _logger = logger;
        }

        public async ValueTask<GetEvidenceDetailsResponse> Handle(GetEvidenceDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var evidenceDetails = await _evidenceOfThirdPartyContributionRepository.GetEvidenceDetails(applicationId);
            var isClosingReportSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            if (evidenceDetails != null && evidenceDetails.Any())
            {
                var response = evidenceDetails.Select(detail => new GetEvidenceDetailsResult
                {
                    Id = detail.Id,
                    ThirdPartyName = detail?.ThirdPartyName,
                    DateOfAttempt = detail?.DateOfAttempt,
                    StatusOfAttempt = detail?.StatusOfAttempt,
                    AttemptDetails = detail?.AttemptDetails,
                    IsEditable = (bool)detail?.IsEditable
                }).ToList();

                return new GetEvidenceDetailsResponse
                {
                    ApplicationId = applicationId,
                    EvidenceDetailsResults = response,
                    IsSubmitted = isClosingReportSubmitted,
                    ApplicationReferenceNumber = applicationReferenceNumber,
                    BuildingName = buildingName
                };
            }

            return new GetEvidenceDetailsResponse{
                ApplicationReferenceNumber = applicationReferenceNumber,
                BuildingName = buildingName
            };
        }
    }

}

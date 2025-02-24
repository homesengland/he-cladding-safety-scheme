using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.CheckYourAnswers.Get
{
    public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
    {
        private readonly IWorkPackageRepository _workPackageRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildDetailsRepository;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetCheckYourAnswersHandler(IWorkPackageRepository workPackageRepository, IApplicationRepository applicationRepository, IBuildingDetailsRepository buildingDetailsRepository, IApplicationDataProvider applicationDataProvider)
        {
            _workPackageRepository = workPackageRepository;
            _applicationRepository = applicationRepository;
            _buildDetailsRepository = buildingDetailsRepository;
            _applicationDataProvider = applicationDataProvider;
        }
        public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            var answers = await _workPackageRepository.GetWorkPackageThirdPartyContributionsCheckYourAnswers();

            var applicationId = _applicationDataProvider.GetApplicationId();
            var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildDetailsRepository.GetBuildingUniqueName(applicationId);
            var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

            return new GetCheckYourAnswersResponse
            {
                PursuingThirdPartyContribution = answers.PursuingThirdPartyContribution,
                ContributionTypes = answers.ContributionTypes,
                ContributionAmount = answers.ContributionAmount,
                ContributionNotes = answers.ContributionNotes,
                ApplicationReferenceNumber = referenceNumber,
                BuildingName = buildingName,
                IsSubmitted = isSubmitted,

            };
        }
    }
}

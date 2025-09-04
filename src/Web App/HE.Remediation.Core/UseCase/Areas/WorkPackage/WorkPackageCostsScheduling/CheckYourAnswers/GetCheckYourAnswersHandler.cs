using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CheckYourAnswers
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
            var quotes = await _workPackageRepository.GetCostsScheduleSoughtQuotes();
            var subcontractors = await _workPackageRepository.GetCostsScheduleSubcontractors();

            var totalCosts = await _workPackageRepository.GetWorkPackageCosts();
            var eligibleCosts = GetEligibleCosts(totalCosts);

            var applicationId = _applicationDataProvider.GetApplicationId();
            var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildDetailsRepository.GetBuildingUniqueName(applicationId);
            var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();


            var preferredContractorLinks = await _workPackageRepository.GetCostsSchedulePreferredContractorLinks();

            return new GetCheckYourAnswersResponse
            {
                CompetitiveBidsObtained = quotes,
                SubContractors = subcontractors.Select(x=> new GetCheckYourAnswersResponse.SubContractor { CompanyName = x.CompanyName, CompanyRegistrationNumber = x.CompanyRegistration}).ToList(),
                EligibleCosts = eligibleCosts,
                IneligibleCosts = totalCosts.IneligibleAmount ?? 0,
                TotalCosts = eligibleCosts + (totalCosts.IneligibleAmount ?? 0),
                AccessDescription = totalCosts.AccessDescription,
                ContingenciesDescription = totalCosts.ContractorContingenciesDescription,
                EligibleExternalDescription = totalCosts.OtherEligibleWorkToExternalWallDescription,
                EligibleInternalDescription = totalCosts.InternalMitigationWorksDescription,
                FeasibilityDescription = totalCosts.FeasibilityStageDescription,
                MainContractorDescription = totalCosts.MainContractorPreliminariesDescription,
                NewCladdingDescription = totalCosts.NewCladdingDescription,
                OverheadsProfitDescription = totalCosts.OverheadsAndProfitDescription,
                PostTenderDescription = totalCosts.PostTenderStageDescription,
                PropertyManagerDescription = totalCosts.PropertyManagerDescription,
                RemovalOfCladdingDescription = totalCosts.RemovalOfCladdingDescription,
                VatDescription = totalCosts.IrrecoverableVatDescription,
                ApplicationReferenceNumber = referenceNumber,
                BuildingName = buildingName,
                IsSubmitted = isSubmitted,
                PreferredContractorLinksResult = preferredContractorLinks,
            };
        }

        private decimal GetEligibleCosts(GetWorkPackageCostsResult totalCosts)
        {

            var installationTotal = (totalCosts.NewCladdingAmount ?? 0) + (totalCosts.OtherEligibleWorkToExternalWallAmount ?? 0) + (totalCosts.InternalMitigationWorksAmount ?? 0);
            var preliminaryTotal = (totalCosts.MainContractorPreliminariesAmount ?? 0) + (totalCosts.AccessAmount ?? 0) +
                                         (totalCosts.OverheadsAndProfitAmount ?? 0) + (totalCosts.ContractorContingenciesAmount ?? 0);
            var otherTotal = (totalCosts.FeasibilityStageAmount ?? 0) + (totalCosts.FraewSurveyAmount ?? 0) + (totalCosts.PostTenderStageAmount ?? 0) +
                                      (totalCosts.PropertyManagerAmount ?? 0) + (totalCosts.IrrecoverableVatAmount ?? 0);

            return (totalCosts.RemovalOfCladdingAmount ?? 0) + installationTotal + preliminaryTotal + otherTotal;

        }
    }
}

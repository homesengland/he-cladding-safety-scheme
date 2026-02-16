using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;

public class GetBuildingsInsuranceHandler : IRequestHandler<GetBuildingsInsuranceRequest, GetBuildingsInsuranceResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IBuildingsInsuranceRepository _buildingsInsuranceRepository;

    public GetBuildingsInsuranceHandler(IApplicationDetailsProvider applicationDetailsProvider, IBuildingsInsuranceRepository buildingsInsuranceRepository)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _buildingsInsuranceRepository = buildingsInsuranceRepository;
    }

    public async ValueTask<GetBuildingsInsuranceResponse> Handle(GetBuildingsInsuranceRequest request, CancellationToken cancellationToken)
    {
        var details = await _applicationDetailsProvider.GetApplicationDetails();

        var buildingInsurance = await _buildingsInsuranceRepository.GetBuildingInsurance(details.ApplicationId);

        var insuranceProviders = await _buildingsInsuranceRepository.GetBuildingInsuranceProviders();

        return new GetBuildingsInsuranceResponse
        {
            ReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            SumInsuredAmount = buildingInsurance?.SumInsuredAmount,
            CurrentBuildingInsurancePremiumAmount = buildingInsurance?.CurrentBuildingInsurancePremiumAmount,
            IfOtherInsuranceProviderName = buildingInsurance?.IfOtherInsuranceProviderName,
            AdditionalInfo = buildingInsurance?.AdditionalInfo,
            InsuranceProvidersJson = buildingInsurance?.InsuranceProvidersJson,
            BuildingRemediationResponsibilityTypeId = buildingInsurance?.BuildingRemediationResponsibilityTypeId,
            InsuranceProviders = insuranceProviders?.ToList()
        };
    }
}
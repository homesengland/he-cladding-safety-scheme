using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance;

public partial class GetBuildingsInsuranceHandler(IApplicationDataProvider applicationDataProvider, IBuildingsInsuranceRepository buildingsInsuranceRepository) : IRequestHandler<GetBuildingsInsuranceRequest, GetBuildingsInsuranceResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
    private readonly IBuildingsInsuranceRepository _buildingsInsuranceRepository = buildingsInsuranceRepository;

    public async Task<GetBuildingsInsuranceResponse> Handle(GetBuildingsInsuranceRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var response = await _buildingsInsuranceRepository.GetClosingReportBuildingsInsurance(applicationId);

        response ??= new GetBuildingsInsuranceResponse();

        var insuranceProviders = await _buildingsInsuranceRepository.GetBuildingInsuranceProviders();
        response.InsuranceProviders = insuranceProviders?.ToList();

        return response;
    }
}
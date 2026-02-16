using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance;

public partial class GetBuildingsInsuranceHandler(
    IApplicationDataProvider applicationDataProvider, 
    IBuildingsInsuranceRepository buildingsInsuranceRepository,
    IClosingReportRepository closingReportRepository) : IRequestHandler<GetBuildingsInsuranceRequest, GetBuildingsInsuranceResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
    private readonly IBuildingsInsuranceRepository _buildingsInsuranceRepository = buildingsInsuranceRepository;
    private readonly IClosingReportRepository _closingReportRepository = closingReportRepository;

    public async ValueTask<GetBuildingsInsuranceResponse> Handle(GetBuildingsInsuranceRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var response = await _buildingsInsuranceRepository.GetClosingReportBuildingsInsurance(applicationId);
        var isClosingReportSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

        response ??= new GetBuildingsInsuranceResponse();

        var insuranceProviders = await _buildingsInsuranceRepository.GetBuildingInsuranceProviders();
        response.InsuranceProviders = insuranceProviders?.ToList();
        response.IsSubmitted = isClosingReportSubmitted;

        return response;
    }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.SetBuildingsInsurance;

public class SetBuildingsInsuranceHandler(IApplicationDataProvider applicationDataProvider, IBuildingsInsuranceRepository buildingsInsuranceRepository) : IRequestHandler<SetBuildingsInsuranceRequest, SetBuildingsInsuranceResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
    private readonly IBuildingsInsuranceRepository _buildingsInsuranceRepository = buildingsInsuranceRepository;

    public async ValueTask<SetBuildingsInsuranceResponse> Handle(SetBuildingsInsuranceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        request.ApplicationId = applicationId;

        var response = await _buildingsInsuranceRepository.SaveBuildingInsurance(request);

        response ??= new SetBuildingsInsuranceResponse();

        return response;
    }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;

public class GetBaseInformationHandler : IRequestHandler<GetBaseInformationRequest, GetBaseInformationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;

    public GetBaseInformationHandler(IApplicationDataProvider applicationDataProvider,
                                     IBuildingDetailsRepository buildingDetailsRepository,
                                     IApplicationRepository applicationRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
    }

    public async Task<GetBaseInformationResponse> Handle(GetBaseInformationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        return new GetBaseInformationResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}

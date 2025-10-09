using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetClosingReportAbout;

public class GetClosingReportAboutHandler : IRequestHandler<GetClosingReportAboutRequest, GetClosingReportAboutResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;

    public GetClosingReportAboutHandler(IApplicationDataProvider applicationDataProvider,
                                        IApplicationRepository applicationRepository,
                                        IBuildingDetailsRepository buildingDetailsRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
    }

    public async Task<GetClosingReportAboutResponse> Handle(GetClosingReportAboutRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        return new GetClosingReportAboutResponse
        {
            ApplicationReferenceNumber = applicationReferenceNumber,
            BuildingName = buildingName,
        };        
    }
}

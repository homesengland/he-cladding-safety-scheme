using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetClosingReportInformation;

public class GetClosingReportInformationHandler : IRequestHandler<GetClosingReportInformationRequest, GetClosingReportInformationResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IClosingReportRepository _closingReportRepository;

    public GetClosingReportInformationHandler(IDbConnectionWrapper connection, 
                                        IApplicationDataProvider applicationDataProvider,
                                        IApplicationRepository applicationRepository,
                                        IBuildingDetailsRepository buildingDetailsRepository,
                                        IClosingReportRepository closingReportRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _closingReportRepository = closingReportRepository;
    }

    public async Task<GetClosingReportInformationResponse> Handle(GetClosingReportInformationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

        return new GetClosingReportInformationResponse
        {
            IsSubmitted = isSubmitted,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };        
    }
}

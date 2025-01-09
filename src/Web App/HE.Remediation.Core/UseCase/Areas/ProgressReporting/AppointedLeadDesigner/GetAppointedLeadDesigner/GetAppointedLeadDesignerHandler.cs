using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedLeadDesigner.GetAppointedLeadDesigner;

public class GetAppointedLeadDesignerHandler : IRequestHandler<GetAppointedLeadDesignerRequest, GetAppointedLeadDesignerResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetAppointedLeadDesignerHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetAppointedLeadDesignerResponse> Handle(GetAppointedLeadDesignerRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var leaseholdersInformed = await _progressReportingRepository.GetProgressReportLeaseholdersInformed();        
        var leadDesignerAppointed = await _progressReportingRepository.GetProgressReportLeadDesignerAppointed();

        return new GetAppointedLeadDesignerResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            LeadDesignerAppointed = leadDesignerAppointed,
            LeaseholdersInformed = leaseholdersInformed
        };
    }
}

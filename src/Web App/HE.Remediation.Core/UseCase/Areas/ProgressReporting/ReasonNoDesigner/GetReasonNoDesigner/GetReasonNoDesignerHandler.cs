using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoDesigner.GetReasonNoDesigner;

public class GetReasonNoDesignerHandler : IRequestHandler<GetReasonNoDesignerRequest, GetReasonNoDesignerResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetReasonNoDesignerHandler(IApplicationDataProvider applicationDataProvider,
                                      IBuildingDetailsRepository buildingDetailsRepository,
                                      IApplicationRepository applicationRepository,
                                      IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetReasonNoDesignerResponse> Handle(GetReasonNoDesignerRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var leadDesignerNotAppointedReason = await _progressReportingRepository.GetProgressReportLeadDesignerNotAppointedReason();
        return new GetReasonNoDesignerResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            LeadDesignerNotAppointedReason = leadDesignerNotAppointedReason?.LeadDesignerNotAppointedReason,
            LeadDesignerNeedsSupport = leadDesignerNotAppointedReason?.LeadDesignerNeedsSupport
        };
    }
}

using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.GetAppointedOtherMembers;

public class GetAppointedOtherMembersHandler : IRequestHandler<GetAppointedOtherMembersRequest, GetAppointedOtherMembersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetAppointedOtherMembersHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetAppointedOtherMembersResponse> Handle(GetAppointedOtherMembersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var otherMembersAppointed = await _progressReportingRepository.GetProgressReportOtherMembersAppointed();
        var leaseholdersInformed = await _progressReportingRepository.GetProgressReportLeaseholdersInformed();

        return new GetAppointedOtherMembersResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            LeaseholdersInformed = leaseholdersInformed,
            OtherMembersAppointed = otherMembersAppointed
        };
    }
}

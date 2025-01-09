using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetRemoveTeamMember;

public class GetRemoveTeamMemberHandler : IRequestHandler<GetRemoveTeamMemberRequest, GetRemoveTeamMemberResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetRemoveTeamMemberHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetRemoveTeamMemberResponse> Handle(GetRemoveTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMemberName = await _progressReportingRepository.GetProgressReportTeamMemberName(request.TeamMemberId);
        return new GetRemoveTeamMemberResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            TeamMemberId = request.TeamMemberId,
            TeamMemberName = teamMemberName
        };
    }
}

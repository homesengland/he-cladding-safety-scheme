using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;

public class GetThirdPartyHandler(IApplicationDataProvider applicationDataProvider,
                                       IBuildingDetailsRepository buildingDetailsRepository,
                                       IApplicationRepository applicationRepository,
                                       IProgressReportingRepository progressReportingRepository) : IRequestHandler<GetThirdPartyRequest, GetThirdPartyResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository = applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository = buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository = progressReportingRepository;

    public async Task<GetThirdPartyResponse> Handle(GetThirdPartyRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMembers = await _progressReportingRepository.GetTeamMembers();

        return new GetThirdPartyResponse
        {
            TeamMembers = (teamMembers ?? new List<GetTeamMembersResult>()).Where(t => !t.IsRevoked.GetValueOrDefault()).ToList(),
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}

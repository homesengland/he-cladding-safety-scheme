using System.Security.Cryptography.X509Certificates;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetWhoIsTheGrantCertifyingOfficerHandler : IRequestHandler<GetWhoIsTheGrantCertifyingOfficerRequest, GetWhoIsTheGrantCertifyingOfficerResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetWhoIsTheGrantCertifyingOfficerHandler(
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IProgressReportingRepository progressReportingRepository, 
        IApplicationDataProvider applicationDataProvider)
    {
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetWhoIsTheGrantCertifyingOfficerResponse> Handle(GetWhoIsTheGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMemberId = await _progressReportingRepository.GetGrantCertifyingOfficerTeamMember();
        var teamMembers = await _progressReportingRepository.GetProjectManagersAndQuantitySurveyors();
        var version = await _progressReportingRepository.GetProgressReportVersion();
        var isGcoComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();
        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetWhoIsTheGrantCertifyingOfficerResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            ProjectTeamMemberId = teamMemberId,
            Version = version,
            IsGcoComplete = isGcoComplete,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers,
            TeamMembers = teamMembers.Select(x => new GetWhoIsTheGrantCertifyingOfficerResponse.TeamMemberResponse
            {
                Id = x.Id,
                Name = x.Name,
                Role = x.Role
            }).ToList()
        };
    }
}

public class GetWhoIsTheGrantCertifyingOfficerRequest : IRequest<GetWhoIsTheGrantCertifyingOfficerResponse>
{
    private GetWhoIsTheGrantCertifyingOfficerRequest()
    {
    }

    public static readonly GetWhoIsTheGrantCertifyingOfficerRequest Request = new();
}

public class GetWhoIsTheGrantCertifyingOfficerResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public Guid? ProjectTeamMemberId { get; set; }
    public IList<TeamMemberResponse> TeamMembers { get; set; }
    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public class TeamMemberResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
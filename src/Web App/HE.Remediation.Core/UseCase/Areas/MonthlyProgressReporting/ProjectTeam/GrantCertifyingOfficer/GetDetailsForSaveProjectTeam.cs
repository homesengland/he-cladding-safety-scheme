using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetDetailsForSaveProjectTeamHandler : IRequestHandler<GetDetailsForSaveProjectTeamRequest, GetDetailsForSaveProjectTeamResponse>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetDetailsForSaveProjectTeamHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<GetDetailsForSaveProjectTeamResponse> Handle(GetDetailsForSaveProjectTeamRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var gcoDetails = await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(progressReportId);
        var hasTeamMembers = await HasTeamMembers(details.ApplicationId, progressReportId);
        var reasonNoTeam = await _progressReportingProjectTeamRepository.GetProjectTeamNoTeam(
            new GetProjectTeamNoTeamParameters() { ApplicationId = details.ApplicationId, ProgressReportId = progressReportId }
        );

        return new GetDetailsForSaveProjectTeamResponse
        {
            HasReasonNoTeam = reasonNoTeam?.ReasonNoTeam != null,
            HasZeroTeamMembers = !hasTeamMembers.HasAnyTeamMembers,
            HasTeamMembersButNoGcoRoles = hasTeamMembers.HasAnyTeamMembers && !hasTeamMembers.HasGcoTeamMember,
            IsGcoComplete = gcoDetails != null && gcoDetails.IsGcoComplete
        };
    }

    private async Task<HasTeamMembersResult> HasTeamMembers(Guid applicationId, Guid progressReportId)
    {
        var teamMembers = await _progressReportingProjectTeamRepository
                    .GetProjectTeamMembers(new GetTeamMembersParameters() { ApplicationId = applicationId, ProgressReportId = progressReportId }
                );

        var applicableRoles = new[] { (int)ETeamRole.ProjectManager, (int)ETeamRole.QuantitySurveyor };

        var hasGcoTeamMember = false;
        foreach (var person in teamMembers)
        {
            if (applicableRoles.Contains(person.RoleId ?? 0))
            {
                hasGcoTeamMember = true;
            }
        }
        return new HasTeamMembersResult(teamMembers.Count > 0, hasGcoTeamMember);
    }

    private record HasTeamMembersResult(bool HasAnyTeamMembers, bool HasGcoTeamMember);
}

public class GetDetailsForSaveProjectTeamRequest : IRequest<GetDetailsForSaveProjectTeamResponse>
{
    private GetDetailsForSaveProjectTeamRequest()
    {
    }

    public static readonly GetDetailsForSaveProjectTeamRequest Request = new();
}

public class GetDetailsForSaveProjectTeamResponse
{
    public bool HasZeroTeamMembers { get; set; }
    public bool HasTeamMembersButNoGcoRoles { get; set; }
    public bool IsGcoComplete { get; set; }
    public bool HasReasonNoTeam { get; set; }
}
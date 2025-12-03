using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;

public class SetKeyRolesHandler : IRequestHandler<SetKeyRolesRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _projectTeamRepository;

    public SetKeyRolesHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingProjectTeamRepository projectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _projectTeamRepository = projectTeamRepository;
    }

    public async Task<Unit> Handle(SetKeyRolesRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        await _projectTeamRepository.SetProjectTeamKeyRoles(new SetProjectTeamKeyRolesParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            ApplicationleadTeamMemberId = request.ApplicationLeadTeamMemberId,
            LeaseholderCommunictorTeamMemberId = request.LeaseholderCommunicatorTeamMemberId,
            RegulatoryComplianceTeamMemberId = request.RegulatoryComplianceTeamMemberId
        });

        return Unit.Value;
    }
}

public class SetKeyRolesRequest : IRequest
{
    public Guid? ApplicationLeadTeamMemberId { get; set; }
    public Guid? LeaseholderCommunicatorTeamMemberId { get; set; }
    public Guid? RegulatoryComplianceTeamMemberId { get; set; }
}
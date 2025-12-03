using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class UpdateProjectTeamTeamMemberHandler : IRequestHandler<UpdateProjectTeamTeamMemberRequest, Guid>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;
    private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;

    public UpdateProjectTeamTeamMemberHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository, 
        IMonthlyProgressReportingRepository monthlyProgressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
        _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
    }

    public async Task<Guid> Handle(UpdateProjectTeamTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var considerateConstructorSchemeReason = request.ConsiderateConstructorSchemeType switch
        {
            EConsiderateConstructorSchemeType.No => request.ConsiderateConstructorSchemeReasonNo,
            EConsiderateConstructorSchemeType.DontKnow => request.ConsiderateConstructorSchemeReasonDontKnow,
            _ => null
        };

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var teamMemberId = await _progressReportingProjectTeamRepository.UpsertProjectTeamMember(new UpsertProjectTeamMemberParameters
        {
            ProgressReportId = progressReportId,
            CompanyName = request.CompanyName,
            CompanyRegistration = request.CompanyRegistration,
            ConsiderateConstructorSchemeTypeId = (int?)request.ConsiderateConstructorSchemeType,
            ContractSigned = request.ContractSigned,
            EmailAddress = request.EmailAddress,
            IndemnityInsurance = request.IndemnityInsurance,
            IndemnityInsuranceReason = request.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = request.InvolvedInOriginalInstallation,
            InvolvedRoleReason = request.InvolvedRoleReason,
            Name = request.Name,
            OtherRole = request.OtherRole,
            PrimaryContactNumber = request.PrimaryContactNumber,
            TeamMemberId = request.IsChangingExistingMember && request.TeamMemberId.HasValue ? request.TeamMemberId : null,
            TeamRoleId = (int)request.Role,
            HasChasCertification = request.HasChasCertification,
            ConsiderateConstructorSchemeReason = considerateConstructorSchemeReason,
            CreatedDate = DateTime.UtcNow,
            LastModifiedDate = DateTime.UtcNow
        });

        await _progressReportingProjectTeamRepository.UpdateProjectTeamStatus(progressReportId, ETaskStatus.InProgress);

        await _monthlyProgressReportingRepository.SetMonthlyReportStatus(new SetMonthlyReportStatusParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            TaskStatusId = (int)ETaskStatus.InProgress
        });

        scope.Complete();

        return teamMemberId;
    }
}
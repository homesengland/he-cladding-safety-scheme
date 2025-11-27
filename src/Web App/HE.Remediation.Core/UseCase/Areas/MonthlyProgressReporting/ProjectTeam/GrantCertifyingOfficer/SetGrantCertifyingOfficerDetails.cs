using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class SetGrantCertifyingOfficerDetailsHandler : IRequestHandler<SetGrantCertifyingOfficerDetailsRequest>
{
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetGrantCertifyingOfficerDetailsHandler(
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<Unit> Handle(SetGrantCertifyingOfficerDetailsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _progressReportingProjectTeamRepository.UpdateGrantCertifyingOfficerDetails(new UpdateGrantCertifyingOfficerDetailsParameters
        {
            CompanyName = request.CompanyName,
            CompanyRegistrationNumber = request.CompanyRegistration,
            ContractSigned = request.ContractSigned,
            EmailAddress = request.EmailAddress,
            IndemnityInsurance = request.IndemnityInsurance,
            IndemnityInsuranceReason = request.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = request.InvolvedInOriginalInstallation,
            InvolvedRoleReason = request.InvolvedRoleReason,
            Name = request.Name,
            PrimaryContactNumber = request.PrimaryContactNumber,
            TeamMemberId = request.TeamMemberId
        });

        return Unit.Value;
    }
}

public class SetGrantCertifyingOfficerDetailsRequest : IRequest
{
    public Guid TeamMemberId { get; set; }
    public ETeamRole Role { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistration { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public bool ContractSigned { get; set; }
    public bool IndemnityInsurance { get; set; }
    public bool InvolvedInOriginalInstallation { get; set; }
    public string IndemnityInsuranceReason { get; set; }
    public string InvolvedRoleReason { get; set; }
}
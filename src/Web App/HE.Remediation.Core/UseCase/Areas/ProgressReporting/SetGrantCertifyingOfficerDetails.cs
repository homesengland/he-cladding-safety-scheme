using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetGrantCertifyingOfficerDetailsHandler : IRequestHandler<SetGrantCertifyingOfficerDetailsRequest, Guid>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetGrantCertifyingOfficerDetailsHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }


    public async Task<Guid> Handle(SetGrantCertifyingOfficerDetailsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var teamMemberId = await _progressReportingRepository.UpsertTeamMember(new UpsertTeamMemberParameters
        {
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
            TeamMemberId = request.TeamMemberId,
            TeamRoleId = (int)request.Role
        });

        await _progressReportingRepository.UpdateGrantCertifyingOfficerDetails();

        scope.Complete();

        return teamMemberId;
    }
}

public class SetGrantCertifyingOfficerDetailsRequest : IRequest<Guid>
{
    public Guid? TeamMemberId { get; set; }
    public ETeamRole Role { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistration { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public string OtherRole { get; set; }
    public bool? ContractSigned { get; set; }
    public bool? IndemnityInsurance { get; set; }
    public bool? InvolvedInOriginalInstallation { get; set; }
    public string IndemnityInsuranceReason { get; set; }
    public string InvolvedRoleReason { get; set; }
    public EConsiderateConstructorSchemeType? ConsiderateConstructorSchemeType { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.Enums;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Set;

public class SetSelectHandler : IRequestHandler<SetSelectRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetSelectHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetSelectRequest request, CancellationToken cancellationToken)
    {
        var grantCertifyingOfficer = await _workPackageRepository.GetGrantCertifyingOfficerDetails();

        if (grantCertifyingOfficer is null)
        {
            await InsertGrantCertifyingOfficer(request);
        }
        else
        {
            await UpdateGrantCertifyingOfficer(request);
        }

        return Unit.Value;
    }

    private async Task InsertGrantCertifyingOfficer(SetSelectRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var projectTeamMember = await _workPackageRepository.GetTeamMember(request.SelectedProjectTeamMemberId);

        await _workPackageRepository.InsertGrantCertifyingOfficer(new InsertGrantCertifyingOfficerParameters
        {
            Name = projectTeamMember?.Name,
            ProjectTeamMemberId = projectTeamMember.TeamMemberId,
            CompanyName = projectTeamMember.CompanyName,
            CompanyRegistrationNumber = projectTeamMember.CompanyRegistration,
            EmailAddress = projectTeamMember.EmailAddress,
            PrimaryContactNumber = projectTeamMember.PrimaryContactNumber,
            ContractSigned = projectTeamMember.ContractSigned,
            IndemnityInsurance = projectTeamMember.IndemnityInsurance,
            IndemnityInsuranceReason = projectTeamMember.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = projectTeamMember.InvolvedInOriginalInstallation,
            InvolvedRoleReason = projectTeamMember.InvolvedRoleReason,
            CertifyingOfficerResponseId = (int)ECertifyingOfficerResponse.Yes
        });

        await _workPackageRepository.UpdateGrantCertifyingOfficerStatus(ETaskStatus.InProgress);

        scope.Complete();
    }

    private async Task UpdateGrantCertifyingOfficer(SetSelectRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var projectTeamMember = await _workPackageRepository.GetTeamMember(request.SelectedProjectTeamMemberId.Value);

        if (projectTeamMember is null)
        {
            return;
        }

        await _workPackageRepository.UpdateGrantCertifyingOfficer(
            new UpdateGrantCertifyingOfficerParameters
            {
                Name = projectTeamMember.Name,
                ProjectTeamMemberId = request.SelectedProjectTeamMemberId,
                CompanyName = projectTeamMember.CompanyName,
                CompanyRegistrationNumber = projectTeamMember.CompanyRegistration,
                EmailAddress = projectTeamMember.EmailAddress,
                PrimaryContactNumber = projectTeamMember.PrimaryContactNumber,
                ContractSigned = projectTeamMember.ContractSigned,
                IndemnityInsurance = projectTeamMember.IndemnityInsurance,
                IndemnityInsuranceReason = projectTeamMember.IndemnityInsuranceReason,
                InvolvedInOriginalInstallation = projectTeamMember.InvolvedInOriginalInstallation,
                InvolvedRoleReason = projectTeamMember.InvolvedRoleReason
            });

        await _workPackageRepository.UpdateGrantCertifyingOfficerStatus(ETaskStatus.InProgress);

        scope.Complete();
    }
}
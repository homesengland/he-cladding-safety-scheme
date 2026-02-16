using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Details.Set;

public class SetDetailsHandler : IRequestHandler<SetDetailsRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetDetailsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetDetailsRequest request, CancellationToken cancellationToken)
    {
        await UpdateGrantCertifyingOfficer(request);

        return Unit.Value;
    }

    private async Task UpdateGrantCertifyingOfficer(SetDetailsRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.UpdateGrantCertifyingOfficerDetails(new UpdateGrantCertifyingOfficerParameters
        {
            Name = request.Name,
            CompanyName = request.CompanyName,
            CompanyRegistrationNumber = request.CompanyRegistrationNumber,
            EmailAddress = request.EmailAddress,
            PrimaryContactNumber = request.PrimaryContactNumber,
            ContractSigned = request.ContractSigned,
            IndemnityInsurance = request.IndemnityInsurance,
            IndemnityInsuranceReason = request.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = request.InvolvedInOriginalInstallation,
            InvolvedRoleReason = request.InvolvedRoleReason
        });

        await _workPackageRepository.UpdateGrantCertifyingOfficerStatus(ETaskStatus.InProgress);

        scope.Complete();
    }
}
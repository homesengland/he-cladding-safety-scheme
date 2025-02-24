using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.Declaration;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Set;

public class SetDeclarationHandler : IRequestHandler<SetDeclarationRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetDeclarationHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetDeclarationRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var requireSignatories = await _workPackageRepository.GetDeclaration();

        if (requireSignatories is null)
        {
            await _workPackageRepository.InsertDeclaration(new InsertDeclarationParameters
            {
                AllCostsReasonable = request.AllCostsReasonable,
                AllContractualRequirementsMet = request.AllContractualRequirementsMet,
                AllCostsSetOutInFull = request.AllCostsSetOutInFull,
                AcceptGrantAwardBasedOnCosts = request.AcceptGrantAwardBasedOnCosts
            });
        }
        else
        {
            await _workPackageRepository.UpdateDeclaration(new UpdateDeclarationParameters
            {
                AllCostsReasonable = request.AllCostsReasonable,
                AllContractualRequirementsMet = request.AllContractualRequirementsMet,
                AllCostsSetOutInFull = request.AllCostsSetOutInFull,
                AcceptGrantAwardBasedOnCosts = request.AcceptGrantAwardBasedOnCosts
            });
        }

        await _workPackageRepository.UpdateDeclarationStatus(ETaskStatus.Completed);

        scope.Complete();

        return Unit.Value;
    }
}
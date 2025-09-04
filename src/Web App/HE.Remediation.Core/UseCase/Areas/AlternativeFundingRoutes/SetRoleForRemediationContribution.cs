using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

public class SetRoleForRemediationContributionHandler : IRequestHandler<SetRoleForRemediationContributionRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlternateFundingRepository _alternateFundingRepository;

    public SetRoleForRemediationContributionHandler(IApplicationDataProvider applicationDataProvider, IAlternateFundingRepository alternateFundingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _alternateFundingRepository = alternateFundingRepository;
    }

    public async Task<Unit> Handle(SetRoleForRemediationContributionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _alternateFundingRepository.SetPartyPursedRoles(new SetPartyPursedRolesParameters
        {
            ApplicationId = applicationId,
            PartyPursuedRoles = request.Roles.Cast<int>()
        });

        if (!request.Roles.Contains(EPartyPursuedRole.Other))
        {
            await _alternateFundingRepository.SetOtherPartyPursuedRole(new SetOtherPartyPursuedRoleParameters
            {
                ApplicationId = applicationId,
                OtherRole = null
            });
        }

        scope.Complete();

        return Unit.Value;
    }
}

public class SetRoleForRemediationContributionRequest : IRequest
{
    public IList<EPartyPursuedRole> Roles { get; set; }
}
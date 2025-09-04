using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

public class SetOtherPartiesHandler : IRequestHandler<SetOtherPartiesRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlternateFundingRepository _alternateFundingRepository;

    public SetOtherPartiesHandler(IApplicationDataProvider applicationDataProvider, IAlternateFundingRepository alternateFundingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _alternateFundingRepository = alternateFundingRepository;
    }

    public async Task<Unit> Handle(SetOtherPartiesRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _alternateFundingRepository.SetOtherPartyPursuedRole(new SetOtherPartyPursuedRoleParameters
        {
            ApplicationId = applicationId,
            OtherRole = request.OtherPartyPursuedRole
        });

        return Unit.Value;
    }
}

public class SetOtherPartiesRequest : IRequest
{
    public string OtherPartyPursuedRole { get; set; }
}
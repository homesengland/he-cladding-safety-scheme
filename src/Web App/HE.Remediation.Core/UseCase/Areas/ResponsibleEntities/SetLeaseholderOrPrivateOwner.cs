using Dapper;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class SetLeaseholderOrPrivateOwnerHandler : IRequestHandler<SetLeaseholderOrPrivateOwnerRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetLeaseholderOrPrivateOwnerHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetLeaseholderOrPrivateOwnerRequest request, CancellationToken cancellationToken)
    {
        if (request.HasOwners != true)
        {
            request.SharedOwnerCount = null;
        }
        
        await _connection.ExecuteAsync("SetHasOwners", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            request.HasOwners,
            request.SharedOwnerCount
        });

        return Unit.Value;
    }
}

public class SetLeaseholderOrPrivateOwnerRequest : IRequest
{
    public bool? HasOwners { get; set; }
    public int? SharedOwnerCount { get; set; }
}
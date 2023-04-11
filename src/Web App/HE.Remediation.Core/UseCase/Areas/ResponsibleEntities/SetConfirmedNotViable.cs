using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class SetConfirmedNotViableHandler : IRequestHandler<SetConfirmedNotViableRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetConfirmedNotViableHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetConfirmedNotViableRequest request, CancellationToken cancellationToken)
    {
        await _connection.ExecuteAsync("SetConfirmedNotViable", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            request.IsConfirmedNotViable
        });
        return Unit.Value;
    }
}

public class SetConfirmedNotViableRequest : IRequest
{
    public bool? IsConfirmedNotViable { get; set; }
}
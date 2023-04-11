using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.SetRepresentativeBasedInUk;

public class SetRepresentativeBasedInUkHandler : IRequestHandler<SetRepresentativeBasedInUkRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetRepresentativeBasedInUkHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetRepresentativeBasedInUkRequest request, CancellationToken cancellationToken)
    {
        await SaveResponse(request.BasedInUk!.Value);
        return Unit.Value;
    }

    private async Task SaveResponse(bool isInUk)
    {
        await _connection.ExecuteAsync("UpdateBasedInUk", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            IsBasedInUk = isInUk
        });
    }

}
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.SetRepresentativeType;

public class SetRepresentativeTypeHandler : IRequestHandler<SetRepresentativeTypeRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetRepresentativeTypeHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetRepresentativeTypeRequest request, CancellationToken cancellationToken)
    {
        await SaveResponse(request);
        return Unit.Value;
    }

    private async Task SaveResponse(SetRepresentativeTypeRequest request)
    {
        await _connection.ExecuteAsync("InsertOrUpdateRepresentativeType", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            RepresentationTypeId = (int?)request.RepresentativeType
        });
    }
}
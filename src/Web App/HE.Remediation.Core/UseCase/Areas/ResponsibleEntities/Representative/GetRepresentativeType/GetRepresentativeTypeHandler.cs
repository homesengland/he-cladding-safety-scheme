using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.GetRepresentativeType;

public class GetRepresentativeTypeHandler : IRequestHandler<GetRepresentativeTypeRequest, GetRepresentativeTypeResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetRepresentativeTypeHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetRepresentativeTypeResponse> Handle(GetRepresentativeTypeRequest request, CancellationToken cancellationToken)
    {
        var representationType = await _connection.QuerySingleOrDefaultAsync<int?>("GetRepresentativeType", new { ApplicationId = _applicationDataProvider.GetApplicationId() });
        return new GetRepresentativeTypeResponse
        {
            RepresentativeType = (EApplicationRepresentationType?)representationType
        };
    }
}
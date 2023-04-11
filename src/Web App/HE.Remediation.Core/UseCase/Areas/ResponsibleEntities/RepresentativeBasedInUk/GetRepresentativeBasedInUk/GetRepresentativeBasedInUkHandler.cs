using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.GetRepresentativeBasedInUk;

public class GetRepresentativeBasedInUkHandler : IRequestHandler<GetRepresentativeBasedInUkRequest, GetRepresentativeBasedInUkResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetRepresentativeBasedInUkHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetRepresentativeBasedInUkResponse> Handle(GetRepresentativeBasedInUkRequest request, CancellationToken cancellationToken)
    {
        var basedInUk = await _connection.QuerySingleOrDefaultAsync<bool?>("GetRepresentativeBasedInUk", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return new GetRepresentativeBasedInUkResponse
        {
            BasedInUk = basedInUk
        };
    }
}
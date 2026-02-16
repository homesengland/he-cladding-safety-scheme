using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class GetConfirmedNotViableHandler : IRequestHandler<GetConfirmedNotViableRequest, GetConfirmedNotViableResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetConfirmedNotViableHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetConfirmedNotViableResponse> Handle(GetConfirmedNotViableRequest request, CancellationToken cancellationToken)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetConfirmedNotViableResponse>("GetConfirmedNotViable",
            new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId()
            });

        return result ?? new GetConfirmedNotViableResponse();
    }
}

public class GetConfirmedNotViableResponse
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    public bool? IsConfirmedNotViable { get; set; }
}

public class GetConfirmedNotViableRequest : IRequest<GetConfirmedNotViableResponse>
{
    private GetConfirmedNotViableRequest()
    {
    }

    public static readonly GetConfirmedNotViableRequest Request = new();
}
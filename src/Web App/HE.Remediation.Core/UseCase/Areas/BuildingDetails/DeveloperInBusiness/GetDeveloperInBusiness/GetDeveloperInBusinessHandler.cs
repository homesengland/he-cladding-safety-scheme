using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.GetDeveloperInBusiness;

public class GetDeveloperInBusinessHandler : IRequestHandler<GetDeveloperInBusinessRequest, GetDeveloperInBusinessResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetDeveloperInBusinessHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetDeveloperInBusinessResponse> Handle(GetDeveloperInBusinessRequest request, CancellationToken cancellationToken)
    {
        var response = await _connection.QuerySingleOrDefaultAsync<int?>("GetDeveloperInBusiness",
            new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId()
            });

        return new GetDeveloperInBusinessResponse
        {
            IsOriginalDeveloperStillInBusiness = (EApplicationDeveloperInBusinessType?)response
        };
    }
}
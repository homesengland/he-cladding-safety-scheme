using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetCountriesHandler : IRequestHandler<GetCountriesRequest, GetCountriesResponse>
    {
        private readonly IDbConnectionWrapper _connection;

        public GetCountriesHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
        }

        public async ValueTask<GetCountriesResponse> Handle(GetCountriesRequest request, CancellationToken cancellationToken)
        {
            var result = new GetCountriesResponse();
            var countries = await _connection.QueryAsync<Country>("GetCountries");
            result.Countries = countries?.ToList();

            return result;
        }
    }

    public class GetCountriesRequest : IRequest<GetCountriesResponse>
    {
        private GetCountriesRequest()
        {
        }

        public static readonly GetCountriesRequest Request = new();
    }

    public class GetCountriesResponse
    {
        public List<Country> Countries { get; set; }
    }
}

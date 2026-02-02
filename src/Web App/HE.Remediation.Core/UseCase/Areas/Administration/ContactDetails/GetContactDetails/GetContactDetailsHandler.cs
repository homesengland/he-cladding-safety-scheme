using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.GetContactDetails
{
    public class GetContactDetailsHandler : IRequestHandler<GetContactDetailsRequest, GetContactDetailsResponse>
    {        
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public GetContactDetailsHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async ValueTask<GetContactDetailsResponse> Handle(GetContactDetailsRequest request, CancellationToken cancellationToken)
        {
            var userId = _applicationDataProvider.GetUserId();            
            return await GetContactDetails(request);
        }

        private async ValueTask<GetContactDetailsResponse> GetContactDetails(GetContactDetailsRequest request)
        {
            var userId = _applicationDataProvider.GetUserId();
            var result = await _db.QuerySingleOrDefaultAsync<GetContactDetailsResponse>("GetUserContactDetails", new
            {
                userId
            });
            return result ?? new GetContactDetailsResponse();                        
        }
    }
}


using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails
{
    public class GetSecondaryContactDetailsHandler : IRequestHandler<GetSecondaryContactDetailsRequest, GetSecondaryContactDetailsResponse>
    {        
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public GetSecondaryContactDetailsHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<GetSecondaryContactDetailsResponse> Handle(GetSecondaryContactDetailsRequest request, CancellationToken cancellationToken)
        {
            var userId = _applicationDataProvider.GetUserId();            
            return await GetContactDetails(request);
        }

        private async Task<GetSecondaryContactDetailsResponse> GetContactDetails(GetSecondaryContactDetailsRequest request)
        {
            var userId = _applicationDataProvider.GetUserId();
            var result = await _db.QuerySingleOrDefaultAsync<GetSecondaryContactDetailsResponse>("GetUserSecondaryContactDetails", new
            {
                userId
            });
            return result ?? new GetSecondaryContactDetailsResponse();
        }
    }
}

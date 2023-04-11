using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetPursuedSourcesFunding
{
    public class GetPursuedSourcesFundingHandler : IRequestHandler<GetPursuedSourcesFundingRequest, GetPursuedSourcesFundingResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public GetPursuedSourcesFundingHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<GetPursuedSourcesFundingResponse> Handle(GetPursuedSourcesFundingRequest request, CancellationToken cancellationToken)
        {
            return await GetPursuedSourcesFunding();
        }

        private async Task<GetPursuedSourcesFundingResponse> GetPursuedSourcesFunding()
        {
            var response = await _db.QuerySingleOrDefaultAsync<GetPursuedSourcesFundingResponse>("GetPursuedSourcesFunding", new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId()
            });

            return response ?? new GetPursuedSourcesFundingResponse();
        }
    }
}

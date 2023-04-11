using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.GetSupportRequired
{
    public class GetSupportRequiredHandler : IRequestHandler<GetSupportRequiredRequest, GetSupportRequiredResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetSupportRequiredHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetSupportRequiredResponse> Handle(GetSupportRequiredRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetSupportRequiredResponse>("GetPreTenderSupportRequired",
                new { applicationId });

            return result ?? new GetSupportRequiredResponse();
        }
    }
}

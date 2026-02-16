using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.GetSupportRequired
{
    public class GetSupportRequiredHandler : IRequestHandler<GetSupportRequiredRequest, GetSupportRequiredResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IPreTenderRepository _preTenderRepository;

        public GetSupportRequiredHandler(
            IDbConnectionWrapper dbConnectionWrapper, 
            IApplicationDataProvider applicationDataProvider, 
            IPreTenderRepository preTenderRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _preTenderRepository = preTenderRepository;
        }

        public async ValueTask<GetSupportRequiredResponse> Handle(GetSupportRequiredRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetSupportRequiredResponse>("GetPreTenderSupportRequired",
                new { applicationId });

            var isSubmitted = await _preTenderRepository.IsPreTenderSubmitted(applicationId);

            if (result is not null)
            {
                result.IsSubmitted = isSubmitted;
            }

            return result ?? new GetSupportRequiredResponse
            {
                IsSubmitted = isSubmitted
            };
        }
    }
}

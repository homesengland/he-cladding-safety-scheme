using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetAccountGrantPaidTo
{
    public class GetAccountGrantPaidToHandler : IRequestHandler<GetAccountGrantPaidToRequest, GetAccountGrantPaidToResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetAccountGrantPaidToHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetAccountGrantPaidToResponse> Handle(GetAccountGrantPaidToRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetAccountGrantPaidToResponse>("GetBankAccountGrantPaidTo",
                new { applicationId });

            return result ?? new GetAccountGrantPaidToResponse();
        }
    }
}

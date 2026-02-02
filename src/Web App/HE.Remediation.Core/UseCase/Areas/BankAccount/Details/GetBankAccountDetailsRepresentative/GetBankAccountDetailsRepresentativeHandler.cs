using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsRepresentative
{
    public class GetBankAccountDetailsRepresentativeHandler : IRequestHandler<GetBankAccountDetailsRepresentativeRequest, GetBankAccountDetailsRepresentativeResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBankAccountDetailsRepresentativeHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetBankAccountDetailsRepresentativeResponse> Handle(GetBankAccountDetailsRepresentativeRequest request, CancellationToken cancellationToken)
        {
            return await GetBankAccountDetailsRepresentative(request);
        }

        private async ValueTask<GetBankAccountDetailsRepresentativeResponse> GetBankAccountDetailsRepresentative(GetBankAccountDetailsRepresentativeRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBankAccountDetailsRepresentativeResponse>("GetBankAccountDetailsRepresentative",
                new { applicationId });

            return result ?? new GetBankAccountDetailsRepresentativeResponse();
        }
    }
}

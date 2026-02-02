using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsRepresentative
{
    public class SetBankAccountDetailsRepresentativeHandler : IRequestHandler<SetBankAccountDetailsRepresentativeRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetBankAccountDetailsRepresentativeHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetBankAccountDetailsRepresentativeRequest request, CancellationToken cancellationToken)
        {
            await UpsertBankAccountDetailsRepresentative(request);
            return Unit.Value;
        }

        private async Task UpsertBankAccountDetailsRepresentative(SetBankAccountDetailsRepresentativeRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("UpsertBankAccountDetailsRepresentative", new { applicationId, request.NameOnTheAccount, request.BankName, request.BranchName, request.AccountNumber, request.SortCode, request.VatNumber });
        }
    }
}

using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetAccountGrantPaidTo
{
    public class SetAccountGrantPaidToHandler : IRequestHandler<SetAccountGrantPaidToRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetAccountGrantPaidToHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetAccountGrantPaidToRequest request, CancellationToken cancellationToken)
        {
            await UpsertAccountGrantPaidTo(request);
            return Unit.Value;
        }

        private async Task UpsertAccountGrantPaidTo(SetAccountGrantPaidToRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("UpsertBankAccountGrantPaidTo", new { applicationId, request.BankDetailsRelationship });
        }
    }
}

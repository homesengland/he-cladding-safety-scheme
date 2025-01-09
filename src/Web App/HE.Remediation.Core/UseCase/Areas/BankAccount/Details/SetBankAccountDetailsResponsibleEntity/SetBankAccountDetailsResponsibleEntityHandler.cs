using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsResponsibleEntity
{
    public class SetBankAccountDetailsResponsibleEntityHandler : IRequestHandler<SetBankAccountDetailsResponsibleEntityRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetBankAccountDetailsResponsibleEntityHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetBankAccountDetailsResponsibleEntityRequest request, CancellationToken cancellationToken)
        {
            await UpsertBankAccountDetailsResponsibleEntity(request);
            return Unit.Value;
        }

        private async Task UpsertBankAccountDetailsResponsibleEntity(SetBankAccountDetailsResponsibleEntityRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("UpsertBankAccountDetailsResponsibleEntity", new { applicationId, request.NameOnTheAccount, request.BankName, request.BranchName, request.AccountNumber, request.SortCode, request.VatNumber });
        }
    }
}
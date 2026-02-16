using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsResponsibleEntity
{
    public class GetBankAccountDetailsResponsibleEntityHandler : IRequestHandler<GetBankAccountDetailsResponsibleEntityRequest, GetBankAccountDetailsResponsibleEntityResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBankAccountDetailsResponsibleEntityHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetBankAccountDetailsResponsibleEntityResponse> Handle(GetBankAccountDetailsResponsibleEntityRequest request, CancellationToken cancellationToken)
        {
            return await GetBankAccountDetailsResponsibleEntity(request);
        }

        private async ValueTask<GetBankAccountDetailsResponsibleEntityResponse> GetBankAccountDetailsResponsibleEntity(GetBankAccountDetailsResponsibleEntityRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBankAccountDetailsResponsibleEntityResponse>("GetBankAccountDetailsResponsibleEntity", new { applicationId });

            return result ?? new GetBankAccountDetailsResponsibleEntityResponse();
        }
    }
}

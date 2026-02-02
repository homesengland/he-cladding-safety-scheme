using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.CheckYourAnswers.GetCheckYourAnswers
{
    public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IApplicationRepository _applicationRepository;

        public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db, IApplicationRepository applicationRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _applicationRepository = applicationRepository;
        }

        public async ValueTask<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            return await GetCheckYourAnswers();
        }

        private async ValueTask<GetCheckYourAnswersResponse> GetCheckYourAnswers()
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationStatus = await _applicationRepository.GetApplicationStatus(applicationId);

            var bankAccountAnswers = await _db.QuerySingleOrDefaultAsync<GetCheckYourAnswersResponse>("GetBankAccountCheckYourAnswers", new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId()
            });

            bankAccountAnswers.ReadOnly = applicationStatus.DeclarationConfirmed;

            return bankAccountAnswers ?? new GetCheckYourAnswersResponse();
        }
    }
}

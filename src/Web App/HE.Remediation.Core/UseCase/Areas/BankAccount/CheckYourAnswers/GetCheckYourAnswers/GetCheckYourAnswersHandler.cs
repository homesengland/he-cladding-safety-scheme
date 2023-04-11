using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

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

        public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            return await GetCheckYourAnswers();
        }

        private async Task<GetCheckYourAnswersResponse> GetCheckYourAnswers()
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

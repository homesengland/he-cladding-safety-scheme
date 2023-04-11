using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.CheckYourAnswers.SetCheckYourAnswers
{
    public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public SetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateBankAccountStatus(applicationId, (int)ETaskStatus.Completed);
            return Unit.Value;
        }

        private async Task UpdateBankAccountStatus(Guid applicationId, int taskStatusId)
        {
            await _db.ExecuteAsync("UpdateBankAccountStatus", new { applicationId, taskStatusId });
        }
    }
}

using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetCheckYourAnswers
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

        public async ValueTask<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateLeaseHolderEvidenceStatus(applicationId, (int)ETaskStatus.Completed);
            return Unit.Value;
        }

        private async Task UpdateLeaseHolderEvidenceStatus(Guid applicationId, int taskStatusId)
        {
            await _db.ExecuteAsync("UpdateLeaseHolderEvidenceStatus", new { applicationId, taskStatusId });
        }
    }
}

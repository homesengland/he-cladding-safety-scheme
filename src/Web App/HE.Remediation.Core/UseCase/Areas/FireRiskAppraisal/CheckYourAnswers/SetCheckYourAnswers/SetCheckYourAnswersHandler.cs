using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CheckYourAnswers.SetCheckYourAnswers
{
    public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IStatusTransitionService _statusTransitionService;

        public SetCheckYourAnswersHandler(
            IApplicationDataProvider applicationDataProvider, 
            IDbConnectionWrapper db, 
            IStatusTransitionService statusTransitionService)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _statusTransitionService = statusTransitionService;
        }

        public async ValueTask<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateFireRiskAppraisalStatus(applicationId, (int)ETaskStatus.Completed);

            await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.FraewSubmitted, applicationIds: applicationId);

            return Unit.Value;
        }

        private async Task UpdateFireRiskAppraisalStatus(Guid applicationId, int taskStatusId)
        {
            await _db.ExecuteAsync("UpdateFireRiskAppraisalStatus", new { applicationId, taskStatusId });
        }
    }
}

using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.SetCompletedAppraisal
{
    public class SetCompletedAppraisalHandler : IRequestHandler<SetCompletedAppraisalRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public SetCompletedAppraisalHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async ValueTask<Unit> Handle(SetCompletedAppraisalRequest request, CancellationToken cancellationToken)
        {
            await SetCompletedAppraisal(request);
            return Unit.Value;
        }

        private async Task SetCompletedAppraisal(SetCompletedAppraisalRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            await _db.ExecuteAsync("InsertOrUpdateFireRiskCompletedStatus", new 
            { 
                applicationId, 
                request.IsAppraisalCompleted 
            });
        }
    }
}

using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.SetBuildingDetailsComplete
{
    public class SetBuildingDetailsCompleteHandler : IRequestHandler<SetBuildingDetailsCompleteRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetBuildingDetailsCompleteHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetBuildingDetailsCompleteRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateBuildingDetailsTaskStatus(applicationId, (int)ETaskStatus.Completed);
            return Unit.Value;
        }

        private async Task UpdateBuildingDetailsTaskStatus(Guid applicationId, int taskStatusId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateBuildingDetailsTaskStatus", new { applicationId, taskStatusId });
        }
    }
}

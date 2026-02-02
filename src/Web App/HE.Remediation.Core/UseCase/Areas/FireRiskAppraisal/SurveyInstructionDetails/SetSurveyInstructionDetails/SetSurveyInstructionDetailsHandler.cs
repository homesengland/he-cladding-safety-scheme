using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.SetSurveyInstructionDetails
{
    public class SetSurveyInstructionDetailsHandler : IRequestHandler<SetSurveyInstructionDetailsRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IStatusTransitionService _statusTransitionService;

        public SetSurveyInstructionDetailsHandler(
            IDbConnectionWrapper dbConnectionWrapper, 
            IApplicationDataProvider applicationDataProvider, 
            IStatusTransitionService statusTransitionService)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _statusTransitionService = statusTransitionService;
        }

        public async ValueTask<Unit> Handle(SetSurveyInstructionDetailsRequest request, CancellationToken cancellationToken)
        {
            await InsertSurveyInstructionDetailsRequest(request);
            return Unit.Value;
        }

        private async Task InsertSurveyInstructionDetailsRequest(SetSurveyInstructionDetailsRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("InsertOrUpdateSurveyInstructionDetails", new { applicationId, request.FireRiskAssessorId, request.DateOfInstruction });

            await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.FraewInstructed, applicationIds: applicationId);
        }
    }
}

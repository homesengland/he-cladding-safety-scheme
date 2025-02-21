using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails
{
    public class SetAppraisalSurveyDetailsHandler : IRequestHandler<SetAppraisalSurveyDetailsRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IStatusTransitionService _statusTransitionService;

        public SetAppraisalSurveyDetailsHandler(
            IDbConnectionWrapper dbConnectionWrapper, 
            IApplicationDataProvider applicationDataProvider, 
            IStatusTransitionService statusTransitionService)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _statusTransitionService = statusTransitionService;
        }

        public async Task<Unit> Handle(SetAppraisalSurveyDetailsRequest request, CancellationToken cancellationToken)
        {
            await InsertAppraisalSurveyDetailsRequest(request);
            return Unit.Value;
        }

        private async Task InsertAppraisalSurveyDetailsRequest(SetAppraisalSurveyDetailsRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("InsertOrUpdateAppraisalSurveyDetails", new { applicationId, request.FireRiskAssessorId, request.DateOfInstruction, request.SurveyDate });

            await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.FraewInstructed, applicationIds: applicationId);
        }
    }
}

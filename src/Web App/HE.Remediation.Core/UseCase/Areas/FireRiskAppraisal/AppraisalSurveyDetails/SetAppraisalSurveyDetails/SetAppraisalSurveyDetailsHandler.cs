using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails
{
    public class SetAppraisalSurveyDetailsHandler : IRequestHandler<SetAppraisalSurveyDetailsRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetAppraisalSurveyDetailsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetAppraisalSurveyDetailsRequest request, CancellationToken cancellationToken)
        {
            await InsertAppraisalSurveyDetailsRequest(request);
            return Unit.Value;
        }

        private async Task<Unit> InsertAppraisalSurveyDetailsRequest(SetAppraisalSurveyDetailsRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("InsertOrUpdateAppraisalSurveyDetails", new { applicationId, request.FireRiskAssessorId, request.DateOfInstruction, request.SurveyDate });

            return Unit.Value;
        }
    }
}

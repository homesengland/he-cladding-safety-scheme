using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails
{
    public class SetAppraisalSurveyDetailsHandler : IRequestHandler<SetAppraisalSurveyDetailsRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;

        public SetAppraisalSurveyDetailsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
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

            var applicationStatus = await _applicationRepository.GetApplicationStatus(applicationId);

            if (applicationStatus != null && applicationStatus.InternalStatus == EApplicationInternalStatus.InitialApplicationSubmitted)
            {
                await _applicationRepository.UpdateInternalStatus(applicationId, EApplicationInternalStatus.FraewInstructed);
            }

            await _applicationRepository.UpdateInternalStatus(applicationId, EApplicationInternalStatus.FraewInstructed); 
            
            return Unit.Value;
        }
    }
}

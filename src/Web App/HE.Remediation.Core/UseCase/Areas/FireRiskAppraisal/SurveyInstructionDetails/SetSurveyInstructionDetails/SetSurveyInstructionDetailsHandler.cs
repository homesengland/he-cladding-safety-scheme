using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.SetSurveyInstructionDetails
{
    public class SetSurveyInstructionDetailsHandler : IRequestHandler<SetSurveyInstructionDetailsRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;

        public SetSurveyInstructionDetailsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
        }

        public async Task<Unit> Handle(SetSurveyInstructionDetailsRequest request, CancellationToken cancellationToken)
        {
            await InsertSurveyInstructionDetailsRequest(request);
            return Unit.Value;
        }

        private async Task<Unit> InsertSurveyInstructionDetailsRequest(SetSurveyInstructionDetailsRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("InsertOrUpdateSurveyInstructionDetails", new { applicationId, request.FireRiskAssessorId, request.DateOfInstruction });

            await _applicationRepository.UpdateInternalStatus(applicationId, EApplicationInternalStatus.FraewInstructed);

            return Unit.Value;
        }
    }
}

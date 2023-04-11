using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.GetSurveyInstructionDetails
{
    public class GetSurveyInstructionDetailsHandler : IRequestHandler<GetSurveyInstructionDetailsRequest, GetSurveyInstructionDetailsResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFireAssessorListRepository _fireAssessorListService;

        public GetSurveyInstructionDetailsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider, IFireAssessorListRepository fireAssessorListService)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _fireAssessorListService = fireAssessorListService;
        }

        public async Task<GetSurveyInstructionDetailsResponse> Handle(GetSurveyInstructionDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetSurveyInstructionDetailsResponse>("GetSurveyInstructionDetails",
                new { applicationId });

            if (response == null)
            {
                return new GetSurveyInstructionDetailsResponse { FireRiskAssessorCompanies = await _fireAssessorListService.GetFireAssessorList()};
            }
            else
            {
                response.FireRiskAssessorCompanies = await _fireAssessorListService.GetFireAssessorList();
                return response;
            }
        }
    }
}

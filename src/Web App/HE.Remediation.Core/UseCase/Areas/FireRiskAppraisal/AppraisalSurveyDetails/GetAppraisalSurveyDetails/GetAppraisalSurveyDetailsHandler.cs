using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.GetAppraisalSurveyDetails
{
    public class GetAppraisalSurveyDetailsHandler : IRequestHandler<GetAppraisalSurveyDetailsRequest, GetAppraisalSurveyDetailsResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFireAssessorListRepository _fireAssessorListService;

        public GetAppraisalSurveyDetailsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider, IFireAssessorListRepository fireAssessorListService)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _fireAssessorListService = fireAssessorListService;
        }

        public async Task<GetAppraisalSurveyDetailsResponse> Handle(GetAppraisalSurveyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetAppraisalSurveyDetailsResponse>("GetAppraisalSurveyDetails",
                new { applicationId });

            if (response == null)
            {
                return new GetAppraisalSurveyDetailsResponse { FireRiskAssessorCompanies = await _fireAssessorListService.GetFireAssessorList() };
            }
            else
            {
                response.FireRiskAssessorCompanies = await _fireAssessorListService.GetFireAssessorList();
                return response;
            }
        }
    }
}

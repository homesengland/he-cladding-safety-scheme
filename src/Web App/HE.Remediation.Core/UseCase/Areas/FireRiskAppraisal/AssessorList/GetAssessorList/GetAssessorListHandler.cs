using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorList.GetAssessorList
{
    public class GetAssessorListHandler : IRequestHandler<GetAssessorListRequest, GetAssessorListResponse>
    {
        private readonly IDbConnectionWrapper _db;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFireRiskAppraisalRepository _fireAssessorListService;

        public GetAssessorListHandler(IDbConnectionWrapper db, IApplicationDataProvider applicationDataProvider, IFireRiskAppraisalRepository fireAssessorListService)
        {
            _db = db;
            _applicationDataProvider = applicationDataProvider;
            _fireAssessorListService = fireAssessorListService;
        }

        public async Task<GetAssessorListResponse> Handle(GetAssessorListRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _db.QuerySingleOrDefaultAsync<string>("GetApplicationReferenceNumber", new { applicationId });

            return new GetAssessorListResponse
            {
                ApplicationId = applicationId,
                ApplicationReferenceNumber = applicationReferenceNumber,
                AssessorList = await _fireAssessorListService.GetFireAssessorList()
            };
        }
    }
}

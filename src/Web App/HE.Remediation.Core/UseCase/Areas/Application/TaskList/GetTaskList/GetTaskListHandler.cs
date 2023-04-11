using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.TaskList.GetTaskList
{
    public class GetTaskListHandler : IRequestHandler<GetTaskListRequest, GetTaskListResponse>
    {
        private readonly IDbConnectionWrapper _db;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetTaskListHandler(IDbConnectionWrapper db, IApplicationDataProvider applicationDataProvider)
        {
            _db = db;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetTaskListResponse> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationTaskListSummary = await _db.QuerySingleOrDefaultAsync<GetTaskListResponse>("GetApplicationTaskListSummary",
                new { applicationId });

            return new GetTaskListResponse
            {
                ApplicationId = applicationId,
                ApplicationReferenceNumber = applicationTaskListSummary.ApplicationReferenceNumber,
                ApplicationStatusId = applicationTaskListSummary.ApplicationStatusId,
                ApplicationLeaseHolderEngagementStatusId = applicationTaskListSummary.ApplicationLeaseHolderEngagementStatusId,
                ApplicationBuildingDetailsStatusId = applicationTaskListSummary.ApplicationBuildingDetailsStatusId,
                ApplicationResponsibleEntityStatusId = applicationTaskListSummary.ApplicationResponsibleEntityStatusId,
                ApplicationFundingRoutesStatusId = applicationTaskListSummary.ApplicationFundingRoutesStatusId,
                ApplicationBankDetailsStatusId = applicationTaskListSummary.ApplicationBankDetailsStatusId,
                ConfirmDeclarationStatusId = GetConfirmDeclarationStatus(applicationTaskListSummary.ConfirmDeclaration),
                ApplicationFireRiskAssessmentStatusId = applicationTaskListSummary.ApplicationFireRiskAssessmentStatusId
            };
        }

        private ETaskStatus GetConfirmDeclarationStatus(bool confirmDeclaration)
        {
            if (confirmDeclaration)
            {
                return ETaskStatus.Completed;
            }
            else
            {
                return ETaskStatus.NotStarted;
            }
        }
    }
}

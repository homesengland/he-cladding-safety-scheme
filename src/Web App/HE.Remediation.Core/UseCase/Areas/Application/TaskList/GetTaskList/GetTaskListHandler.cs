using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.TaskList.GetTaskList
{
    public class GetTaskListHandler : IRequestHandler<GetTaskListRequest, GetTaskListResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;

        public GetTaskListHandler(
            IApplicationDataProvider applicationDataProvider, 
            IApplicationRepository applicationRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
        }

        public async Task<GetTaskListResponse> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationTaskListSummary = await _applicationRepository.GetApplicationTaskListSummary(applicationId);

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
                ConfirmDeclarationStatusId = applicationTaskListSummary.ConfirmDeclaration ? ETaskStatus.Completed : ETaskStatus.NotStarted,
                ApplicationFireRiskAssessmentStatusId = applicationTaskListSummary.ApplicationFireRiskAssessmentStatusId,
                ApplicationFraStatusId = applicationTaskListSummary.ApplicationFraStatusId,
                IsDataIngestion = applicationTaskListSummary.IsDataIngestion
            };
        }
    }
}
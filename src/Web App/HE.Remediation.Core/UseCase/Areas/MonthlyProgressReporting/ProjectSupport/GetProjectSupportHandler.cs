using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport
{
    public partial class GetProjectSupportHandler : IRequestHandler<GetProjectSupportRequest, GetProjectSupportResponse>
    {
        private readonly IProgressReportingProjectSupportRepository _progressReportingProjectSupportRepository;
        private readonly IApplicationDetailsProvider _applicationDetailsProvider;
        private readonly ILogger<GetProjectPlanHandler> _logger;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetProjectSupportHandler(IProgressReportingProjectSupportRepository progressReportingProjectSupport,
            IApplicationDetailsProvider applicationDetailsProvider,
            ILogger<GetProjectPlanHandler> logger,
            IApplicationDataProvider applicationDataProvider)
        {
            _progressReportingProjectSupportRepository = progressReportingProjectSupport;
            _applicationDetailsProvider = applicationDetailsProvider;
            _logger = logger;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetProjectSupportResponse> Handle(GetProjectSupportRequest request, CancellationToken cancellationToken)
        {
            var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
            var applicationId = _applicationDataProvider.GetApplicationId();
            var progressReportId = _applicationDataProvider.GetProgressReportId();
            var projectSupport = await _progressReportingProjectSupportRepository.GetProjectSupportDetails(applicationDetails.ApplicationId, progressReportId);

            return new GetProjectSupportResponse
            {
                Id = projectSupport?.Id,
                ApplicationId = applicationDetails.ApplicationId,
                BuildingName = applicationDetails.BuildingName,
                ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
                RequiresSupport = projectSupport?.RequiresSupport,
                TaskStatusId = projectSupport?.TaskStatusId
            };
        }
    }
}
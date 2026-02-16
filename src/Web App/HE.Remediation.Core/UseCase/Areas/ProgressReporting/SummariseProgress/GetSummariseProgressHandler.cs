using Amazon.Runtime.Internal;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeaseholderInformedLast;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.Remediation.Core.Data.StoredProcedureParameters;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SummariseProgress
{
    public class GetSummariseProgressHandler : IRequestHandler<GetSummariseProgressRequest, GetSummariseProgressResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IProgressReportingRepository _progressReportingRepository;

        public GetSummariseProgressHandler(IApplicationDataProvider applicationDataProvider,
                                               IBuildingDetailsRepository buildingDetailsRepository,
                                               IApplicationRepository applicationRepository,
                                               IProgressReportingRepository progressReportingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _progressReportingRepository = progressReportingRepository;
        }

        public async ValueTask<GetSummariseProgressResponse> Handle(GetSummariseProgressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var progressSummary = await _progressReportingRepository.GetProgressReportProgressSummary();

            var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });
            
            return new GetSummariseProgressResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                ProgressSummary = progressSummary.ProgressSummary,
                GoalSummary = progressSummary.GoalSummary,
                IsSupportNeeded = progressSummary.RequiresSupport,
                HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
            };
        }
    }

    public class GetSummariseProgressRequest : IRequest<GetSummariseProgressResponse>
    {
        private GetSummariseProgressRequest() { }

        public static readonly GetSummariseProgressRequest Request = new();
    }

    public class GetSummariseProgressResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public string ProgressSummary { get; set; }
        public string GoalSummary { get; set; }
        public bool? IsSupportNeeded { get; set; }
        public bool HasVisitedCheckYourAnswers { get; set; }
    }

}

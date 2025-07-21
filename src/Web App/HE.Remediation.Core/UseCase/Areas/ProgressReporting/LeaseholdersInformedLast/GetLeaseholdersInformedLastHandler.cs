using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeaseholderInformedLast
{
    public class GetLeaseholdersInformedLastHandler : IRequestHandler<GetLeaseholdersInformedLastRequest, GetLeaseholdersInformedLastResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IProgressReportingRepository _progressReportingRepository;

        public GetLeaseholdersInformedLastHandler(IApplicationDataProvider applicationDataProvider,
                                               IBuildingDetailsRepository buildingDetailsRepository,
                                               IApplicationRepository applicationRepository,
                                               IProgressReportingRepository progressReportingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<GetLeaseholdersInformedLastResponse> Handle(GetLeaseholdersInformedLastRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var informedLastDate = await _progressReportingRepository.GetProgressReportLeaseholdersInformedLastDate();

            var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
                new GetHasVisitedCheckYourAnswersParameters
                {
                    ApplicationId = applicationId,
                    ProgressReportId = _applicationDataProvider.GetProgressReportId()
                });

            return new GetLeaseholdersInformedLastResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                LeaseholdersInformedLastDate = informedLastDate,
                HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
            };
        }
    }

    public class GetLeaseholdersInformedLastRequest : IRequest<GetLeaseholdersInformedLastResponse>
    {
        private GetLeaseholdersInformedLastRequest() { }

        public static readonly GetLeaseholdersInformedLastRequest Request = new();
    }

    public class GetLeaseholdersInformedLastResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public DateTime? LeaseholdersInformedLastDate { get; set; }
        public bool HasVisitedCheckYourAnswers { get; set; }
    }
}
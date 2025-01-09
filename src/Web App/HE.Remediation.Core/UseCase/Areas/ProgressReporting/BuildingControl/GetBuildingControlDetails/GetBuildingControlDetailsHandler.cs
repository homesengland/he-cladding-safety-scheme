using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.GetBuildingControlDetails
{
    public class GetBuildingControlDetailsHandler : IRequestHandler<GetBuildingControlDetailsRequest, GetBuildingControlDetailsResponse>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;

        public GetBuildingControlDetailsHandler(IProgressReportingRepository progressReportingRepository, IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository, IBuildingDetailsRepository buildingDetailsRepository)
        {
            _progressReportingRepository = progressReportingRepository;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
        }

        public async Task<GetBuildingControlDetailsResponse> Handle(GetBuildingControlDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
            var version = await _progressReportingRepository.GetProgressReportVersion();
            var result = await _progressReportingRepository.GetBuildingControlDetails();

            return new GetBuildingControlDetailsResponse
            {
                ForecastDateMonth = result.ForecastDate?.Month,
                ForecastDateYear = result.ForecastDate?.Year,
                ActualDateMonth = result.ActualDate?.Month,
                ActualDateYear = result.ActualDate?.Year,
                DecisionDateMonth = result.DecisionDate?.Month,
                DecisionDateYear = result.DecisionDate?.Year,
                ValidationDateMonth = result.ValidationDate?.Month,
                ValidationDateYear = result.ValidationDate?.Year,
                Decision = result.Decision,
                ApplicationReferenceNumber = applicationReferenceNumber,
                BuildingName = buildingName,
                Version = version,
            };
        }
    }

    public class GetBuildingControlDetailsRequest : IRequest<GetBuildingControlDetailsResponse>
    {
        private GetBuildingControlDetailsRequest()
        {
            
        }

        public static readonly GetBuildingControlDetailsRequest Request = new();
    }

    public class GetBuildingControlDetailsResponse
    {
        public int? ForecastDateMonth { get; set; }
        public int? ForecastDateYear { get; set; }
        public int? ActualDateMonth { get; set; }
        public int? ActualDateYear { get; set; }
        public int? ValidationDateMonth { get; set; }
        public int? ValidationDateYear { get; set; }
        public int? DecisionDateMonth { get; set; }
        public int? DecisionDateYear { get; set; }
        public bool? Decision { get; set; }
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public int Version { get; set; }
    }
}

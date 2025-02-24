using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode
{
    public class GetBuildingSafetyRegulatorRegistrationCodeHandler : IRequestHandler<GetBuildingSafetyRegulatorRegistrationCodeRequest, GetBuildingSafetyRegulatorRegistrationCodeResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IProgressReportingRepository _progressReportingRepository;

        public GetBuildingSafetyRegulatorRegistrationCodeHandler(IApplicationDataProvider applicationDataProvider,
            IBuildingDetailsRepository buildingDetailsRepository,
            IApplicationRepository applicationRepository,
            IProgressReportingRepository progressReportingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<GetBuildingSafetyRegulatorRegistrationCodeResponse> Handle(GetBuildingSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var progressReportBuildingSafetyRegulatorRegistrationCode = await _progressReportingRepository.GetProgressReportBuildingSafetyRegulatorRegistrationCode();

            var version = await _progressReportingRepository.GetProgressReportVersion();

            return new GetBuildingSafetyRegulatorRegistrationCodeResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                BuildingSafetyRegulatorRegistrationCode = progressReportBuildingSafetyRegulatorRegistrationCode,
                Version = version
            };
        }
    }
}

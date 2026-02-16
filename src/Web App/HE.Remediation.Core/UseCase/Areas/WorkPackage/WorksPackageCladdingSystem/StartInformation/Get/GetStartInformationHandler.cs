using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.StartInformation.Get
{
    public class GetStartInformationHandler : IRequestHandler<GetStartInformationRequest, GetStartInformationResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;

        public GetStartInformationHandler(IApplicationDataProvider applicationDataProvider,
                                       IBuildingDetailsRepository buildingDetailsRepository,
                                       IApplicationRepository applicationRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
        }

        public async ValueTask<GetStartInformationResponse> Handle(GetStartInformationRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            return new GetStartInformationResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber
            };
        }
    }
}

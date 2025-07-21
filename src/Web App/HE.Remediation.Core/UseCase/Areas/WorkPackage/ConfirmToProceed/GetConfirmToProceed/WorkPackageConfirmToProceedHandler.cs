using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.GetConfirmToProceed
{
    public class WorkPackageConfirmToProceedHandler : IRequestHandler<GetWorkPackageConfirmToProceedRequest, GetWorkPackageConfirmToProceedResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IWorkPackageRepository _workPackageRepository;

        public WorkPackageConfirmToProceedHandler(IApplicationDataProvider applicationDataProvider,
                                  IBuildingDetailsRepository buildingDetailsRepository,
                                  IApplicationRepository applicationRepository,
                                  IWorkPackageRepository workPackageRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _workPackageRepository = workPackageRepository;
        }

        public async Task<GetWorkPackageConfirmToProceedResponse> Handle(GetWorkPackageConfirmToProceedRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var isConfirmedToProceed = await _workPackageRepository.GetWorkPackageConfirmToProceed();

            ENoYes? isConfirmedToProceedValue = isConfirmedToProceed == null ? null :
                                           isConfirmedToProceed == true ? ENoYes.Yes : ENoYes.No;

            return new GetWorkPackageConfirmToProceedResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                IsConfirmedToProceed = isConfirmedToProceedValue,
            };
        }

    }
}
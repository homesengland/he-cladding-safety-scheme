using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.CheckYourAnswers;

    public class GetInternalDefectsCheckYourAnswersHandler : IRequestHandler<GetInternalDefectsCheckYourAnswersRequest, GetInternalDefectsCheckYourAnswersResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IWorkPackageRepository _workPackageRepository;

        public GetInternalDefectsCheckYourAnswersHandler(
            IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository,
            IBuildingDetailsRepository buildingDetailsRepository,
            IWorkPackageRepository workPackageRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
            _workPackageRepository = workPackageRepository;
        }

        public async Task<GetInternalDefectsCheckYourAnswersResponse> Handle(GetInternalDefectsCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var applicationId = _applicationDataProvider.GetApplicationId();

            var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
            var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

            var answers = await _workPackageRepository.GetInternalDefectsCost();

            return new GetInternalDefectsCheckYourAnswersResponse
            {

                InternalDefectsCost = answers?.InternalDefectsCosts,
                Description = answers?.Description,
                ApplicationReferenceNumber = reference,
                BuildingName = buildingName,
                IsSubmitted = isSubmitted
            };
        }
    }

    public class GetInternalDefectsCheckYourAnswersRequest : IRequest<GetInternalDefectsCheckYourAnswersResponse>
    {
        private GetInternalDefectsCheckYourAnswersRequest()
        {
        }

        public static readonly GetInternalDefectsCheckYourAnswersRequest Request = new();
    }

    public class GetInternalDefectsCheckYourAnswersResponse
    {
        public decimal? InternalDefectsCost { get; set; }
        public string Description { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
    }

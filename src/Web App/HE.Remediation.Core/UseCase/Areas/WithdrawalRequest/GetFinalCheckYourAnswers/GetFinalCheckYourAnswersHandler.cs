using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetReasonForClosing;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetFinalCheckYourAnswers
{
    public class GetFinalCheckYourAnswersHandler : IRequestHandler<GetFinalCheckYourAnswersRequest, GetFinalCheckYourAnswersResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IClosingReportRepository _closingReportRepository;

        public GetFinalCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
                                     IApplicationRepository applicationRepository,
                                     IBuildingDetailsRepository buildingDetailsRepository,
                                     IClosingReportRepository closingReportRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
            _closingReportRepository = closingReportRepository;
        }

        public async Task<GetFinalCheckYourAnswersResponse> Handle(GetFinalCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

            var reasonForClosing = await _applicationRepository.GetApplicationReasonForWithdrawalRequest(applicationId);

            return new GetFinalCheckYourAnswersResponse
            {
                ReasonForClosing = reasonForClosing,
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                IsSubmitted = isSubmitted
            };
        }
    }
}

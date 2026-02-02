using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;


namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetReasonForClosing
{
    public class GetReasonForClosingHandler : IRequestHandler<GetReasonForClosingRequest, GetReasonForClosingResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IClosingReportRepository _closingReportRepository;

        public GetReasonForClosingHandler(IApplicationDataProvider applicationDataProvider,
                                     IApplicationRepository applicationRepository,
                                     IBuildingDetailsRepository buildingDetailsRepository,
                                     IClosingReportRepository closingReportRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
            _closingReportRepository = closingReportRepository;
        }

        public async ValueTask<GetReasonForClosingResponse> Handle(GetReasonForClosingRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

            var reasonForClosing = await _applicationRepository.GetApplicationReasonForWithdrawalRequest(applicationId);

            return new GetReasonForClosingResponse
            {
                ReasonForClosing = reasonForClosing,
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                IsSubmitted = isSubmitted
            };
        }
    }
}

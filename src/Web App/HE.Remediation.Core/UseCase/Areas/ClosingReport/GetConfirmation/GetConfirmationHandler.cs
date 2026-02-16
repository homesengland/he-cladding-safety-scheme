using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetConfirmation;

public class GetConfirmationHandler : IRequestHandler<GetConfirmationRequest, GetConfirmationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IClosingReportRepository _closingReportRepository;

    public GetConfirmationHandler(IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        IClosingReportRepository closingReportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _closingReportRepository = closingReportRepository;
    }

    public async ValueTask<GetConfirmationResponse> Handle(GetConfirmationRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var confirmationDetails = await _closingReportRepository.GetClosingReportConfirmationDetails(applicationId);
        var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

        return new GetConfirmationResponse
        {
            IsSubmitted = isSubmitted,
            FinalCostReport = confirmationDetails?.FinalCostReport,
            ExitFraew = confirmationDetails?.ExitFraew,
            CompletionCertificate = confirmationDetails?.CompletionCertificate,
            InformedPracticalCompletion = confirmationDetails?.InformedPracticalCompletion,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
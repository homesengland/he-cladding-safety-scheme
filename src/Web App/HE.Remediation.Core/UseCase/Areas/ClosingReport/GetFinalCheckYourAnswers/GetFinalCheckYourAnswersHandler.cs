using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetFinalCheckYourAnswers;

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

    public async ValueTask<GetFinalCheckYourAnswersResponse> Handle(GetFinalCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

        var exitFraewFiles = await _closingReportRepository.GetFiles(applicationId, EClosingReportFileType.ExitFraew);
        var practicalCompletionCertificateFiles = await _closingReportRepository.GetFiles(applicationId, EClosingReportFileType.PracticalCompletionCertificate); 
        var buildingRegulationsFiles = await _closingReportRepository.GetFiles(applicationId, EClosingReportFileType.BuildingRegulations);
        var leaseholderResidentCommunication = await _closingReportRepository.GetFiles(applicationId, EClosingReportFileType.LeaseholderResidentCommunication);
        var finalCostFiles = await _closingReportRepository.GetFiles(applicationId, EClosingReportFileType.FinalCost);

        var finalPaymentRequest = await _closingReportRepository.GetClosingReportRequestedAmount(applicationId);

        return new GetFinalCheckYourAnswersResponse
        {
            ExitFraewFiles = exitFraewFiles is not null ? exitFraewFiles.ToList() : new List<FileResult>(),
            PracticalCompletionCertificateFiles = practicalCompletionCertificateFiles is not null ? practicalCompletionCertificateFiles.ToList() : new List<FileResult>(),
            BuildingRegulationsFiles = buildingRegulationsFiles is not null ? buildingRegulationsFiles.ToList() : new List<FileResult>(),
            LeaseholderResidentCommunication = leaseholderResidentCommunication is not null ? leaseholderResidentCommunication.ToList() : new List<FileResult>(),
            FinalCostFiles = finalCostFiles is not null ? finalCostFiles.ToList() : new List<FileResult>(),
            FinalPaymentRequest = finalPaymentRequest,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted
        };
    }
}

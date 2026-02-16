using Amazon.Runtime.Internal;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class GetUploadFireRiskAssessmentReportHandler : IRequestHandler<GetUploadFireRiskAssessmentReportRequest, GetUploadFireRiskAssessmentReportResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetUploadFireRiskAssessmentReportHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async ValueTask<GetUploadFireRiskAssessmentReportResponse> Handle(GetUploadFireRiskAssessmentReportRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var report = await _fireRiskAssessmentRepository.GetWorkPackageFraReport(applicationId);

        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetWorkPackageFraVisitedCheckYourAnswers(applicationId);

        return new GetUploadFireRiskAssessmentReportResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            FireRiskAssessmentType = report?.FireRiskAssessmentTypeId,
            AddedFiles = report?.File is not null ? new List<FileResult> { report.File } : new List<FileResult>(),
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetUploadFireRiskAssessmentReportRequest : IRequest<GetUploadFireRiskAssessmentReportResponse>
{
    private GetUploadFireRiskAssessmentReportRequest()
    {
    }

    public static readonly GetUploadFireRiskAssessmentReportRequest Request = new();
}

public class GetUploadFireRiskAssessmentReportResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public List<FileResult> AddedFiles { get; set; } = new List<FileResult>();
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
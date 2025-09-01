using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class GetReportHandler : IRequestHandler<GetReportRequest, GetReportResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;
    private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;

    public GetReportHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository, 
        IFireRiskAppraisalRepository fireRiskAppraisalRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        _fireRiskAppraisalRepository = fireRiskAppraisalRepository;
    }

    public async Task<GetReportResponse> Handle(GetReportRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var assessors = await _fireRiskAppraisalRepository.GetFireAssessorList();

        var result = await _fireRiskAssessmentRepository.GetWorkPackageFraAssessorAndDate(applicationId);

        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetWorkPackageFraVisitedCheckYourAnswers(applicationId);

        return new GetReportResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            AssessorId = result?.FireRiskAssessorListId,
            FraDate = result?.FireRiskAssessmentDate,
            VisitedCheckYourAnswers = visitedCheckYourAnswers,
            FireRiskAssessorCompanies = assessors
        };
    }
}

public class GetReportRequest : IRequest<GetReportResponse>
{
    private GetReportRequest()
    {
    }

    public static readonly GetReportRequest Request = new();
}

public class GetReportResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? AssessorId { get; set; }
    public DateTime? FraDate { get; set; }
    public IList<GetFireRiskAssessorListResult> FireRiskAssessorCompanies { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
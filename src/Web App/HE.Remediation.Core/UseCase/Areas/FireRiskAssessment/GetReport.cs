using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetReportHandler : IRequestHandler<GetReportRequest, GetReportResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;
    private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;

    public GetReportHandler(
        IApplicationDataProvider applicationDataProvider,
        IFireRiskAssessmentRepository fireRiskAssessmentRepository,
        IFireRiskAppraisalRepository fireRiskAppraisalRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        _fireRiskAppraisalRepository = fireRiskAppraisalRepository;
    }

    public async ValueTask<GetReportResponse> Handle(GetReportRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationScheme = _applicationDataProvider.GetApplicationScheme();

        int? fraTypeId = null;
        if (applicationScheme == EApplicationScheme.ResponsibleActorsScheme)
        {
            fraTypeId = await _fireRiskAssessmentRepository.GetFraCommissionerType(applicationId);
        }

        var assessors = await _fireRiskAppraisalRepository.GetFireAssessorList();

        var assessorAndFraDate = await _fireRiskAssessmentRepository.GetAssessorAndFraDate(applicationId);
        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

        return new GetReportResponse
        {
            AssessorId = assessorAndFraDate.FireRiskAssessorListId,
            FraDate = assessorAndFraDate.FireRiskAssessmentDate,
            FraCommissionerType = fraTypeId.HasValue? (EFraCommissionerType?)fraTypeId.Value: null,
            ApplicationScheme = applicationScheme,
            FireRiskAssessorCompanies = assessors,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
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
    public int? AssessorId { get; set; }
    public DateTime? FraDate { get; set; }
    public EFraCommissionerType? FraCommissionerType { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
    public IList<GetFireRiskAssessorListResult> FireRiskAssessorCompanies { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
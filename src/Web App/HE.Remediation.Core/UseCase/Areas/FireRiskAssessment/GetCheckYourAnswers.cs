using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var answers = await _fireRiskAssessmentRepository.GetFraCheckYourAnswers(applicationId);
        await _fireRiskAssessmentRepository.SetFraVisitedCheckYourAnswers(new SetFraVisitedCheckYourAnswersParameters
        {
            ApplicationId = applicationId,
            VisitedCheckYourAnswers = true
        });

        return new GetCheckYourAnswersResponse
        {
            HasFra = answers.HasFra,
            FraFile = answers.FraFile,
            FireRiskAssessmentType = answers.FireRiskAssessmentTypeId,
            FireRiskAssessor = answers.FireRiskAssessor,
            OtherAssessorFirstName = answers.OtherAssessorFirstName,
            OtherAssessorLastName = answers.OtherAssessorLastName,
            OtherAssessorCompanyName = answers.OtherAssessorCompanyName,
            OtherAssessorCompanyNumber = answers.OtherAssessorCompanyNumber,
            OtherAssessorEmailAddress = answers.OtherAssessorEmailAddress,
            OtherAssessorTelephone = answers.OtherAssessorTelephone,
            FireRiskAssessmentDate = answers.FireRiskAssessmentDate,
            FireRiskRating = answers.FireRiskRatingId,
            HasInternalFireSafetyRisks = answers.HasInternalFireSafetyRisks,
            HasFunding = answers.HasFunding,
            FraFundingType = answers.FraFundingTypeId,
            OtherInternalFireSafetyRisk = answers.OtherInternalFireSafetyRisk,
            IsSubmitted = answers.IsSubmitted,
            Defects = answers.Defects
        };
    }
}

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static readonly GetCheckYourAnswersRequest Request = new();
}

public class GetCheckYourAnswersResponse
{
    public bool? HasFra { get; set; }
    public string FraFile { get; set; }
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }
    public string FireRiskAssessor { get; set; }
    public string OtherAssessorFirstName { get; set; }
    public string OtherAssessorLastName { get; set; }
    public string OtherAssessorCompanyName { get; set; }
    public string OtherAssessorCompanyNumber { get; set; }
    public string OtherAssessorEmailAddress { get; set; }
    public string OtherAssessorTelephone { get; set; }
    public DateTime? FireRiskAssessmentDate { get; set; }
    public EFraRiskRating? FireRiskRating { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }
    public bool? HasFunding { get; set; }
    public EFraFundingType? FraFundingType { get; set; }
    public string OtherInternalFireSafetyRisk { get; set; }
    public bool IsSubmitted { get; set; }
    public IList<string> Defects { get; set; } = new List<string>();
}
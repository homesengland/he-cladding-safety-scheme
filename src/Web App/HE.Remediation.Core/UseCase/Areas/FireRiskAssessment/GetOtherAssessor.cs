using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetOtherAssessorHandler : IRequestHandler<GetOtherAssessorRequest, GetOtherAssessorResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetOtherAssessorHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async ValueTask<GetOtherAssessorResponse> Handle(GetOtherAssessorRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var details = await _fireRiskAssessmentRepository.GetOtherAssessor(applicationId);
        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

        return new GetOtherAssessorResponse
        {
            FirstName = details?.OtherAssessorFirstName,
            LastName = details?.OtherAssessorLastName,
            CompanyName = details?.OtherAssessorCompanyName,
            CompanyNumber = details?.OtherAssessorCompanyNumber,
            EmailAddress = details?.OtherAssessorEmailAddress,
            Telephone = details?.OtherAssessorTelephone,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetOtherAssessorRequest : IRequest<GetOtherAssessorResponse>
{
    private GetOtherAssessorRequest()
    {
    }

    public static readonly GetOtherAssessorRequest Request = new();
}

public class GetOtherAssessorResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CompanyName { get; set; }
    public string CompanyNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Telephone { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
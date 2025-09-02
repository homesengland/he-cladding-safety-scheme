using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class GetOtherAssessorHandler : IRequestHandler<GetOtherAssessorRequest, GetOtherAssessorResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetOtherAssessorHandler(
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

    public async Task<GetOtherAssessorResponse> Handle(GetOtherAssessorRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var assessor = await _fireRiskAssessmentRepository.GetWorkPackageFraOtherAssessor(applicationId);
        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetWorkPackageFraVisitedCheckYourAnswers(applicationId);

        return new GetOtherAssessorResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            FirstName = assessor?.OtherAssessorFirstName,
            LastName = assessor?.OtherAssessorLastName,
            CompanyName = assessor?.OtherAssessorCompanyName,
            CompanyNumber = assessor?.OtherAssessorCompanyNumber,
            EmailAddress = assessor?.OtherAssessorEmailAddress,
            Telephone = assessor?.OtherAssessorTelephone,
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
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CompanyName { get; set; }
    public string CompanyNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Telephone { get; set; }

    public bool VisitedCheckYourAnswers { get; set; }
}
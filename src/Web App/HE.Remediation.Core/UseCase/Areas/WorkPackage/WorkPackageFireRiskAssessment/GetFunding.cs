using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class GetFundingHandler : IRequestHandler<GetFundingRequest, GetFundingResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetFundingHandler(
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

    public async ValueTask<GetFundingResponse> Handle(GetFundingRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var funding = await _fireRiskAssessmentRepository.GetWorkPackageFraFunding(applicationId);
        var defects = await _fireRiskAssessmentRepository.GetWorkPackageFraInternalDefects(applicationId);
        var visistedCheckYourAnswers = await _fireRiskAssessmentRepository.GetWorkPackageFraVisitedCheckYourAnswers(applicationId);

        return new GetFundingResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            VisitedCheckYourAnswers = visistedCheckYourAnswers,
            HasFunding = funding?.HasFunding,
            HasFundingType = funding?.HasFunding == true ? funding?.FraFundingTypeId : null,
            HasNoFundingType = funding?.HasFunding == false ? funding?.FraFundingTypeId : null,
            HasDefects = defects.Count > 0
        };
    }
}

public class GetFundingRequest : IRequest<GetFundingResponse>
{
    private GetFundingRequest()
    {
    }

    public static readonly GetFundingRequest Request = new();
}

public class GetFundingResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool? HasFunding { get; set; }
    public EFraFundingType? HasFundingType { get; set; }
    public EFraFundingType? HasNoFundingType { get; set; }

    public bool HasDefects { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
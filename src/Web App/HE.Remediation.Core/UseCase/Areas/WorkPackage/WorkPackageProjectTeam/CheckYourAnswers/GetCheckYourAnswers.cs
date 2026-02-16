using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.CheckYourAnswers;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetCheckYourAnswersHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMembers = await _workPackageRepository.GetTeamMembers();
        var regulatoryCompliance = await _workPackageRepository.GetRegulatoryComplianceTeamMember();
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetCheckYourAnswersResponse
        {
            ApplicationReferenceNumber = referenceNumber,
            BuildingName = buildingName,
            TeamMembers = teamMembers,
            RegulatoryCompliancePerson = regulatoryCompliance.RegulatoryCompliancePerson,
            RegulatoryCompliancePersonRole = regulatoryCompliance.Role,
            IsSubmitted = isSubmitted
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
    public List<ProjectTeamMembersResult> TeamMembers { get; set; }
    public string RegulatoryCompliancePerson { get; set; }
    public string RegulatoryCompliancePersonRole { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.RegulatoryCompliance;

public class GetRegulatoryComplianceHandler : IRequestHandler<GetRegulatoryComplianceRequest, GetRegulatoryComplianceResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetRegulatoryComplianceHandler(
        IApplicationRepository applicationRepository, 
        IApplicationDataProvider applicationDataProvider, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageRepository workPackageRepository)
    {
        _applicationRepository = applicationRepository;
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetRegulatoryComplianceResponse> Handle(GetRegulatoryComplianceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationReference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var projectTeamMembers = await _workPackageRepository.GetTeamMembers();
        var regulatoryComplianceTeamMemberId = await _workPackageRepository.GetWorkPackageRegulatoryCompliance();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetRegulatoryComplianceResponse
        {
            ApplicationReferenceNumber = applicationReference,
            BuildingName = buildingName,
            TeamMembers = projectTeamMembers,
            RegulatoryCompliancePersonId = regulatoryComplianceTeamMemberId,
            IsSubmitted = isSubmitted
        };
    }
}

public class GetRegulatoryComplianceRequest : IRequest<GetRegulatoryComplianceResponse>
{
    private GetRegulatoryComplianceRequest()
    {
    }

    public static readonly GetRegulatoryComplianceRequest Request = new();
}

public class GetRegulatoryComplianceResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public List<ProjectTeamMembersResult> TeamMembers { get; set; }
    public Guid? RegulatoryCompliancePersonId { get; set; }
    public bool IsSubmitted { get; set; }
}
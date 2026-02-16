
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.GetAddRole;

public class GetAddRoleHandler : IRequestHandler<GetAddRoleRequest, GetAddRoleResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetAddRoleHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetAddRoleResponse> Handle(GetAddRoleRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var version = await _progressReportingRepository.GetProgressReportVersion();

        var companyDetails = await _progressReportingRepository.GetLeadDesignerCompanyDetails();

        var teamMembers = await _progressReportingRepository.GetTeamMembers();
        var allOptions = Enum.GetValues<ETeamRole>().ToList();
        var consumedOptions = teamMembers.Select(tm => (ETeamRole)tm.RoleId);
        var availableOptions = allOptions.Except(consumedOptions).ToList();

        // Other should always be available.
        if (!availableOptions.Contains(ETeamRole.Other))
        {
            availableOptions.Add(ETeamRole.Other);
        }

        return new GetAddRoleResponse
        {
            ShowLeadDesigner = companyDetails is null,
            BuildingName = buildingName,
            AvailableTeamRoles = availableOptions,
            ApplicationReferenceNumber = applicationReferenceNumber,
            Version = version
        };
    }
}

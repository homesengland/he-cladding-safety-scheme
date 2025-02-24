﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.GetWorksRequirePermission;

public class GetWorksRequirePermissionHandler : IRequestHandler<GetWorksRequirePermissionRequest, GetWorksRequirePermissionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetWorksRequirePermissionHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetWorksRequirePermissionResponse> Handle(GetWorksRequirePermissionRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var permissionRequired = await _progressReportingRepository.GetProgressReportRequirePlanningPermission();
        var quotesSought = await _progressReportingRepository.GetProgressReportQuotesSought();

        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new GetWorksRequirePermissionResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            PermissionRequired = permissionRequired,
            QuotesSought = quotesSought,
            Version = version
        };
    }
}

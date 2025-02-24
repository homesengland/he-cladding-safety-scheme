﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Get;

public class GetDeclarationHandler : IRequestHandler<GetDeclarationRequest, GetDeclarationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetDeclarationHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<GetDeclarationResponse> Handle(GetDeclarationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        var projectDates = await _scheduleOfWorksRepository.GetProjectDates();

        var declaration = await _scheduleOfWorksRepository.GetDeclaration();

        return new GetDeclarationResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ConfirmedAccuratelyProfiledCosts = declaration.ConfirmedAccuratelyProfiledCosts,
            ConfirmedAwareOfProcess = declaration.ConfirmedAwareOfProcess,
            ConfirmedAwareOfVariationApproval = declaration.ConfirmedAwareOfVariationApproval,
            ProjectStartDate = projectDates.ProjectStartDate,
            IsSubmitted = isSubmitted
        };
    }
}

﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetUpload;

public class GetUploadHandler : IRequestHandler<GetUploadRequest, GetUploadResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IClosingReportRepository _closingReportRepository;

    public GetUploadHandler(IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        IClosingReportRepository closingReportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _closingReportRepository = closingReportRepository;
    }

    public async Task<GetUploadResponse> Handle(GetUploadRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var fileList = await _closingReportRepository.GetFiles(applicationId, request.UploadType);
        var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

        return new GetUploadResponse
        {
            IsSubmitted = isSubmitted,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            AddedFiles = fileList,
            UploadType = request.UploadType
        };
    }
}
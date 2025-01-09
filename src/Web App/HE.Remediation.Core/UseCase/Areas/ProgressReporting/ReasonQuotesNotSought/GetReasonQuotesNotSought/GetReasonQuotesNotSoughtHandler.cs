﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.GetReasonQuotesNotSought;

public class GetReasonQuotesNotSoughtHandler : IRequestHandler<GetReasonQuotesNotSoughtRequest, GetReasonQuotesNotSoughtResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetReasonQuotesNotSoughtHandler(IApplicationDataProvider applicationDataProvider,
                                          IBuildingDetailsRepository buildingDetailsRepository,
                                          IApplicationRepository applicationRepository,
                                          IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetReasonQuotesNotSoughtResponse> Handle(GetReasonQuotesNotSoughtRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var version = await _progressReportingRepository.GetProgressReportVersion();

        var quotesNotSoughtReason = await _progressReportingRepository.GetProgressReportQuotesNotSoughtReason();
        return new GetReasonQuotesNotSoughtResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            WhyYouHaveNotSoughtQuotes = quotesNotSoughtReason?.WhyYouHaveNotSoughtQuotes,
            QuotesNotSoughtReason = quotesNotSoughtReason?.QuotesNotSoughtReason,
            QuotesNeedsSupport = quotesNotSoughtReason?.QuotesNeedsSupport,
            Version = version
        };
    }
}

using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.GetSoughtQuotes;

public class GetSoughtQuotesHandler : IRequestHandler<GetSoughtQuotesRequest, GetSoughtQuotesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetSoughtQuotesHandler(IApplicationDataProvider applicationDataProvider,
                                  IBuildingDetailsRepository buildingDetailsRepository,
                                  IApplicationRepository applicationRepository,
                                  IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetSoughtQuotesResponse> Handle(GetSoughtQuotesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var quotesSought = await _progressReportingRepository.GetProgressReportQuotesSought();

        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new GetSoughtQuotesResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            QuotesSought = quotesSought,
            Version = version
        };
    }
}

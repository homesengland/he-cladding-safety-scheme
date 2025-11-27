using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.GetReportDetails;

public class GetReportDetailsHandler: IRequestHandler<GetReportDetailsRequest, GetReportDetailsResponse>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
        
    public GetReportDetailsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetReportDetailsResponse> Handle(GetReportDetailsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationScheme = _applicationDataProvider.GetApplicationScheme();

        var buildingDetails = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<FRAEWBuildingDetails>("GetFRAEWBuildingDetails",
                                                                                                           new { applicationId });

        var reportDetails = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<FRAEWReportDetails>("GetFireRiskAssessmentReportDetails",
                                                                                                           new { applicationId });

        var response = new GetReportDetailsResponse
        {
            ApplicationScheme = applicationScheme,
            AuthorsName = reportDetails?.AuthorsName,
            PeerReviewPerson = reportDetails?.PeerReviewPerson,
            FraewCost = reportDetails?.FraewCost,
            NumberOfStoreys = reportDetails?.NumberOfStoreys,
            BuildingHeight = reportDetails?.BuildingHeight,
            BasicComplexId = reportDetails?.BasicComplexId,
            BuildingAddress = buildingDetails?.BuildingAddress,
            FRAEWInstructedDate = buildingDetails?.FRAEWInstructedDate,
            BuildingName = buildingDetails?.BuildingName,
            FRAEWCompletedDate = buildingDetails?.FRAEWCompletedDate,
            CompanyUndertakingReport = buildingDetails?.CompanyUndertakingReport,
            ApplicationReferenceNumber = buildingDetails.ApplicationReferenceNumber,
            PartOfDevelopment = buildingDetails.PartOfDevelopment,
            Development = buildingDetails.Development
        };

        return response;
    }
}

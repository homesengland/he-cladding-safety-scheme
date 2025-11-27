using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;

public class GetUploadBuildingControl : IRequestHandler<GetUploadBuildingControlRequest, GetUploadBuildingControlResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IProgressReportingBuildingControlRepository _buildingControlRepository;

    public GetUploadBuildingControl(
        IApplicationDataProvider applicationDataProvider,
        IApplicationDetailsProvider applicationDetailsProvider,
        IProgressReportingBuildingControlRepository buildingControlRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationDetailsProvider = applicationDetailsProvider;
        _buildingControlRepository = buildingControlRepository;
    }

    public async Task<GetUploadBuildingControlResponse> Handle(GetUploadBuildingControlRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var parameters = new GetUploadBuildingControlDocumentsParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };
        var upload = await _buildingControlRepository.GetUploadBuildingControlDocuments(parameters);

        return new GetUploadBuildingControlResponse
        {
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            BuildingName = applicationDetails.BuildingName,
            AddedFiles = upload?.BuildingControlDocuments ?? Enumerable.Empty<FileResult>(),
            BuildingControlDecision = upload?.BuildingControlDecision
        };
    }
}

public class GetUploadBuildingControlRequest : IRequest<GetUploadBuildingControlResponse>
{
    private GetUploadBuildingControlRequest()
    {
    }

    public static readonly GetUploadBuildingControlRequest Request = new();
}

public class GetUploadBuildingControlResponse
{
    public IEnumerable<FileResult> AddedFiles { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public EBuildingControlDecisionType? BuildingControlDecision { get; set; }
}
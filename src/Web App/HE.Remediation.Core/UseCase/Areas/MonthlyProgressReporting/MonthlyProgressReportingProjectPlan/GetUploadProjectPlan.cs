using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class GetUploadProjectPlanHandler : IRequestHandler<GetUploadProjectPlanRequest, GetUploadProjectPlanResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;

    public GetUploadProjectPlanHandler(
        IApplicationDataProvider applicationDataProvider,
        IApplicationDetailsProvider applicationDetailsProvider,
        IProgressReportingProjectPlanRepository projectPlanRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationDetailsProvider = applicationDetailsProvider;
        _projectPlanRepository = projectPlanRepository;
    }

    public async ValueTask<GetUploadProjectPlanResponse> Handle(GetUploadProjectPlanRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationScheme = _applicationDataProvider.GetApplicationScheme();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var parameters = new GetMonthlyReportProjectPlanDocumentsParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };
        var upload = await _projectPlanRepository.GetMonthlyReportProjectPlanDocuments(parameters);

        return new GetUploadProjectPlanResponse
        {
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            ApplicationScheme = applicationScheme,
            BuildingName = applicationDetails.BuildingName,
            AddedFiles = upload.ProjectPlanDocuments,
            PreviousUploadDate = upload.PreviousUploadDate,
            HasEnoughFunds = upload.HasEnoughFunds
        };
    }
}

public class GetUploadProjectPlanRequest : IRequest<GetUploadProjectPlanResponse>
{
    private GetUploadProjectPlanRequest()
    {
    }

    public static readonly GetUploadProjectPlanRequest Request = new();
}

public class GetUploadProjectPlanResponse
{
    public IEnumerable<FileResult> AddedFiles { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
    public string BuildingName { get; set; }
    public DateTime? PreviousUploadDate { get; set; }
    public bool? HasEnoughFunds { get; set; }
}
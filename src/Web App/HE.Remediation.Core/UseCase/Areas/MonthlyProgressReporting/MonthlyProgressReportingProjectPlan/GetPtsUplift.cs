using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class GetPtsUpliftHandler : IRequestHandler<GetPtsUpliftRequest, GetPtsUpliftResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;

    public GetPtsUpliftHandler(
        IApplicationDetailsProvider applicationDetailsProvider, 
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingProjectPlanRepository projectPlanRepository)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _projectPlanRepository = projectPlanRepository;
    }

    public async ValueTask<GetPtsUpliftResponse> Handle(GetPtsUpliftRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _applicationDetailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var files = await _projectPlanRepository.GetPtsUpliftDocument(new GetPtsUpliftDocumentParameters
        {
            ProgressReportId = progressReportId,
            ApplicationId = details.ApplicationId
        });

        return new GetPtsUpliftResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            AddedFiles = files
        };
    }
}

public class GetPtsUpliftRequest : IRequest<GetPtsUpliftResponse>
{
    private GetPtsUpliftRequest()
    {
    }

    public static readonly GetPtsUpliftRequest Request = new();
}

public class GetPtsUpliftResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public IEnumerable<FileResult> AddedFiles { get; set; }
}
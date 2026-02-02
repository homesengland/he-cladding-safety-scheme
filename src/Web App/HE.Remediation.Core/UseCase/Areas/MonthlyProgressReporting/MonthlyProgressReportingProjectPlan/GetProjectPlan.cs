using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class GetProjectPlanHandler : IRequestHandler<GetProjectPlanRequest, GetProjectPlanResponse>
{
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetProjectPlanHandler(
        IProgressReportingProjectPlanRepository projectPlanRepository,
        IApplicationDetailsProvider applicationDetailsProvider, 
        IApplicationDataProvider applicationDataProvider)
    {
        _projectPlanRepository = projectPlanRepository;
        _applicationDetailsProvider = applicationDetailsProvider;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetProjectPlanResponse> Handle(GetProjectPlanRequest request, CancellationToken cancellationToken)
    {
        var applicationScheme = _applicationDataProvider.GetApplicationScheme();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var projectPlan = await _projectPlanRepository.GetProjectPlanDetails(applicationDetails.ApplicationId, progressReportId);

        return new GetProjectPlanResponse
        {
            ApplicationScheme = applicationScheme,
            BuildingName = applicationDetails.BuildingName,
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            AmountPaidForPTS = projectPlan?.AmountPaidForPTS,
            RemainingAmount = projectPlan?.RemainingAmount,
            EnoughFunds = projectPlan?.EnoughFunds,
            IntentToProceedType = projectPlan?.IntentToProceedType,
            InternalAdditionalWork = projectPlan?.InternalAdditionalWork
        };
    }
}

public class GetProjectPlanRequest : IRequest<GetProjectPlanResponse>
{
    private GetProjectPlanRequest()
    {
    }

    public static readonly GetProjectPlanRequest Request = new();
}

public class GetProjectPlanResponse
{
    public EApplicationScheme ApplicationScheme { get; set; }
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public EIntentToProceedType? IntentToProceedType { get; set; }
    public decimal? AmountPaidForPTS { get; set; }
    public decimal? RemainingAmount { get; set; }
    public bool? EnoughFunds { get; set; }
    public bool? InternalAdditionalWork { get; set; }
}
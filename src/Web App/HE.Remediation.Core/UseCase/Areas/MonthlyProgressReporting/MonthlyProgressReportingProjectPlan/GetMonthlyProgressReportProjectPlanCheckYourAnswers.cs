using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class GetMonthlyProgressReportProjectPlanCheckYourAnswersHandler : IRequestHandler<GetMonthlyProgressReportProjectPlanCheckYourAnswersRequest, GetMonthlyProgressReportProjectPlanCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;

    public GetMonthlyProgressReportProjectPlanCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IApplicationDetailsProvider applicationDetailsProvider, IProgressReportingProjectPlanRepository projectPlanRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationDetailsProvider = applicationDetailsProvider;
        _projectPlanRepository = projectPlanRepository;
    }

    public async Task<GetMonthlyProgressReportProjectPlanCheckYourAnswersResponse> Handle(GetMonthlyProgressReportProjectPlanCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();

        var checkYourAnswers = await _projectPlanRepository.GetProjectPlanCheckYourAnswers(
            new GetProjectPlanCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return new GetMonthlyProgressReportProjectPlanCheckYourAnswersResponse
        {
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            BuildingName = applicationDetails.BuildingName,
            IntentToProceedType = checkYourAnswers.IntentToProceedTypeId,
            RemainingAmount = checkYourAnswers.RemainingAmount,
            EnoughFunds = checkYourAnswers.EnoughFunds,
            InternalAdditionalWork = checkYourAnswers.InternalAdditionalWork,
            ProjectPlanDocument = checkYourAnswers.ProjectPlanDocument,
            PtsUpliftDocument = checkYourAnswers.PtsUpliftDocument
        };
    }
}

public class GetMonthlyProgressReportProjectPlanCheckYourAnswersRequest : IRequest<GetMonthlyProgressReportProjectPlanCheckYourAnswersResponse>
{
    private GetMonthlyProgressReportProjectPlanCheckYourAnswersRequest()
    {
    }

    public static readonly GetMonthlyProgressReportProjectPlanCheckYourAnswersRequest Request = new();
}

public class GetMonthlyProgressReportProjectPlanCheckYourAnswersResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public EIntentToProceedType? IntentToProceedType { get; set; }
    public decimal? RemainingAmount { get; set; }
    public bool? EnoughFunds { get; set; }
    public bool? InternalAdditionalWork { get; set; }
    public string ProjectPlanDocument { get; set; }
    public string PtsUpliftDocument { get; set; }
}
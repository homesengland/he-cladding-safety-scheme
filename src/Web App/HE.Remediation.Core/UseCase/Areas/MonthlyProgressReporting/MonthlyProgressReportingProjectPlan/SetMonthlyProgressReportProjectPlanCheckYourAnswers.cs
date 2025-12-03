using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Interface;
using MediatR;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class SetMonthlyProgressReportProjectPlanCheckYourAnswersHandler : IRequestHandler<SetMonthlyProgressReportProjectPlanCheckYourAnswersRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;

    public SetMonthlyProgressReportProjectPlanCheckYourAnswersHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectPlanRepository projectPlanRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _projectPlanRepository = projectPlanRepository;
    }
    public async Task<Unit> Handle(SetMonthlyProgressReportProjectPlanCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var parameters = new SetMonthlyProgressReportProjectPlanCheckYourAnswersParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            TaskStatusId = (int)ETaskStatus.Completed
        };

        await _projectPlanRepository.SetMonthlyProgressReportProjectPlanCheckYourAnswers(parameters);
        return Unit.Value;
    }
}

public class SetMonthlyProgressReportProjectPlanCheckYourAnswersRequest : IRequest
{
    private SetMonthlyProgressReportProjectPlanCheckYourAnswersRequest()
    {
    }

    public static readonly SetMonthlyProgressReportProjectPlanCheckYourAnswersRequest Request = new();
}
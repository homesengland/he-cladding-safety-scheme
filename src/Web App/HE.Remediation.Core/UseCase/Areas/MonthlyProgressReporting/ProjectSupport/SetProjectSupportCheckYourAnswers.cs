using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;

public class SetProjectSupportCheckYourAnswers : IRequestHandler<SetProjectSupportCheckYourAnswersRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectSupportRepository _projectSupportRepository;

    public SetProjectSupportCheckYourAnswers(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectSupportRepository projectSupportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _projectSupportRepository = projectSupportRepository;
    }
    public async Task<Unit> Handle(SetProjectSupportCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var parameters = new SetProjectSupportCheckYourAnswersParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            TaskStatusId = (int)ETaskStatus.Completed
        };

        await _projectSupportRepository.SetProjectSupportCheckYourAnswers(parameters);
        return Unit.Value;
    }
}

public class SetProjectSupportCheckYourAnswersRequest : IRequest
{
    private SetProjectSupportCheckYourAnswersRequest()
    {
    }

    public static readonly SetProjectSupportCheckYourAnswersRequest Request = new();
}

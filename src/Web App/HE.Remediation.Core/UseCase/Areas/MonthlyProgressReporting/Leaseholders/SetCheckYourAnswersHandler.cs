using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest>
{
    private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _dataProvider;

    public SetCheckYourAnswersHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider dataProvider,
        IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository
    )
    {
        _detailsProvider = detailsProvider;
        _dataProvider = dataProvider;
        _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
    }

    public async Task<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _dataProvider.GetProgressReportId();
        var parameters = new SetCheckYourAnswersParameters()
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = progressReportId,
        };
        await _progressReportingLeaseholdersRepository.SetProgressReportLeaseholderCheckYourAnswers(parameters);
        return Unit.Value;
    }
}

public class SetCheckYourAnswersRequest() : IRequest {}

using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

public class SetLastCommunicationDateHandler : IRequestHandler<SetLastCommunicationDateRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;

    public SetLastCommunicationDateHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
    }

    public async Task<Unit> Handle(SetLastCommunicationDateRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        await _progressReportingLeaseholdersRepository.SetProgressReportLeaseholderDate(
            new SetProgressReportLeaseholderDateParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                LastCommunicationDate = request.LastCommunicationDate
            });

        return Unit.Value;
    }
}

public class SetLastCommunicationDateRequest : IRequest
{
    public DateTime LastCommunicationDate { get; set; }
}

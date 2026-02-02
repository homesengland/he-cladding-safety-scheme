using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using HE.Remediation.Core.Services.FileService;
using Mediator;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

public class SetHaveYouContactedHandler : DeleteUploadEvidenceService, IRequestHandler<SetHaveYouContactedRequest>
{
    private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;

    public SetHaveYouContactedHandler(
        IApplicationDetailsProvider detailsProvider, 
        IApplicationDataProvider dataProvider,
        IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository,
        IFileRepository fileRepository,
        IFileService fileService,
        ILogger<SetHaveYouContactedHandler> logger, 
        IMonthlyProgressReportingRepository monthlyProgressReportingRepository) 
            : base(fileRepository, fileService, progressReportingLeaseholdersRepository, logger)
    {
        _detailsProvider = detailsProvider;
        _dataProvider = dataProvider;
        _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
        _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetHaveYouContactedRequest request, CancellationToken cancellationToken)
    {
        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _dataProvider.GetProgressReportId();
        var parameters = new SetProgressReportLeaseholderCommunicationParameters()
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = progressReportId,
            HasContacted = request.HasContacted == ENoYes.Yes
        };

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _progressReportingLeaseholdersRepository.SetProgressReportLeaseholderCommunication(parameters);
        await _monthlyProgressReportingRepository.SetMonthlyReportStatus(new SetMonthlyReportStatusParameters
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = progressReportId,
            TaskStatusId = (int)ETaskStatus.InProgress
        });

        if(request.HasContacted == ENoYes.No)
        {
            // remove evidence files

            var files = await _progressReportingLeaseholdersRepository.GetProgressReportLeaseholderCommunicationFiles(
                    new GetProgressReportLeaseholderCommunicationParameters() {
                        ApplicationId = details.ApplicationId,
                        ProgressReportId = _dataProvider.GetProgressReportId()
                    }
            );
            foreach (var file in files)
            {
                await DeleteFile(details.ApplicationId, progressReportId, file.Id);
            }
        }

        scope.Complete();

        return Unit.Value;
    }
}


public class SetHaveYouContactedRequest() : IRequest 
{
    public ENoYes? HasContacted { get; set; }
}



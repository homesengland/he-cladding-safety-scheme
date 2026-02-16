
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Services.FileService;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ChangeAnswers;

public class ChangeYourAnswersHandler : IRequestHandler<ChangeYourAnswersRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IFileService _fileService;

    public ChangeYourAnswersHandler(IProgressReportingRepository progressReportingRepository,
                                    IFileService fileService)
    {
        _progressReportingRepository = progressReportingRepository;
        _fileService = fileService;
    }

    public async ValueTask<Unit> Handle(ChangeYourAnswersRequest request, CancellationToken cancellationToken)
    {
        await ResetProgressReport(request);
        return Unit.Value;
    }

    private async Task ResetProgressReport(ChangeYourAnswersRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await DeleteEvidenceFiles();
        await _progressReportingRepository.ResetProgressReport();

        scope.Complete();
    }

    private async Task DeleteEvidenceFiles()
    {
        var files = await _progressReportingRepository.GetProgressReportLeaseholdersInformedFiles();
        foreach(var file in files)
        {
            await _fileService.DeleteFile($"{file.Id}{file.Extension}");
        }
    }
}

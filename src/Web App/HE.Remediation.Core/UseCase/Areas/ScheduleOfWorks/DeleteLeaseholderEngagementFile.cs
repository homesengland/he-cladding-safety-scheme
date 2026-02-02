using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;

public class DeleteLeaseholderEngagementFileHandler : IRequestHandler<DeleteLeaseholderEngagementFileRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;

    public DeleteLeaseholderEngagementFileHandler(
        IApplicationDataProvider applicationDataProvider, 
        IScheduleOfWorksRepository scheduleOfWorksRepository, 
        IFileRepository fileRepository, 
        IFileService fileService)
    {
        _applicationDataProvider = applicationDataProvider;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
        _fileRepository = fileRepository;
        _fileService = fileService;
    }

    public async ValueTask<Unit> Handle(DeleteLeaseholderEngagementFileRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _scheduleOfWorksRepository.DeleteLeaseholderEngagement(new DeleteLeaseholderEngagementParameters
        {
            ApplicationId = applicationId,
            FileId = request.FileId
        });

        var result = await _fileRepository.DeleteFile(request.FileId);

        scope.Complete();

        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

        return Unit.Value;
    }
}

public class DeleteLeaseholderEngagementFileRequest : IRequest
{
    public Guid FileId { get; set; }
}
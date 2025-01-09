using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Delete;

public class DeleteWorksContractHandler : IRequestHandler<DeleteWorksContractRequest>
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public DeleteWorksContractHandler(IFileService fileService, 
                                      IFileRepository fileRepository, 
                                      IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _fileService = fileService;
        _fileRepository = fileRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<Unit> Handle(DeleteWorksContractRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _scheduleOfWorksRepository.DeleteContract(request.FileId);
        var result = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");
        scope.Complete();

        return Unit.Value;
    }
}

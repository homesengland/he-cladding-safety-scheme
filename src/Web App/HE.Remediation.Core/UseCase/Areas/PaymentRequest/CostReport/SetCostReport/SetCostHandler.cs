using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using Mediator;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.SetCostReport;

public class SetCostHandler : IRequestHandler<SetCostRequest>
{    
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly FileServiceSettings _fileServiceSettings;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetCostHandler(IApplicationDataProvider applicationDataProvider,
                          IFileRepository fileRepository,
                          IFileService fileService,
                          IOptions<FileServiceSettings> fileServiceSettings,
                          IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileRepository = fileRepository;
        _fileService = fileService;
        _fileServiceSettings = fileServiceSettings.Value;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<Unit> Handle(SetCostRequest request, CancellationToken cancellationToken)
    {
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();
        
        if (request.File is not null)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var fileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.PaymentRequestEvidence);

            await _fileRepository.InsertFile(new InsertFileParameters
            {
                Id = fileResult.FileId,
                Extension = Path.GetExtension(request.File.FileName),
                MimeType = fileResult.MimeType,
                Name = request.File.FileName,
                Size = request.File.Length
            });

            await _paymentRequestRepository.InsertPaymentRequestCostFile(fileResult.FileId, paymentRequestId);
                                
            scope.Complete();
        }

        return Unit.Value;
    }    
}

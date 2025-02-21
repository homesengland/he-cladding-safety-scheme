using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest;

public class SetPaymentRequestInvoiceHandler : IRequestHandler<SetPaymentRequestInvoiceRequest>
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly FileServiceSettings _fileServiceSettings;

    public SetPaymentRequestInvoiceHandler(IFileService fileService, IFileRepository fileRepository, IApplicationDataProvider applicationDataProvider, IPaymentRequestRepository paymentRequestRepository, IOptions<FileServiceSettings> fileServiceSettings)
    {
        _fileService = fileService;
        _fileRepository = fileRepository;
        _applicationDataProvider = applicationDataProvider;
        _paymentRequestRepository = paymentRequestRepository;
        _fileServiceSettings = fileServiceSettings.Value;
    }

    public async Task<Unit> Handle(SetPaymentRequestInvoiceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var fileResponse = await _fileService.ProcessFile(request.File, _fileServiceSettings.PaymentRequestInvoice);

        await _fileRepository.InsertFile(new InsertFileParameters
        {
            Id = fileResponse.FileId,
            MimeType = fileResponse.MimeType,
            Name = request.File.FileName,
            Extension = Path.GetExtension(request.File.FileName),
            Size = request.File.Length
        });

        await _paymentRequestRepository.InsertPaymentRequestInvoiceFile(new InsertPaymentRequestInvoiceFileParameters
        {
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId,
            FileId = fileResponse.FileId
        });

        scope.Complete();

        return Unit.Value;
    }
}

public class SetPaymentRequestInvoiceRequest : IRequest
{
    public IFormFile File { get; set; }
}
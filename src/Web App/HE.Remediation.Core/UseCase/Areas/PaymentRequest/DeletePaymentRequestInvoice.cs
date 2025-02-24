using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest;

public class DeletePaymentRequestInvoiceHandler : IRequestHandler<DeletePaymentRequestInvoiceRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;

    public DeletePaymentRequestInvoiceHandler(
        IApplicationDataProvider applicationDataProvider, 
        IPaymentRequestRepository paymentRequestRepository, 
        IFileRepository fileRepository, 
        IFileService fileService)
    {
        _applicationDataProvider = applicationDataProvider;
        _paymentRequestRepository = paymentRequestRepository;
        _fileRepository = fileRepository;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeletePaymentRequestInvoiceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _paymentRequestRepository.DeletePaymentRequestInvoiceFile(new DeletePaymentRequestInvoiceFileParameters
        {
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId,
            FileId = request.FileId
        });

        var deleteResult = await _fileRepository.DeleteFile(request.FileId);
        await _fileService.DeleteFile($"{request.FileId}{deleteResult.Extension}");

        scope.Complete();

        return Unit.Value;
    }
}

public class DeletePaymentRequestInvoiceRequest : IRequest
{
    public Guid FileId { get; set; }
    public string ReturnUrl { get; set; }
}
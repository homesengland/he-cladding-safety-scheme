﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.DeleteCostReport;

public class DeleteCostHandler : IRequestHandler<DeleteCostRequest>
{    
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;    
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public DeleteCostHandler(IApplicationDataProvider applicationDataProvider,
                             IFileService fileService,
                             IFileRepository fileRepository,
                             IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<Unit> Handle(DeleteCostRequest request, CancellationToken cancellationToken)
    {
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _paymentRequestRepository.DeletePaymentRequestCostFile(request.FileId, paymentRequestId);
        
        var result = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

        scope.Complete();

        return Unit.Value;
    }
}

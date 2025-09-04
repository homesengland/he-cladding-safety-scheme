using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetLeaseholderResidentUploadEvidence;

public class SetLeaseholderResidentUploadEvidenceHandler : IRequestHandler<SetLeaseholderResidentUploadEvidenceRequest>
{    
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly FileServiceSettings _fileServiceSettings;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetLeaseholderResidentUploadEvidenceHandler(IApplicationDataProvider applicationDataProvider,
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

    public async Task<Unit> Handle(SetLeaseholderResidentUploadEvidenceRequest request, CancellationToken cancellationToken)
    {
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();
        
        if (request.File is not null)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var fileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.PaymentRequestLeaseholderResidentUploadEvidence);

            await _fileRepository.InsertFile(new InsertFileParameters
            {
                Id = fileResult.FileId,
                Extension = Path.GetExtension(request.File.FileName),
                MimeType = fileResult.MimeType,
                Name = request.File.FileName,
                Size = request.File.Length
            });

            await _paymentRequestRepository.InsertLeaseholderResidentUploadEvidenceFile(fileResult.FileId, paymentRequestId);
                                
            scope.Complete();
        }

        if (request.LastCommunicationDateMonth is not null && request.LastCommunicationDateYear is not null)
        {
            var lastCommunicationDate = GetDate(request.LastCommunicationDateMonth, request.LastCommunicationDateYear);
            await _paymentRequestRepository.UpdatePaymentRequestLeaseholderResidentLastCommunicationDate(paymentRequestId, lastCommunicationDate);
        }

        return Unit.Value;
    }

    private DateTime? GetDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1)
            : null;
    }
}

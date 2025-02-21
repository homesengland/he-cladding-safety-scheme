using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class InvoicesViewModelMapper : Profile
{
    public InvoicesViewModelMapper()
    {
        CreateMap<GetPaymentRequestInvoicesResponse, InvoicesViewModel>();
        CreateMap<GetPaymentRequestInvoiceFilesResult, File>();
        CreateMap<InvoicesViewModel, SetPaymentRequestInvoiceRequest>();
    }
}
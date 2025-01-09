using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetConfirmation;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetConfirmation;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubmitPayment;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class ConfirmationViewModelMapper : Profile
{
    public ConfirmationViewModelMapper()
    {
        CreateMap<GetConfirmationResponse, ConfirmationViewModel>();
        CreateMap<ConfirmationViewModel, SetConfirmationRequest>();
        CreateMap<CostsViewModel, SetSubmitPaymentRequest>();
    }
}

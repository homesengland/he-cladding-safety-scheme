using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetConfirmation;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetNeedVariations;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetConfirmation;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetNeedVariations;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubmitPayment;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class NeedVariationsViewModelMapper : Profile
{
    public NeedVariationsViewModelMapper()
    {
        CreateMap<GetNeedVariationsReponse, NeedVariationsViewModel>();
        CreateMap<NeedVariationsViewModel, SetNeedVariationsRequest>();
    }
}

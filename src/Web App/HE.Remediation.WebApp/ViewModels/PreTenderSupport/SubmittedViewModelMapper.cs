using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.Submit.GetSubmit;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class SubmittedViewModelMapper : Profile
{
    public SubmittedViewModelMapper()
    {
        CreateMap<GetSubmitResponse, SubmittedViewModel>();
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ConfirmedNotViableViewModelMapper : Profile
{
    public ConfirmedNotViableViewModelMapper()
    {
        CreateMap<GetConfirmedNotViableResponse, ConfirmedNotViableViewModel>();
        CreateMap<ConfirmedNotViableViewModel, SetConfirmedNotViableRequest>();
    }
}
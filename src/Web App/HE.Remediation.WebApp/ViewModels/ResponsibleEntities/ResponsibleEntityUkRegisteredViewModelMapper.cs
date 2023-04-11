using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.GetResponsibleEntityUkRegistered;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.SetResponsibleEntityUkRegistered;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityUkRegisteredViewModelMapper : Profile
{
    public ResponsibleEntityUkRegisteredViewModelMapper()
    {
        CreateMap<GetResponsibleEntityUkRegisteredResponse, ResponsibleEntityUkRegisteredViewModel>();
        CreateMap<ResponsibleEntityUkRegisteredViewModel, SetResponsibleEntityUkRegisteredRequest>();
    }
}
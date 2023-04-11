using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.Profile.SetUserResponsibleEntityType;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class UserResponsibleEntityTypeViewModelMapper : Profile
{
    public UserResponsibleEntityTypeViewModelMapper()
    {
        CreateMap<UserResponsibleEntityTypeViewModel, SetUserResponsibleEntityTypeRequest>();
    }
}
using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.GetResponsibleEntityCompanyType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.SetResponsibleEntityCompanyType;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityCompanyTypeViewModelMapper : Profile
{
    public ResponsibleEntityCompanyTypeViewModelMapper()
    {
        CreateMap<GetResponsibleEntityCompanyTypeResponse, ResponsibleEntityCompanyTypeViewModel>();
        CreateMap<ResponsibleEntityCompanyTypeViewModel, SetResponsibleEntityCompanyTypeRequest>();
    }
}
using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.GetResponsibleEntityCompanySubType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.SetResponsibleEntityCompanySubType;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityCompanySubTypeViewModelMapper : Profile
{
    public ResponsibleEntityCompanySubTypeViewModelMapper()
    {                        
        CreateMap<GetResponsibleEntityCompanySubTypeResponse, ResponsibleEntityCompanySubTypeViewModel>();
        CreateMap<ResponsibleEntityCompanySubTypeViewModel, SetResponsibleEntityCompanySubTypeRequest>();
    }
}

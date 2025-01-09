using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.GetResponsibleEntityResponsibleForGrantFunding;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.SetResponsibleEntityResponsibleForGrantFunding;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityResponsibleForGrantFundingViewModelMapper : Profile
{
    public ResponsibleEntityResponsibleForGrantFundingViewModelMapper()
    {
        CreateMap<GetResponsibleEntityResponsibleForGrantFundingResponse, ResponsibleEntityResponsibleForGrantFundingViewModel>();
        CreateMap<ResponsibleEntityResponsibleForGrantFundingViewModel, SetResponsibleEntityResponsibleForGrantFundingRequest>();
    }
}
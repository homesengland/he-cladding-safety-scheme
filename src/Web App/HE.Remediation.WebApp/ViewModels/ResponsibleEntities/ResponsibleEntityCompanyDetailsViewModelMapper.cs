using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyDetailsViewModelMapper : Profile
    {
        public ResponsibleEntityCompanyDetailsViewModelMapper()
        {
            CreateMap<GetResponsibleEntityCompanyDetailsResponse, ResponsibleEntityCompanyDetailsViewModel>();
            CreateMap<ResponsibleEntityCompanyDetailsViewModel, SetResponsibleEntityCompanyDetailsRequest>();
        }
    }
}

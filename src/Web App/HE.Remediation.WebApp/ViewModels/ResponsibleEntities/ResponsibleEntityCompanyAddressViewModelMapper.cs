using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyAddressViewModelMapper : Profile
    {
        public ResponsibleEntityCompanyAddressViewModelMapper()
        {
            CreateMap<GetResponsibleEntityCompanyAddressResponse, ResponsibleEntityCompanyAddressViewModel>();
            CreateMap<ResponsibleEntityCompanyAddressViewModel, SetResponsibleEntityCompanyAddressRequest>();
        }
    }
}

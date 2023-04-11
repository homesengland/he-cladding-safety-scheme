using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class FreeholderAddressViewModelMapper : Profile
    {
        public FreeholderAddressViewModelMapper()
        {
            CreateMap<GetFreeholderAddressResponse, FreeholderAddressViewModel>();
            CreateMap<FreeholderAddressViewModel, SetFreeholderAddressRequest>();
        }
    }
}

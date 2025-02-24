using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using HE.Remediation.WebApp.ViewModels.Location;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class FreeholderAddressViewModelMapper : Profile
    {
        public FreeholderAddressViewModelMapper()
        {
            CreateMap<GetFreeholderAddressResponse, FreeholderAddressViewModel>();
            CreateMap<FreeholderAddressViewModel, SetFreeholderAddressRequest>();
            CreateMap<FreeholderAddressViewModel, PostCodeEntryViewModel>();
        }
    }
}

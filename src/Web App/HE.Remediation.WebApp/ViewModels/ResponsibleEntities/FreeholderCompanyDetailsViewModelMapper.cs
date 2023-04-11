using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class FreeholderCompanyDetailsViewModelMapper : Profile
    {
        public FreeholderCompanyDetailsViewModelMapper()
        {
            CreateMap<GetFreeholderCompanyDetailsResponse, FreeholderCompanyDetailsViewModel>();
            CreateMap<FreeholderCompanyDetailsViewModel, SetFreeholderCompanyDetailsRequest>();
        }
    }
}

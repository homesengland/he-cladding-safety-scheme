using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class FreeholderIndividualDetailsViewModelMapper : Profile
    {
        public FreeholderIndividualDetailsViewModelMapper()
        {
            CreateMap<GetFreeholderIndividualDetailsResponse, FreeholderIndividualDetailsViewModel>();
            CreateMap<FreeholderIndividualDetailsViewModel, SetFreeholderIndividualDetailsRequest>();
        }
    }
}

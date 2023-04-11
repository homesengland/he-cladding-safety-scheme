using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.GetFreeholderCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.SetFreeholderCompanyOrIndividual;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class FreeholderCompanyOrIndividualViewModelMapper : Profile
    {
        public FreeholderCompanyOrIndividualViewModelMapper()
        {
            CreateMap<GetFreeholderCompanyOrIndividualResponse, FreeholderCompanyOrIndividualViewModel>();
            CreateMap<FreeholderCompanyOrIndividualViewModel, SetFreeholderCompanyOrIndividualRequest>();
        }
    }
}

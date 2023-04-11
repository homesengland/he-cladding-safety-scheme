using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class CheckYourAnswersViewModelMapper : Profile
    {
        public CheckYourAnswersViewModelMapper()
        {
            CreateMap<GetResponsibleEntityAnswersResponse, CheckYourAnswersViewModel>();
        }
    }
}

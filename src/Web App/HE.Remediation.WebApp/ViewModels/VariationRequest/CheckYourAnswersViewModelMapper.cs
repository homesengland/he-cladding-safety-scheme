using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.CheckYourAnswers.Get;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class CheckYourAnswersViewModelMapper : Profile
    {
        public CheckYourAnswersViewModelMapper()
        {
            CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
        }
    }
}

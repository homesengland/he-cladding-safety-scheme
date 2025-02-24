using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.CheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper ()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BankAccount.CheckYourAnswers.GetCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.BankAccount
{
    public class CheckYourAnswersViewModelMapper : Profile
    {
        public CheckYourAnswersViewModelMapper()
        {
            CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
        }
    }
}

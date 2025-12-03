using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}

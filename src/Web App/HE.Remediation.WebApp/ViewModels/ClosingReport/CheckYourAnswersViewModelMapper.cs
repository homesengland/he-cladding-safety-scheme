using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetSubContractorCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}
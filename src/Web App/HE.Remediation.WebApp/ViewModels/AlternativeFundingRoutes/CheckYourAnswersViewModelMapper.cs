using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.GetCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>()
            .ForMember(dest => dest.FundingStillPursuingAnswer, opt => 
                opt.MapFrom(x => 
                    x.FundingStillPursuingAnswer.Split(",  ", StringSplitOptions.TrimEntries))
                );
    }
}
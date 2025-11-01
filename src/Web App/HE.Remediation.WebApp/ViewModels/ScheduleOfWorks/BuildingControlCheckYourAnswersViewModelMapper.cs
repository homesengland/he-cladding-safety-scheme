using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingControlCheckYourAnswers.Get;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class BuildingControlCheckYourAnswersViewModelMapper : Profile
{
    public BuildingControlCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, BuildingControlCheckYourAnswersViewModel>();
    }
}
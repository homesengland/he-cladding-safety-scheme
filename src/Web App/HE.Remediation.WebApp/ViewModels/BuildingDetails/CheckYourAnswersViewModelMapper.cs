using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.GetBuildingDetailsAnswers;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class CheckYourAnswersViewModelMapper : Profile
    {
        public CheckYourAnswersViewModelMapper()
        {
            CreateMap<GetBuildingDetailsAnswersResponse, CheckYourAnswersViewModel>();
        }
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.HaveAnyAnswersChanged;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.GetHasProjectPlanMilestones;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.SetProjectPlanMilestones;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.SetWhenStartOnSite;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class HasProjectPlanMilestonesViewModelMapper : Profile
    {
        public HasProjectPlanMilestonesViewModelMapper()
        {
            CreateMap<GetHasProjectPlanMilestonesResponse, HasProjectPlanMilestonesViewModel>();
            CreateMap<HasProjectPlanMilestonesViewModel, SetHasProjectPlanMilestonesRequest>();
        }
    }
}
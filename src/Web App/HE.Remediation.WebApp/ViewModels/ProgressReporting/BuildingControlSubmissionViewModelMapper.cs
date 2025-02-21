using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlSubmissionViewModelMapper : Profile
{
    public BuildingControlSubmissionViewModelMapper()
    {
        CreateMap<GetBuildingControlSubmissionResponse, BuildingControlSubmissionViewModel>();
        CreateMap<BuildingControlSubmissionViewModel, SetBuildingControlSubmissionRequest>();
    }
}
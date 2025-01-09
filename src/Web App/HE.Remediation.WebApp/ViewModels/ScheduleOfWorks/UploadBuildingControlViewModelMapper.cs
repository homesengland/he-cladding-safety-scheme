using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class UploadBuildingControlViewModelMapper : Profile
{
    public UploadBuildingControlViewModelMapper()
    {
        CreateMap<GetBuildingControlResponse, UploadBuildingControlViewModel>();
        CreateMap<UploadBuildingControlViewModel, AddBuildingControlFileRequest>();
    }
}
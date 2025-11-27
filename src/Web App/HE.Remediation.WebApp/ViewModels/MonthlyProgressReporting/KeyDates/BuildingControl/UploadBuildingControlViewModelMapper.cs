using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates.BuildingControl;

public class UploadBuildingControlViewModelMapper : Profile
{
    public UploadBuildingControlViewModelMapper()
    {
        CreateMap<GetUploadBuildingControlResponse, UploadBuildingControlViewModel>();
        CreateMap<UploadBuildingControlViewModel, SetUploadBuildingControlRequest>();
    }
}
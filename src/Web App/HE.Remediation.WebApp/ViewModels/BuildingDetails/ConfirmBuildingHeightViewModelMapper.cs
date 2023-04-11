using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.GetBuildingHeight;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConfirmBuildingHeightViewModelMapper : Profile
{
    public ConfirmBuildingHeightViewModelMapper()
    {
        CreateMap<ConfirmBuildingHeightViewModel, SetBuildingHeightRequest>();

        CreateMap<GetBuildingHeightResponse, ConfirmBuildingHeightViewModel>();
    }
}
using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.GetDeveloperInBusiness;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.SetDeveloperInBusiness;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class DeveloperInBusinessViewModelMapper : Profile
{
    public DeveloperInBusinessViewModelMapper()
    {
        CreateMap<DeveloperInBusinessViewModel, SetDeveloperInBusinessRequest>();

        CreateMap<GetDeveloperInBusinessResponse, DeveloperInBusinessViewModel>();
    }
}
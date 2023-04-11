using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.GetResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.SetResidentialUnits;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class ResidentialUnitsViewModelMapper : Profile
    {
        public ResidentialUnitsViewModelMapper()
        {
            CreateMap<ResidentialUnitsViewModel, SetResidentialUnitsRequest>();
            CreateMap<GetResidentialUnitsResponse, ResidentialUnitsViewModel>();
        }
    }
}

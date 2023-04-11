using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.GetNonResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.SetNonResidentialUnits;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class NonResidentialUnitsViewModelMapper : Profile
    {
        public NonResidentialUnitsViewModelMapper()
        {
            CreateMap<NonResidentialUnitsViewModel, SetNonResidentialUnitsRequest>();
            CreateMap<GetNonResidentialUnitsResponse, NonResidentialUnitsViewModel>();
        }
    }
}

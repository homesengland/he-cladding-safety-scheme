using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.GetLocalAuthorityCostCentre;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.SetLocalAuthorityCostCentre;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class LocalAuthorityCostCentreViewModelMapper : Profile
    {
        public LocalAuthorityCostCentreViewModelMapper()
        {
            CreateMap<LocalAuthorityCostCentreViewModel, GetLocalAuthorityCostCentreRequest>();
            CreateMap<GetLocalAuthorityCostCentreResponse, LocalAuthorityCostCentreViewModel>();
            CreateMap<LocalAuthorityCostCentreViewModel, SetLocalAuthorityCostCentreRequest>();
            CreateMap<Core.UseCase.Areas.BuildingDetails.LocalAuthority.GetLocalAuthorityCostCentre.LocalAuthorityCostCentre, LocalAuthorityCostCentre>();
        }
    }
}

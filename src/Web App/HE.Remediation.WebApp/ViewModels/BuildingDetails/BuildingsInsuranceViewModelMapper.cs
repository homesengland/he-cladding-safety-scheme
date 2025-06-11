using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.SetBuildingsInsurance;
using HE.Remediation.WebApp.ViewModels.BuildingsInsurance;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingsInsuranceViewModelMapper : Profile
{
    public BuildingsInsuranceViewModelMapper()
    {
        CreateMap<BuildingsInsuranceViewModel, SetBuildingsInsuranceRequest>();
        CreateMap<GetBuildingsInsuranceResponse, BuildingsInsuranceViewModel>()
            .ForMember(x => x.SumInsuredAmountText, opt => opt.MapFrom(s => s.SumInsuredAmount.HasValue
                                 ? s.SumInsuredAmount.Value.ToString("N0")
                                 : null))
            .ForMember(x => x.CurrentBuildingInsurancePremiumAmountText, opt => opt.MapFrom(s => s.CurrentBuildingInsurancePremiumAmount.HasValue
                                 ? s.CurrentBuildingInsurancePremiumAmount.Value.ToString("N0")
                                 : null));

        CreateMap<BuildingsInsuranceViewModel, Core.UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance.SetBuildingsInsuranceRequest>();
        CreateMap<Core.UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance.GetBuildingsInsuranceResponse, BuildingsInsuranceViewModel>()
            .ForMember(x => x.SumInsuredAmountText, opt => opt.MapFrom(s => s.SumInsuredAmount.HasValue
                                 ? s.SumInsuredAmount.Value.ToString("N0")
                                 : null))
            .ForMember(x => x.CurrentBuildingInsurancePremiumAmountText, opt => opt.MapFrom(s => s.CurrentBuildingInsurancePremiumAmount.HasValue
                                 ? s.CurrentBuildingInsurancePremiumAmount.Value.ToString("N0")
                                 : null));
    }
}

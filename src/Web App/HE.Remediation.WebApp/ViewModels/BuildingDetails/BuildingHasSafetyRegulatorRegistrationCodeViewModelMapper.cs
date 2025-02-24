using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingHasSafetyRegulatorRegistrationCodeViewModelMapper : Profile
{
    public BuildingHasSafetyRegulatorRegistrationCodeViewModelMapper()
    {
        CreateMap<BuildingHasSafetyRegulatorRegistrationCodeViewModel, SetBuildingHasSafetyRegulatorRegistrationCodeRequest>();

        CreateMap<GetBuildingHasSafetyRegulatorRegistrationCodeResponse, BuildingHasSafetyRegulatorRegistrationCodeViewModel>();
    }
}
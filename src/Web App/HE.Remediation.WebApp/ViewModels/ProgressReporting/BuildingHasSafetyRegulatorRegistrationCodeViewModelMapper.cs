using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingHasSafetyRegulatorRegistrationCodeViewModelMapper : Profile
{
    public BuildingHasSafetyRegulatorRegistrationCodeViewModelMapper()
    {
        CreateMap<BuildingHasSafetyRegulatorRegistrationCodeViewModel, SetBuildingHasSafetyRegulatorRegistrationCodeRequest>();

        CreateMap<GetBuildingHasSafetyRegulatorRegistrationCodeResponse, BuildingHasSafetyRegulatorRegistrationCodeViewModel>();
    }
}
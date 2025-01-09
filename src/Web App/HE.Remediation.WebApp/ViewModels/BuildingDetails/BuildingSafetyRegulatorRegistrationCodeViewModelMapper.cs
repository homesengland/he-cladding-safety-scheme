using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class BuildingSafetyRegulatorRegistrationCodeViewModelMapper : Profile
    {
        public BuildingSafetyRegulatorRegistrationCodeViewModelMapper()
        {
            CreateMap<BuildingSafetyRegulatorRegistrationCodeViewModel, SetBuildingSafetyRegulatorRegistrationCodeRequest>();
            CreateMap<GetBuildingSafetyRegulatorRegistrationCodeResponse, BuildingSafetyRegulatorRegistrationCodeViewModel>();
        }
    }
}

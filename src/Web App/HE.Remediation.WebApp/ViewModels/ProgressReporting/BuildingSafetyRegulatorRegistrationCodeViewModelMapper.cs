using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
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

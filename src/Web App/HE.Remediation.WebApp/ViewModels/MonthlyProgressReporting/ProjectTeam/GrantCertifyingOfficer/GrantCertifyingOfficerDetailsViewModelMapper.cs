using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GrantCertifyingOfficerDetailsViewModelMapper : Profile
{
    public GrantCertifyingOfficerDetailsViewModelMapper()
    {
        CreateMap<GetGcoDetailsResponse, GrantCertifyingOfficerDetailsViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (ETeamRole)src.RoleId))
            .ForMember(dest => dest.InvolvedInOriginalInstallation, opt => opt.MapFrom(src => src.IsInvolvedInOriginalInstallation))
            .ForMember(dest => dest.IndemnityInsurance, opt => opt.MapFrom(src => src.HasIndemnityInsurance))
            .ForMember(dest => dest.ContractSigned, opt => opt.MapFrom(src => src.IsContractSigned));

        CreateMap<GrantCertifyingOfficerDetailsViewModel, SetGrantCertifyingOfficerDetailsRequest>();
    }
}

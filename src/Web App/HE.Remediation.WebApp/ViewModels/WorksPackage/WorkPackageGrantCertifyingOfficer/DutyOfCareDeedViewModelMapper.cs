using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.DutyOfCareDeed.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class DutyOfCareDeedViewModelMapper : Profile
{
    public DutyOfCareDeedViewModelMapper()
    {
        CreateMap<GetDutyOfCareDeedResponse, DutyOfCareDeedViewModel>();
    }
}

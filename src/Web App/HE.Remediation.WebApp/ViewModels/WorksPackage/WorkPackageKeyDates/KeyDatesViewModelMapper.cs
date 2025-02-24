using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageKeyDates;

public class KeyDatesViewModelMapper : Profile
{
    public KeyDatesViewModelMapper()
    {
        CreateMap<GetKeyDatesResponse, KeyDatesViewModel>();
        CreateMap<KeyDatesViewModel, SetKeyDatesRequest>();
    }
}

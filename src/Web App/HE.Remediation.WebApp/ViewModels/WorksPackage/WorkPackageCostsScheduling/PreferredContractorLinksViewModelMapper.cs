using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling
{
    public class PreferredContractorLinksViewModelMapper : Profile
    {
        public PreferredContractorLinksViewModelMapper()
        {
            CreateMap<GetPreferredContractorLinksResponse, PreferredContractorLinksViewModel>();
            CreateMap<PreferredContractorLinksViewModel, SetPreferredContractorLinksRequest>();
        }
    }
}

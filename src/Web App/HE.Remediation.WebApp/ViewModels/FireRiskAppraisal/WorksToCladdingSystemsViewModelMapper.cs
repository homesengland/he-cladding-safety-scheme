using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetWorksToCladdingSystems;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class WorksToCladdingSystemsViewModelMapper : Profile
    {
        public WorksToCladdingSystemsViewModelMapper()
        {
            CreateMap<IEnumerable<GetWorksToCladdingSystemsResponse>, List<SelectedWorksToCladdingSystemsViewModel>>();
            CreateMap<List<SelectedWorksToCladdingSystemsViewModel>, IEnumerable<GetWorksToCladdingSystemsResponse>>();
            CreateMap<SelectedWorksToCladdingSystemsViewModel, GetWorksToCladdingSystemsResponse>();
            CreateMap<GetWorksToCladdingSystemsResponse, SelectedWorksToCladdingSystemsViewModel>();
        }
    }
}
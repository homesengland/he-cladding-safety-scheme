using AutoMapper;
using HE.Remediation.Core.UseCase.Shared.Costs.Get;
using HE.Remediation.Core.UseCase.Shared.Costs.Set;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class MilestonesViewModelMapper : Profile
{
    public MilestonesViewModelMapper()
    {
        CreateMap<GetCostsResponse, MilestonesViewModel>()
            .ForMember(d => d.Costs, act => act.MapFrom(s => s));
        CreateMap<MilestonesViewModel, SetCostsRequest>();
    }
}

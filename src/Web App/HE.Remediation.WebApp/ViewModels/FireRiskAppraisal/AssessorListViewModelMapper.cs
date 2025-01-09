using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorList.GetAssessorList;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class AssessorListViewModelMapper : Profile
{
    public AssessorListViewModelMapper()
    {
        CreateMap<GetAssessorListResponse, AssessorListViewModel>();
        CreateMap<GetFireRiskAssessorListResult, AssessorCompanyViewModel>()
            .ForMember(d => d.RegionsCovered, o => o.MapFrom(s => FormatRegions(s.Regions)));
    }

    private static string FormatRegions(IList<GetFireRiskAssessorListResult.RegionResult> regions)
    {
        var allRegions = Enum.GetValues<ERegion>();

        return allRegions.All(x => regions.Any(r => r.Id == (int)x))
            ? "All Regions"
            : string.Join(", ", regions.OrderBy(x => x.Name).Select(x => x.Name));
    }
}
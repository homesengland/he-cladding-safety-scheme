using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorRating;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubContractorRatings;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class SubContractorRatingsViewModelMapper : Profile
{
    public SubContractorRatingsViewModelMapper()
    {
        CreateMap<GetSubContractorRatingsResponse, SubContractorRatingsViewModel>();
        CreateMap<GetSubContractorRatingResult, SubContractorRating>();
        CreateMap<SubContractorRatingsViewModel, SetSubContractorRatingsRequest>();
    }
}

using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorRating;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSubContractorRatings;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest.SubcontractorSurvey;

public class SubContractorRatingsViewModelMapper : Profile
{
    public SubContractorRatingsViewModelMapper()
    {
        CreateMap<GetSubContractorRatingsResponse, SubContractorRatingsViewModel>();
        CreateMap<GetSubContractorRatingResult, SubContractorRating>();
        CreateMap<SubContractorRatingsViewModel, SetSubContractorRatingsRequest>();
    }
}

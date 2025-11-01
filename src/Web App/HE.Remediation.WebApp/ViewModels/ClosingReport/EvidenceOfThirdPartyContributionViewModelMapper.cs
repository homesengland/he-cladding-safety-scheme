using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport.EvidenceOfThirdPartyContribution;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class EvidenceOfThirdPartyContributionViewModelMapper : Profile
    {
        public EvidenceOfThirdPartyContributionViewModelMapper()
        {
            CreateMap<GetEvidenceDetailsResponse, EvidenceOfThirdPartyContributionViewModel>();
            CreateMap<GetEvidenceDetailsResult, EvidenceOfThirdPartyContributionViewModel>()
                .ForMember(dest => dest.GetEvidenceDetailsResponse, opt => opt.MapFrom(src => new GetEvidenceDetailsResponse
                {
                    EvidenceDetailsResults = new List<GetEvidenceDetailsResult> { src }
                }))
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId));

            CreateMap<GetEvidenceDetailsResponse, EvidenceOfThirdPartyContributionViewModel>()
                .ForMember(dest => dest.GetEvidenceDetailsResponse, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(dest => dest.IsSubmitted, opt => opt.MapFrom(src => src.IsSubmitted))
                .ForMember(dest => dest.IsEditable, opt => opt.MapFrom(src => src.IsEditable))
                .ForMember(dest => dest.ReturnUrl, opt => opt.MapFrom(src => src.ReturnUrl));

            CreateMap<EvidenceOfThirdPartyContributionViewModel, SetEvidenceDetailRequest>()
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId));

            CreateMap<SetEvidenceDetailResponse, EvidenceOfThirdPartyContributionViewModel>();

            CreateMap<GetEvidenceDetailsRequest, EvidenceOfThirdPartyContributionViewModel>();
            CreateMap<EvidenceOfThirdPartyContributionViewModel, GetEvidenceDetailsRequest>();
            CreateMap<ChangeEvidenceDetailsRequest, EvidenceOfThirdPartyContributionViewModel>();
            CreateMap<EvidenceOfThirdPartyContributionViewModel, ChangeEvidenceDetailsRequest>();

        }
    }
}
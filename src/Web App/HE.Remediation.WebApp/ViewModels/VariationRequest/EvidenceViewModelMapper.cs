using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Add;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class EvidenceViewModelMapper : Profile
{
    public EvidenceViewModelMapper()
    {
        CreateMap<GetEvidenceResponse, EvidenceViewModel>();
        CreateMap<EvidenceViewModel, AddEvidenceRequest>();
        CreateMap<EvidenceViewModel, SetEvidenceRequest>();
    }
}

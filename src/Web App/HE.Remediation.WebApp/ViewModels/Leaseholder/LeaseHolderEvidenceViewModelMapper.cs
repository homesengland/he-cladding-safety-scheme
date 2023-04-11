using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetLeaseholderEvidence;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder;

public class LeaseHolderEvidenceViewModelMapper : Profile
{
    public LeaseHolderEvidenceViewModelMapper()
    {
        CreateMap<LeaseHolderEvidenceViewModel, SetLeaseHolderEvidenceRequest>()
            .ForMember(x => x.Completed, o => o.MapFrom(s => s.SubmitAction != ESubmitAction.Upload));
    }
}
using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class AddEditEvidenceDetailsViewModelMapper : Profile
    {
        public AddEditEvidenceDetailsViewModelMapper()
        {
            CreateMap<GetEvidenceDetailResponse, AddEditEvidenceDetailsViewModel>();
            CreateMap<AddEditEvidenceDetailsViewModel, SetEvidenceDetailRequest>();
        }
    }
}

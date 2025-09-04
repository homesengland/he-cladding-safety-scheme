using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetLeaseholderResidentUploadEvidence;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetLeaseholderResidentUploadEvidence;

using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class LeaseholderResidentEvidenceViewModelMapper : Profile
{
    public LeaseholderResidentEvidenceViewModelMapper()
    {
        CreateMap<GetLeaseholderResidentUploadEvidenceResponse, LeaseholderResidentEvidenceViewModel>();
        CreateMap<PaymentLeaseholderResidentUploadEvidenceResult, File>();
        CreateMap<LeaseholderResidentEvidenceViewModel, SetLeaseholderResidentUploadEvidenceRequest>();
    }
}
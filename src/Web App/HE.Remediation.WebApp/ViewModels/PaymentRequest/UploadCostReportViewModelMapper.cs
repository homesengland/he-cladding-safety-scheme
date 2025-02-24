using AutoMapper;
using ByteSizeLib;
using HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.SetCostReport;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetUploadCostReport;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class UploadCostReportViewModelMapper : Profile
{
    public UploadCostReportViewModelMapper()
    {
        CreateMap<UploadCostReportViewModel, SetCostRequest>();

        CreateMap<GetUploadCostReportResponse, UploadCostReportViewModel>();

        CreateMap<PaymentCostReportResult, HE.Remediation.WebApp.ViewModels.Shared.File>()
                .ForMember(x => x.FileSize, o => o.MapFrom(s => ByteSize.FromBytes(s.Size).ToString()));

        //CreateMap<PaymentCostReportResult, File>()
            //.ForMember(x => x.)
            //.ForMember(x => x.AddedFiles, o => o.MapFrom(f => f.AddedFiles));                        
    }
}

using AutoMapper;
using ByteSizeLib;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.Application.LeaseHolderEvidence.GetLeaseHolderEvidence;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport;

namespace HE.Remediation.WebApp.ViewModels.Shared
{
    public class FileUploadViewModelMapper : Profile
    {
        public FileUploadViewModelMapper()
        {
            CreateMap<GetLeaseHolderEvidenceResponse, File>()
                .ForMember(x => x.FileSize, o => o.MapFrom(s => ByteSize.FromBytes(s.Size).ToString()));

            CreateMap<GetFireRiskAppraisalReportResponse, File>()
                .ForMember(x => x.FileSize, o => o.MapFrom(s => ByteSize.FromBytes(s.Size).ToString()));

            CreateMap<FileResult, File>()
                .ForMember(x => x.FileSize, o => o.MapFrom(s => ByteSize.FromBytes(s.Size).ToString()));
        }
    }
}

using AutoMapper;
using ByteSizeLib;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetLeaseHolderEvidence;

namespace HE.Remediation.WebApp.ViewModels.Shared
{
    public class FileUploadViewModelMapper : Profile
    {
        public FileUploadViewModelMapper()
        {
            CreateMap<GetLeaseHolderEvidenceResponse, File>()
                .ForMember(x => x.FileSize, o => o.MapFrom(s => ByteSize.FromBytes(s.Size).ToString()));

            CreateMap<Core.Data.StoredProcedureResults.FileResult, File>()
                .ForMember(x => x.FileSize, o => o.MapFrom(s => ByteSize.FromBytes(s.Size).ToString()));

            CreateMap<Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport.FileResult, File>()
                .ForMember(x => x.FileSize, o => o.MapFrom(s => ByteSize.FromBytes(s.Size).ToString()));

            CreateMap<Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.GetFireRiskAssessmentReport.FileResult, File>()
                .ForMember(x => x.FileSize, o => o.MapFrom(s => ByteSize.FromBytes(s.Size).ToString()));
        }
    }
}

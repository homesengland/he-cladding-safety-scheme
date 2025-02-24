using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class UploadLeaseholderEngagementViewModelMapper : Profile
{
    public UploadLeaseholderEngagementViewModelMapper()
    {
        CreateMap<GetLeaseholderEngagementResponse, UploadLeaseholderEngagementViewModel>();
        CreateMap<UploadLeaseholderEngagementViewModel, AddLeaseholderEngagementFileRequest>();
    }
}
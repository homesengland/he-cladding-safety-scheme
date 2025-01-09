using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedLeadDesigner.GetAppointedLeadDesigner;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedLeadDesigner.SetAppointedLeadDesigner;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AppointedLeadDesignerViewModelMapper : Profile
{
    public AppointedLeadDesignerViewModelMapper()
    {
        CreateMap<GetAppointedLeadDesignerResponse, AppointedLeadDesignerViewModel>();
        CreateMap<AppointedLeadDesignerViewModel, SetAppointedLeadDesignerRequest>();
    }
}

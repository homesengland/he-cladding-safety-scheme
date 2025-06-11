using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities.RightToManage;

public class WhenRightToManageAcquiredViewModelMapper : Profile
{
    public WhenRightToManageAcquiredViewModelMapper()
    {
        CreateMap<GetWhenRightToManageAcquiredResponse, WhenRightToManageAcquiredViewModel>();
        CreateMap<WhenRightToManageAcquiredViewModel, SetWhenRightToManageAcquiredRequest>();
    }
}
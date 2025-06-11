using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities.RightToManage;

public class AcquiredRightToManageViewModelMapper : Profile
{
    public AcquiredRightToManageViewModelMapper()
    {
        CreateMap<GetAcquiredRightToManageResponse, AcquiredRightToManageViewModel>();
        CreateMap<AcquiredRightToManageViewModel, SetAcquiredRightToManageRequest>();
    }
}
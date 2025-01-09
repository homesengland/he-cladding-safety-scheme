using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetAddRole;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class AddRoleViewModelMapper : Profile
{
    public AddRoleViewModelMapper()
    {
        CreateMap<GetAddRoleResponse, AddRoleViewModel>();
    }
}

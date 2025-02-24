using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;

public class SetBuildingAddressRequest : IRequest<Unit>
{
    public string SelectedAddressId { get; set; }        
}

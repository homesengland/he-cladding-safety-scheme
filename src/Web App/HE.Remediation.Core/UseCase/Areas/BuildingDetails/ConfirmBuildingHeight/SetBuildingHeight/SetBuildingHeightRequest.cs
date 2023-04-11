using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;

public class SetBuildingHeightRequest : IRequest
{
    public int? NumberOfStoreys { get; set; }
}
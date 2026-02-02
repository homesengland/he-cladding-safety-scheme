using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.GetBuildingHeight;

public class GetBuildingHeightRequest : IRequest<GetBuildingHeightResponse>
{
	private GetBuildingHeightRequest() { }

	public static readonly GetBuildingHeightRequest Request = new();
}
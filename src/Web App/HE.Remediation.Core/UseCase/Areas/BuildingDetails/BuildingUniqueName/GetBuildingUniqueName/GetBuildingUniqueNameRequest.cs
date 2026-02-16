using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName
{
	public class GetBuildingUniqueNameRequest : IRequest<GetBuildingUniqueNameResponse>
	{
		private GetBuildingUniqueNameRequest() { }

		public static readonly GetBuildingUniqueNameRequest Request = new();
	}
}

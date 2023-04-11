using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.GetResidentialUnits
{
	public class GetResidentialUnitsRequest : IRequest<GetResidentialUnitsResponse>
	{
		private GetResidentialUnitsRequest() { }

		public static readonly GetResidentialUnitsRequest Request = new();
	}
}

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress
{
    public class GetBuildingAddressRequest : IRequest<GetBuildingAddressResponse>
    {
        private GetBuildingAddressRequest() { }
        public static readonly GetBuildingAddressRequest Request = new();
    }
}

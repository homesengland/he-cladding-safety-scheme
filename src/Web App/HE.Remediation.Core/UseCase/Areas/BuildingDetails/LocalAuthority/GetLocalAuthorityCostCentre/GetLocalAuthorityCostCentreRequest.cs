using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.GetLocalAuthorityCostCentre
{
    public class GetLocalAuthorityCostCentreRequest : IRequest<GetLocalAuthorityCostCentreResponse>
    {
        private GetLocalAuthorityCostCentreRequest() { }

        public static readonly GetLocalAuthorityCostCentreRequest Request = new();
    }
}

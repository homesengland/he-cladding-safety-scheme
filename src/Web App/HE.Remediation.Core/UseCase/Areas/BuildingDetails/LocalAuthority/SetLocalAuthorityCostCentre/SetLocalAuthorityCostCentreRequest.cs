using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.SetLocalAuthorityCostCentre
{
    public class SetLocalAuthorityCostCentreRequest : IRequest<Unit>
    {
        public string LocalAuthorityCostCentreId {  get; set; }
        private SetLocalAuthorityCostCentreRequest() { }

        public static readonly SetLocalAuthorityCostCentreRequest Request = new();
    }
}

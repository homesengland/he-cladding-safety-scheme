using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Location.BuildingLookup
{
    public class BuildingLookupRequest: IRequest<BuildingLookupResponse>
    {
        public string Postcode { get; set; }
    }
}

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.GetNonResidentialUnits
{
    public class GetNonResidentialUnitsRequest : IRequest<GetNonResidentialUnitsResponse>
    {
        private GetNonResidentialUnitsRequest() { }

        public static readonly GetNonResidentialUnitsRequest Request = new();
    }
}
